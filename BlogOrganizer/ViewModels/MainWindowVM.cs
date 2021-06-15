using BlogOrganizer.Models;
using BlogOrganizer.Views;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOrganizer.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region 手動操作用の記事[ManualContents]プロパティ
        /// <summary>
        /// 手動操作用の記事[ManualContents]プロパティ用変数
        /// </summary>
        Wp_ContentsM _ManualContents = new Wp_ContentsM();
        /// <summary>
        /// 手動操作用の記事[ManualContents]プロパティ
        /// </summary>
        public Wp_ContentsM ManualContents
        {
            get
            {
                return _ManualContents;
            }
            set
            {
                if (_ManualContents == null || !_ManualContents.Equals(value))
                {
                    _ManualContents = value;
                    NotifyPropertyChanged("ManualContents");
                }
            }
        }
        #endregion


        



        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Close(object sender, EventArgs e)
        {
            //throw new NotImplementedException();   
        }
        #endregion

        #region コンテンツの手動入力
        /// <summary>
        /// コンテンツの手動入力
        /// </summary>
        public void AddManualContents()
        {
            try
            {
                this.ManualContents.Post_date = DateTime.Now;
                this.BlogElement.WpContents.Items.Add(this.ManualContents);
                this.ManualContents = new Wp_ContentsM();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ブログの要素[BlogElement]プロパティ
        /// <summary>
        /// ブログの要素[BlogElement]プロパティ用変数
        /// </summary>
        BlogM _BlogElement = new BlogM();
        /// <summary>
        /// ブログの要素[BlogElement]プロパティ
        /// </summary>
        public BlogM BlogElement
        {
            get
            {
                return _BlogElement;
            }
            set
            {
                if (_BlogElement == null || !_BlogElement.Equals(value))
                {
                    _BlogElement = value;
                    NotifyPropertyChanged("BlogElement");
                }
            }
        }
        #endregion

        #region ファイルの出力処理
        /// <summary>
        /// ファイルの出力処理
        /// </summary>
        public void OutputContents()
        {
            try
            {
                StringBuilder content = new StringBuilder();
                foreach (var tmp in this.BlogElement.WpContents)
                {
                    content.AppendLine(tmp.Post_content_View);
                }


                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    //第1引数：ファイルパス
                    //第2引数：追記するテキスト 
                    File.WriteAllText(dialog.FileName, content.ToString());

                    ShowMessage.ShowNoticeOK("各記事を一つのファイルにまとめて保存しました。\r\nKH Coderとかで分析すると何か見えるかもしれません。", "通知");
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region カテゴリリスト[Categorys]プロパティ
        /// <summary>
        /// カテゴリリスト[Categorys]プロパティ用変数
        /// </summary>
        ModelList<CategoryM> _Categorys = new ModelList<CategoryM>();
        /// <summary>
        /// カテゴリリスト[Categorys]プロパティ
        /// </summary>
        public ModelList<CategoryM> Categorys
        {
            get
            {
                return _Categorys;
            }
            set
            {
                if (_Categorys == null || !_Categorys.Equals(value))
                {
                    _Categorys = value;
                    NotifyPropertyChanged("Categorys");
                    NotifyPropertyChanged("CategoryRowCount");
                }
            }
        }
        #endregion

        #region [CategoryRowCount]プロパティ
        /// <summary>
        /// [CategoryRowCount]プロパティ
        /// </summary>
        public int CategoryRowCount
        {
            get
            {
                return this.Categorys.Items.Count;
            }
        }
        #endregion


        #region ファイルを開く処理
        /// <summary>
        /// ファイルを開く処理
        /// </summary>
        public void OpenFile()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "テキストファイル (*.sql)|*.sql";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    var query_contents = FileAnalyzerM.GetQueryParameters(dialog.FileName);

                    foreach (var tmp in query_contents)
                    {
                        var wp_content = FileAnalyzerM.DivParameters(tmp);

                        // revisionやautosaveが含まれている場合は履歴なので無視
                        // コンテンツに文字列が含まれてない場合は無視
                        // タイトルがない場合は無視
                        if (wp_content.Post_type.Equals("post"))
                        {
                            this.BlogElement.Add(wp_content);
                        }
                    }

                    // カテゴリの作成処理
                    CreateCategory();
                }

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region タグセット
        /// <summary>
        /// タグセット
        /// </summary>
        public void SetArticleTags()
        {
            try
            {
                // 確認
                if (ShowMessage.ShowQuestionYesNo("各記事のタグを作成します。\r\n記事・文字数によっては数分程度かかる場合があります。\r\n実行してもよろしいですか？", "確認") == System.Windows.MessageBoxResult.Yes)
                {
                    // MeCabで各記事を形態素解析
                    this.BlogElement.AnalysisMeCab(this.BlogElement.ArticleNounDelimita);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region カテゴリのセット
        /// <summary>
        /// カテゴリのセット
        /// </summary>
        public void SetCategory()
        {

            foreach (var tmp in this.BlogElement.WpContents.Items)
            {
                var cate = tmp.GetCategory(this.Categorys, this.BlogElement.CategoryTop, this.BlogElement.CountDisplay);
                tmp.Category = cate;
            }

            this.BlogElement.SetCategoryCount();
        }
        #endregion

        #region カテゴリの保存処理
        /// <summary>
        /// カテゴリの保存処理
        /// </summary>
        public void SaveCategory()
        {
            try
            {
                // ファイル保存ダイアログを生成します。
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "ブログ解析カテゴリファイル(*.bocate)|*.bocate";


                // 保存ボタン以外が押下された場合
                if (dialog.ShowDialog() == true)
                {
                    XMLUtil.Seialize<ModelList<CategoryM>>(dialog.FileName, this.Categorys);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region カテゴリの読み込み処理
        /// <summary>
        /// カテゴリの読み込み処理
        /// </summary>
        public void LoadCagetory()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "ブログ解析カテゴリファイル(*.bocate)|*.bocate";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    this.Categorys = XMLUtil.Deserialize<ModelList<CategoryM>>(dialog.FileName);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 各記事の保存処理
        /// <summary>
        /// 各記事の保存処理
        /// </summary>
        public void SaveArticles()
        {
            try
            {
                // ファイル保存ダイアログを生成します。
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "ブログ記事ファイル(*.boart)|*.boart";


                // 保存ボタン以外が押下された場合
                if (dialog.ShowDialog() == true)
                {
                    XMLUtil.Seialize<BlogM>(dialog.FileName, this.BlogElement);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 各記事のファイル読み込み処理
        /// <summary>
        /// 各記事のファイル読み込み処理
        /// </summary>
        public void LoadArticles()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "ブログ記事ファイル(*.boart)|*.boart";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    this.BlogElement = XMLUtil.Deserialize<BlogM>(dialog.FileName);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 全記事からカテゴリの作成処理
        /// <summary>
        /// 全記事からカテゴリの作成処理
        /// </summary>
        public void CreateCategory()
        {
            try
            {
                // 確認
                if (ShowMessage.ShowQuestionYesNo("各記事の情報からカテゴリのリストを作成します。\r\n記事・文字数によっては数分程度かかる場合があります。\r\n実行してもよろしいですか？", "確認") == System.Windows.MessageBoxResult.Yes)
                {
                    this.Categorys = this.BlogElement.GetCategorys();
                    this.BlogElement.SetContentsCount();
                    this.BlogElement.SetCategoryCount();

                    ShowMessage.ShowNoticeOK("カテゴリの作成が完了しました。", "通知");
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region 全てクリア
        /// <summary>
        /// 全てクリア
        /// </summary>
        public void Clear()
        {
            try
            {
                this.BlogElement = new BlogM();
                this.Categorys = new ModelList<CategoryM>();
                NotifyPropertyChanged("CategoryRowCount");
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion
    }
}
