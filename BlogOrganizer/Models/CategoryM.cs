using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOrganizer.Models
{
    public class CategoryM : ModelBase
	{
		#region [Noun]プロパティ
		/// <summary>
		/// [Noun]プロパティ用変数
		/// </summary>
		string _Noun = string.Empty;
		/// <summary>
		/// [Noun]プロパティ
		/// </summary>
		public string Noun
		{
			get
			{
				return _Noun;
			}
			set
			{
				if (!_Noun.Equals(value))
				{
					_Noun = value;
					NotifyPropertyChanged("Noun");
				}
			}
		}
		#endregion
		#region [Count]プロパティ
		/// <summary>
		/// [Count]プロパティ用変数
		/// </summary>
		int _Count = 0;
		/// <summary>
		/// [Count]プロパティ
		/// </summary>
		public int Count
		{
			get
			{
				return _Count;
			}
			set
			{
				if (!_Count.Equals(value))
				{
					_Count = value;
					NotifyPropertyChanged("Count");
				}
			}
		}
		#endregion
		#region [IsSelected]プロパティ
		/// <summary>
		/// [IsSelected]プロパティ用変数
		/// </summary>
		bool _IsSelected = false;
		/// <summary>
		/// [IsSelected]プロパティ
		/// </summary>
		public bool IsSelected
		{
			get
			{
				return _IsSelected;
			}
			set
			{
				if (!_IsSelected.Equals(value))
				{
					_IsSelected = value;
					NotifyPropertyChanged("IsSelected");
				}
			}
		}
		#endregion

	}
}
