using MeCab;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOrganizer.Models
{
    public class Wp_ContentsM : ModelBase
	{

		#region Insert文の1要素分お挿入句の全文[FullText]プロパティ
		/// <summary>
		/// Insert文の1要素分お挿入句の全文[FullText]プロパティ用変数
		/// </summary>
		string _FullText = string.Empty;
		/// <summary>
		/// Insert文の1要素分お挿入句の全文[FullText]プロパティ
		/// </summary>
		public string FullText
		{
			get
			{
				return _FullText;
			}
			set
			{
				if (!_FullText.Equals(value))
				{
					_FullText = value;
					NotifyPropertyChanged("FullText");
				}
			}
		}
		#endregion

		#region MeCabの解析結果[MeCabItems]プロパティ
		/// <summary>
		/// MeCabの解析結果[MeCabItems]プロパティ用変数
		/// </summary>
		ModelList<MeCab.MeCabNode> _MeCabItems = new ModelList<MeCab.MeCabNode>();
		/// <summary>
		/// MeCabの解析結果[MeCabItems]プロパティ
		/// </summary>
		public ModelList<MeCab.MeCabNode> MeCabItems
		{
			get
			{
				return _MeCabItems;
			}
			set
			{
				if (_MeCabItems == null || !_MeCabItems.Equals(value))
				{
					_MeCabItems = value;
					NotifyPropertyChanged("MeCabItems");
				}
			}
		}
		#endregion

		#region [TopNoun]プロパティ
		/// <summary>
		/// [TopNoun]プロパティ用変数
		/// </summary>
		string _TopNoun = string.Empty;
		/// <summary>
		/// [TopNoun]プロパティ
		/// </summary>
		public string TopNoun
		{
			get
			{
				return _TopNoun;
			}
			set
			{
				if (!_TopNoun.Equals(value))
				{
					_TopNoun = value;
					NotifyPropertyChanged("TopNoun");
				}
			}
		}
		#endregion


		#region [ID]プロパティ
		/// <summary>
		/// [ID]プロパティ用変数
		/// </summary>
		Int64 _ID = 0;
		/// <summary>
		/// [ID]プロパティ
		/// </summary>
		public Int64 ID
		{
			get
			{
				return _ID;
			}
			set
			{
				if (!_ID.Equals(value))
				{
					_ID = value;
					NotifyPropertyChanged("ID");
				}
			}
		}
		#endregion

		#region [Post_author]プロパティ
		/// <summary>
		/// [Post_author]プロパティ用変数
		/// </summary>
		Int64 _Post_author = 0;
		/// <summary>
		/// [Post_author]プロパティ
		/// </summary>
		public Int64 Post_author
		{
			get
			{
				return _Post_author;
			}
			set
			{
				if (!_Post_author.Equals(value))
				{
					_Post_author = value;
					NotifyPropertyChanged("Post_author");
				}
			}
		}
		#endregion

		#region [Post_date]プロパティ
		/// <summary>
		/// [Post_date]プロパティ用変数
		/// </summary>
		DateTime _Post_date = DateTime.MinValue;
		/// <summary>
		/// [Post_date]プロパティ
		/// </summary>
		public DateTime Post_date
		{
			get
			{
				return _Post_date;
			}
			set
			{
				if (!_Post_date.Equals(value))
				{
					_Post_date = value;
					NotifyPropertyChanged("Post_date");
				}
			}
		}
		#endregion

		#region [Post_date_gmt]プロパティ
		/// <summary>
		/// [Post_date_gmt]プロパティ用変数
		/// </summary>
		DateTime _Post_date_gmt = DateTime.MinValue;
		/// <summary>
		/// [Post_date_gmt]プロパティ
		/// </summary>
		public DateTime Post_date_gmt
		{
			get
			{
				return _Post_date_gmt;
			}
			set
			{
				if (!_Post_date_gmt.Equals(value))
				{
					_Post_date_gmt = value;
					NotifyPropertyChanged("Post_date_gmt");
				}
			}
		}
		#endregion

		#region [Post_content]プロパティ
		/// <summary>
		/// [Post_content]プロパティ用変数
		/// </summary>
		string _Post_content = string.Empty;
		/// <summary>
		/// [Post_content]プロパティ
		/// </summary>
		public string Post_content
		{
			get
			{
				return _Post_content;
			}
			set
			{
				if (!_Post_content.Equals(value))
				{
					_Post_content = value;
					NotifyPropertyChanged("Post_content");
				}
			}
		}
		#endregion

		#region [Post_title]プロパティ
		/// <summary>
		/// [Post_title]プロパティ用変数
		/// </summary>
		string _Post_title = string.Empty;
		/// <summary>
		/// [Post_title]プロパティ
		/// </summary>
		public string Post_title
		{
			get
			{
				return _Post_title;
			}
			set
			{
				if (!_Post_title.Equals(value))
				{
					_Post_title = value;
					NotifyPropertyChanged("Post_title");
				}
			}
		}
		#endregion

		#region [Post_excerpt]プロパティ
		/// <summary>
		/// [Post_excerpt]プロパティ用変数
		/// </summary>
		string _Post_excerpt = string.Empty;
		/// <summary>
		/// [Post_excerpt]プロパティ
		/// </summary>
		public string Post_excerpt
		{
			get
			{
				return _Post_excerpt;
			}
			set
			{
				if (!_Post_excerpt.Equals(value))
				{
					_Post_excerpt = value;
					NotifyPropertyChanged("Post_excerpt");
				}
			}
		}
		#endregion

		#region [Post_status]プロパティ
		/// <summary>
		/// [Post_status]プロパティ用変数
		/// </summary>
		string _Post_status = string.Empty;
		/// <summary>
		/// [Post_status]プロパティ
		/// </summary>
		public string Post_status
		{
			get
			{
				return _Post_status;
			}
			set
			{
				if (!_Post_status.Equals(value))
				{
					_Post_status = value;
					NotifyPropertyChanged("Post_status");
				}
			}
		}
		#endregion

		#region [Comment_status]プロパティ
		/// <summary>
		/// [Comment_status]プロパティ用変数
		/// </summary>
		string _Comment_status = string.Empty;
		/// <summary>
		/// [Comment_status]プロパティ
		/// </summary>
		public string Comment_status
		{
			get
			{
				return _Comment_status;
			}
			set
			{
				if (!_Comment_status.Equals(value))
				{
					_Comment_status = value;
					NotifyPropertyChanged("Comment_status");
				}
			}
		}
		#endregion

		#region [Ping_status]プロパティ
		/// <summary>
		/// [Ping_status]プロパティ用変数
		/// </summary>
		string _Ping_status = string.Empty;
		/// <summary>
		/// [Ping_status]プロパティ
		/// </summary>
		public string Ping_status
		{
			get
			{
				return _Ping_status;
			}
			set
			{
				if (!_Ping_status.Equals(value))
				{
					_Ping_status = value;
					NotifyPropertyChanged("Ping_status");
				}
			}
		}
		#endregion

		#region [Post_password]プロパティ
		/// <summary>
		/// [Post_password]プロパティ用変数
		/// </summary>
		string _Post_password = string.Empty;
		/// <summary>
		/// [Post_password]プロパティ
		/// </summary>
		public string Post_password
		{
			get
			{
				return _Post_password;
			}
			set
			{
				if (!_Post_password.Equals(value))
				{
					_Post_password = value;
					NotifyPropertyChanged("Post_password");
				}
			}
		}
		#endregion

		#region [Post_name]プロパティ
		/// <summary>
		/// [Post_name]プロパティ用変数
		/// </summary>
		string _Post_name = string.Empty;
		/// <summary>
		/// [Post_name]プロパティ
		/// </summary>
		public string Post_name
		{
			get
			{
				return _Post_name;
			}
			set
			{
				if (!_Post_name.Equals(value))
				{
					_Post_name = value;
					NotifyPropertyChanged("Post_name");
				}
			}
		}
		#endregion

		#region [To_ping]プロパティ
		/// <summary>
		/// [To_ping]プロパティ用変数
		/// </summary>
		string _To_ping = string.Empty;
		/// <summary>
		/// [To_ping]プロパティ
		/// </summary>
		public string To_ping
		{
			get
			{
				return _To_ping;
			}
			set
			{
				if (!_To_ping.Equals(value))
				{
					_To_ping = value;
					NotifyPropertyChanged("To_ping");
				}
			}
		}
		#endregion

		#region [Pinged]プロパティ
		/// <summary>
		/// [Pinged]プロパティ用変数
		/// </summary>
		string _Pinged = string.Empty;
		/// <summary>
		/// [Pinged]プロパティ
		/// </summary>
		public string Pinged
		{
			get
			{
				return _Pinged;
			}
			set
			{
				if (!_Pinged.Equals(value))
				{
					_Pinged = value;
					NotifyPropertyChanged("Pinged");
				}
			}
		}
		#endregion

		#region [Post_modified]プロパティ
		/// <summary>
		/// [Post_modified]プロパティ用変数
		/// </summary>
		DateTime _Post_modified = DateTime.MinValue;
		/// <summary>
		/// [Post_modified]プロパティ
		/// </summary>
		public DateTime Post_modified
		{
			get
			{
				return _Post_modified;
			}
			set
			{
				if (!_Post_modified.Equals(value))
				{
					_Post_modified = value;
					NotifyPropertyChanged("Post_modified");
				}
			}
		}
		#endregion

		#region [Post_modified_gmt]プロパティ
		/// <summary>
		/// [Post_modified_gmt]プロパティ用変数
		/// </summary>
		DateTime _Post_modified_gmt = DateTime.MinValue;
		/// <summary>
		/// [Post_modified_gmt]プロパティ
		/// </summary>
		public DateTime Post_modified_gmt
		{
			get
			{
				return _Post_modified_gmt;
			}
			set
			{
				if (!_Post_modified_gmt.Equals(value))
				{
					_Post_modified_gmt = value;
					NotifyPropertyChanged("Post_modified_gmt");
				}
			}
		}
		#endregion

		#region [Post_content_filtered]プロパティ
		/// <summary>
		/// [Post_content_filtered]プロパティ用変数
		/// </summary>
		string _Post_content_filtered = string.Empty;
		/// <summary>
		/// [Post_content_filtered]プロパティ
		/// </summary>
		public string Post_content_filtered
		{
			get
			{
				return _Post_content_filtered;
			}
			set
			{
				if (!_Post_content_filtered.Equals(value))
				{
					_Post_content_filtered = value;
					NotifyPropertyChanged("Post_content_filtered");
				}
			}
		}
		#endregion

		#region [Post_parent]プロパティ
		/// <summary>
		/// [Post_parent]プロパティ用変数
		/// </summary>
		Int64 _Post_parent = 0;
		/// <summary>
		/// [Post_parent]プロパティ
		/// </summary>
		public Int64 Post_parent
		{
			get
			{
				return _Post_parent;
			}
			set
			{
				if (!_Post_parent.Equals(value))
				{
					_Post_parent = value;
					NotifyPropertyChanged("Post_parent");
				}
			}
		}
		#endregion

		#region [Guid]プロパティ
		/// <summary>
		/// [Guid]プロパティ用変数
		/// </summary>
		string _Guid = string.Empty;
		/// <summary>
		/// [Guid]プロパティ
		/// </summary>
		public string Guid
		{
			get
			{
				return _Guid;
			}
			set
			{
				if (!_Guid.Equals(value))
				{
					_Guid = value;
					NotifyPropertyChanged("Guid");
				}
			}
		}
		#endregion

		#region [Menu_order]プロパティ
		/// <summary>
		/// [Menu_order]プロパティ用変数
		/// </summary>
		int _Menu_order = 0;
		/// <summary>
		/// [Menu_order]プロパティ
		/// </summary>
		public int Menu_order
		{
			get
			{
				return _Menu_order;
			}
			set
			{
				if (!_Menu_order.Equals(value))
				{
					_Menu_order = value;
					NotifyPropertyChanged("Menu_order");
				}
			}
		}
		#endregion

		#region [Post_type]プロパティ
		/// <summary>
		/// [Post_type]プロパティ用変数
		/// </summary>
		string _Post_type = string.Empty;
		/// <summary>
		/// [Post_type]プロパティ
		/// </summary>
		public string Post_type
		{
			get
			{
				return _Post_type;
			}
			set
			{
				if (!_Post_type.Equals(value))
				{
					_Post_type = value;
					NotifyPropertyChanged("Post_type");
				}
			}
		}
		#endregion

		#region [Post_mime_type]プロパティ
		/// <summary>
		/// [Post_mime_type]プロパティ用変数
		/// </summary>
		string _Post_mime_type = string.Empty;
		/// <summary>
		/// [Post_mime_type]プロパティ
		/// </summary>
		public string Post_mime_type
		{
			get
			{
				return _Post_mime_type;
			}
			set
			{
				if (!_Post_mime_type.Equals(value))
				{
					_Post_mime_type = value;
					NotifyPropertyChanged("Post_mime_type");
				}
			}
		}
		#endregion

		#region [Comment_count]プロパティ
		/// <summary>
		/// [Comment_count]プロパティ用変数
		/// </summary>
		Int64 _Comment_count = 0;
		/// <summary>
		/// [Comment_count]プロパティ
		/// </summary>
		public Int64 Comment_count
		{
			get
			{
				return _Comment_count;
			}
			set
			{
				if (!_Comment_count.Equals(value))
				{
					_Comment_count = value;
					NotifyPropertyChanged("Comment_count");
				}
			}
		}
		#endregion


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

			ret.ID = long.Parse(mc1.ElementAt(0).ToString().Replace(",",""));
			ret.Post_author = long.Parse(mc1.ElementAt(1).ToString().Replace(",", ""));
			ret.Post_parent = long.Parse(mc1.ElementAt(2).ToString().Replace(",", ""));
			ret.Menu_order = int.Parse(mc1.ElementAt(3).ToString().Replace(",", ""));
			//ret.Comment_count = long.Parse(mc1.ElementAt(4).ToString().Replace(")", ""));


			//Insert文の()内の挿入句をすべて抽出する
			System.Text.RegularExpressions.MatchCollection mc2 =
				System.Text.RegularExpressions.Regex.Matches(
				full_text, match_pattern2);

			DateTime tmp_date = DateTime.MinValue;
			DateTime.TryParseExact(mc2.ElementAt(0).ToString().Replace(",", "").Replace("'",""), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out tmp_date);
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

		#region 記事内容の画面表示用[Post_content_View]プロパティ
		/// <summary>
		/// 記事内容の画面表示用[Post_content_View]プロパティ用変数
		/// </summary>
		string _Post_content_View = string.Empty;
		/// <summary>
		/// 記事内容の画面表示用[Post_content_View]プロパティ
		/// </summary>
		public string Post_content_View
		{
			get
			{
				return _Post_content_View;
			}
			set
			{
				if (!_Post_content_View.Equals(value))
				{
					_Post_content_View = value;
					NotifyPropertyChanged("Post_content_View");
				}
			}
		}
		#endregion


		public void UseMecab()
        {
			var tagger = MeCabTagger.Create();
			this.MeCabItems.Items.Clear();
			List<string> noun = new List<string>();


			foreach (var node in tagger.ParseToNodes(this.Post_content_View))
			{
                if (0 < node.CharType)
                {
                    this.MeCabItems.Items.Add(node);
					var tmp = node.Feature.Split(",");


					if (tmp.ElementAt(0).Equals("名詞"))
                    {
						noun.Add(node.Surface);
                    }


                }
			}
			var nounGroup = noun.GroupBy(c => c)
				.Select(c => new { Noun = c.Key, Count = c.Count() })
				.OrderByDescending(c => c.Count);

			StringBuilder noun_text = new StringBuilder();
			foreach (var noun_item in nounGroup)
			{
				noun_text.Append(noun_item.Noun.ToString() + string.Format("({0}) ", noun_item.Count));
			}

			this.TopNoun = noun_text.ToString();

		}

	}
}
