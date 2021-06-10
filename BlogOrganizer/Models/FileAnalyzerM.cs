using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOrganizer.Models
{
    public class FileAnalyzerM : ModelBase
    {
		/// <summary>
		/// wp_contentsへのINSERT文のパラメータをクエリから抜き出す
		/// </summary>
		/// <param name="file_path">ファイルパス</param>
		/// <returns>パラメータのリスト</returns>
		public static List<string> GetQueryParameters(string file_path)
		{
			List<string> contents = new List<string>();
			using (StreamReader sr = new StreamReader(	file_path, Encoding.UTF8))
			{
				bool content_query_F = false;
				string line = "";

				while ((line = sr.ReadLine()) != null)
				{
					if (content_query_F)
					{
						string match_pattern
							= @"\((\d*?),(\d*?),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('.*?'),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),('.*?'),(.*?),('.*?'),(.*?),('.*?'),('.*?'),(.*?)\)";// (.*?),\d*\)";

						//Insert文の()内の挿入句をすべて抽出する
						System.Text.RegularExpressions.MatchCollection mc =
							System.Text.RegularExpressions.Regex.Matches(
							line, match_pattern);

						foreach (var tmp in mc)
						{
							contents.Add(tmp.ToString());
						}

					}

					if (line.Contains("LOCK TABLES `wp_posts` WRITE;"))
					{
						content_query_F = true;
					}
				}
			}
			return contents;
		}


		#region パラメータの分解処理
		/// <summary>
		/// パラメータの分解処理
		/// </summary>
		/// <param name="full_text">クエリ全文</param>
		/// <returns>クエリ分解結果</returns>
		public static Wp_ContentsM DivParameters(string full_text)
		{
			Wp_ContentsM ret = new Wp_ContentsM();
			string div_text = full_text = full_text.Trim().Replace("\\r", "").Replace("\\n", "");
			div_text = div_text.Replace("(", "").Replace(")", "");

			ret.FullText = full_text;


			string match_pattern1 = @"([0-9]+?),";
			string match_pattern2 = @"('\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d'),";
			string match_pattern3 = @"('.*?'),";


			//Insert文の()内の挿入句をすべて抽出する
			System.Text.RegularExpressions.MatchCollection mc1 =
				System.Text.RegularExpressions.Regex.Matches(
				full_text, match_pattern1);

			ret.ID = long.Parse(mc1.ElementAt(0).ToString().Replace(",", ""));
			ret.Post_author = long.Parse(mc1.ElementAt(1).ToString().Replace(",", ""));
			ret.Post_parent = long.Parse(mc1.ElementAt(2).ToString().Replace(",", ""));
			ret.Menu_order = int.Parse(mc1.ElementAt(3).ToString().Replace(",", ""));
			//ret.Comment_count = long.Parse(mc1.ElementAt(4).ToString().Replace(")", ""));


			//Insert文の()内の挿入句をすべて抽出する
			System.Text.RegularExpressions.MatchCollection mc2 =
				System.Text.RegularExpressions.Regex.Matches(
				full_text, match_pattern2);

			DateTime tmp_date = DateTime.MinValue;
			DateTime.TryParseExact(mc2.ElementAt(0).ToString().Replace(",", "").Replace("'", ""), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tmp_date);
			ret.Post_date = tmp_date;
			DateTime.TryParseExact(mc2.ElementAt(1).ToString().Replace(",", "").Replace("'", ""), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tmp_date);
			ret.Post_date_gmt = tmp_date;
			DateTime.TryParseExact(mc2.ElementAt(2).ToString().Replace(",", "").Replace("'", ""), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tmp_date);
			ret.Post_modified = tmp_date;
			DateTime.TryParseExact(mc2.ElementAt(3).ToString().Replace(",", "").Replace("'", ""), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tmp_date);
			ret.Post_modified_gmt = tmp_date;


			full_text = full_text.Replace(mc1.ElementAt(0).ToString(), "");
			full_text = full_text.Replace(mc1.ElementAt(1).ToString(), "");
			full_text = full_text.Replace(mc1.ElementAt(2).ToString(), "");
			full_text = full_text.Replace(mc1.ElementAt(3).ToString(), "");
			//full_text = full_text.Replace(mc1.ElementAt(4).ToString(), "");

			full_text = full_text.Replace(mc2.ElementAt(0).ToString(), "");
			full_text = full_text.Replace(mc2.ElementAt(1).ToString(), "");
			full_text = full_text.Replace(mc2.ElementAt(2).ToString(), "");
			full_text = full_text.Replace(mc2.ElementAt(3).ToString(), "");


			//Insert文の()内の挿入句をすべて抽出する
			System.Text.RegularExpressions.MatchCollection mc3 =
				System.Text.RegularExpressions.Regex.Matches(
				full_text, match_pattern3);

			int index = mc3.Count - 1;
			ret.Post_mime_type = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_type = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Guid = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_content_filtered = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Pinged = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.To_ping = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_name = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_password = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Ping_status = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Comment_status = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_status = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_excerpt = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");
			ret.Post_title = mc3.ElementAt(index--).ToString().Replace(",", "").Replace("'", "");

			for (int index_tmp = index; index_tmp >= 0; index_tmp--)
			{
				ret.Post_content = mc3.ElementAt(index_tmp).ToString().Replace(",", "").Replace("'", "");
			}


			string match_pattern4 = "<(\".*?\"|'.*?'|[^'\"])*?>";
			//Insert文の()内の挿入句をすべて抽出する
			System.Text.RegularExpressions.MatchCollection mc4 =
				System.Text.RegularExpressions.Regex.Matches(
				ret.Post_content, match_pattern4);

			string tmp_txt = ret.Post_content;
			foreach (var html_tag in mc4)
			{
				tmp_txt = tmp_txt.Replace(html_tag.ToString(), "");
			}

			ret.Post_content_View = tmp_txt;



			return ret;
		}
		#endregion
	}
}
