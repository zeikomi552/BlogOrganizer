using BlogOrganizer.Models;
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
        #region 数量表示[CountDisplay]プロパティ
        /// <summary>
        /// 数量表示[CountDisplay]プロパティ用変数
        /// </summary>
        bool _CountDisplay = false;
        /// <summary>
        /// 数量表示[CountDisplay]プロパティ
        /// </summary>
        public bool CountDisplay
        {
            get
            {
                return _CountDisplay;
            }
            set
            {
                if (!_CountDisplay.Equals(value))
                {
                    _CountDisplay = value;
                    NotifyPropertyChanged("CountDisplay");
                }
            }
        }
        #endregion

        #region 全行数[RowNum]プロパティ
        /// <summary>
        /// 全行数[RowNum]プロパティ用変数
        /// </summary>
        int _RowNum = 0;
        /// <summary>
        /// 全行数[RowNum]プロパティ
        /// </summary>
        public int RowNum
        {
            get
            {
                return _RowNum;
            }
            set
            {
                if (!_RowNum.Equals(value))
                {
                    _RowNum = value;
                    NotifyPropertyChanged("RowNum");
                }
            }
        }
        #endregion

        #region 名詞数[NounNum]プロパティ
        /// <summary>
        /// 名詞数[NounNum]プロパティ用変数
        /// </summary>
        int _NounNum = 0;
        /// <summary>
        /// 名詞数[NounNum]プロパティ
        /// </summary>
        public int NounNum
        {
            get
            {
                return _NounNum;
            }
            set
            {
                if (!_NounNum.Equals(value))
                {
                    _NounNum = value;
                    NotifyPropertyChanged("NounNum");
                }
            }
        }
        #endregion

        #region カテゴリTopXの指定数[CategoryTop]プロパティ
        /// <summary>
        /// カテゴリTopXの指定数[CategoryTop]プロパティ用変数
        /// </summary>
        int _CategoryTop = 1;
        /// <summary>
        /// カテゴリTopXの指定数[CategoryTop]プロパティ
        /// </summary>
        public int CategoryTop
        {
            get
            {
                return _CategoryTop;
            }
            set
            {
                if (!_CategoryTop.Equals(value))
                {
                    _CategoryTop = value;
                    NotifyPropertyChanged("CategoryTop");
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

        #region 各記事のタグの区切り文字(true:カンマ false(数))[ArticleNounDelimita]プロパティ
        /// <summary>
        /// 各記事のタグの区切り文字(true:カンマ false(数))[ArticleNounDelimita]プロパティ用変数
        /// </summary>
        bool _ArticleNounDelimita = false;
        /// <summary>
        /// 各記事のタグの区切り文字(true:カンマ false(数))[ArticleNounDelimita]プロパティ
        /// </summary>
        public bool ArticleNounDelimita
        {
            get
            {
                return _ArticleNounDelimita;
            }
            set
            {
                if (!_ArticleNounDelimita.Equals(value))
                {
                    _ArticleNounDelimita = value;
                    NotifyPropertyChanged("ArticleNounDelimita");
                }
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
                }

                var noun_rank = this.BlogElement.GetNounRank();

                ModelList<CategoryM> tmp_cate = new ModelList<CategoryM>();
                foreach (var tmp in noun_rank)
                {
                    CategoryM cate = new CategoryM();
                    cate.Noun = tmp.Key;
                    cate.Count = tmp.Value;
                    tmp_cate.Items.Add(cate);
                }
                this.Categorys = tmp_cate;

                this.BlogElement.SetContentsCount();
                this.BlogElement.SetCategoryCount();
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
                // MeCabで各記事を形態素解析
                this.BlogElement.AnalysisMeCab(this.ArticleNounDelimita);
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
                var cate = tmp.GetCategory(this.Categorys, this.CategoryTop, this.CountDisplay);
                tmp.Category = cate;
            }

            this.BlogElement.SetCategoryCount();
        }
        #endregion

    }
}
