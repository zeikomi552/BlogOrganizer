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
        public override void Init(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public override void Close(object sender, EventArgs e)
        {
            //throw new NotImplementedException();   
        }

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


        public void OpenFile()
        {
            // ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "テキストファイル (*.sql)|*.sql";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                string line = "";
                //ArrayList al = new ArrayList();

                using (StreamReader sr = new StreamReader(
                    dialog.FileName, Encoding.UTF8))
                {
                    bool content_query_F = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (content_query_F)
                        {
                            //string match_pattern = @"(.,.,'\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d','\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d','','.+','','.+','.+','.+','','.+','','','\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d','\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d','',.+,'.+',\d,'.+','.+',.)";
                            string match_pattern 
                                = @"\((\d*?),(\d*?),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('.*?'),(.*?),('.*?'),(.*?),('.*?'),('.*?'),(.*?)\)";// (.*?),\d*\)";

                            //Insert文の()内の挿入句をすべて抽出する
                            System.Text.RegularExpressions.MatchCollection mc =
                                System.Text.RegularExpressions.Regex.Matches(
                                line, match_pattern);

                            foreach (var tmp in mc)
                            {
                                var wp_content = Wp_ContentsM.DivParameters(tmp.ToString());

                                // revisionやautosaveが含まれている場合は履歴なので無視
                                // コンテンツに文字列が含まれてない場合は無視
                                // タイトルがない場合は無視
                                if (wp_content.Post_type.Equals("post"))
                                {
                                    this.BlogElement.Add(wp_content);
                                }
                            }


                        }

                        if (line.Contains("LOCK TABLES `wp_posts` WRITE;"))
                        {
                            content_query_F = true;
                        }
                    }
                }
            }
        }
    }
}
