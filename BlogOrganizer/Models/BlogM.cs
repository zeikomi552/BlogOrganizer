using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOrganizer.Models
{
	public class BlogM : ModelBase
	{
		#region wp_contentsの要素[WpContents]プロパティ
		/// <summary>
		/// wp_contentsの要素[WpContents]プロパティ用変数
		/// </summary>
		ModelList<Wp_ContentsM> _WpContents = new ModelList<Wp_ContentsM>();
		/// <summary>
		/// wp_contentsの要素[WpContents]プロパティ
		/// </summary>
		public ModelList<Wp_ContentsM> WpContents
		{
			get
			{
				return _WpContents;
			}
			set
			{
				if (_WpContents == null || !_WpContents.Equals(value))
				{
					_WpContents = value;
					NotifyPropertyChanged("WpContents");
				}
			}
		}
		#endregion

		public void Add(Wp_ContentsM item)
		{
			this.WpContents.Items.Add(item);
		}
			


		public static void DivQuery(string insert_query)
        {
			insert_query = insert_query.Trim().Replace("\\r", "").Replace("\\n", "");
			insert_query = insert_query.Replace("(", "").Replace(")", "");


			string match_pattern = @",'(.*?)'.";

			System.Text.RegularExpressions.MatchCollection mc =
				System.Text.RegularExpressions.Regex.Matches(
				insert_query, match_pattern);


			foreach (var tmp in mc)
			{
				string data = tmp.ToString();
			}
		}

    }
}
