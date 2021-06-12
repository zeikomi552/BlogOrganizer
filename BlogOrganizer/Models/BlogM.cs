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

		#region 全記事を1つにまとめたもの[AllContents]プロパティ
		/// <summary>
		/// 全記事を1つにまとめたもの[AllContents]プロパティ用変数
		/// </summary>
		string _AllContents = string.Empty;
		/// <summary>
		/// 全記事を1つにまとめたもの[AllContents]プロパティ
		/// </summary>
		public string AllContents
		{
			get
			{
				return _AllContents;
			}
			set
			{
				if (!_AllContents.Equals(value))
				{
					_AllContents = value;
					NotifyPropertyChanged("AllContents");
				}
			}
		}
		#endregion

		#region 全記事を連結して一つにする
		/// <summary>
		/// 全記事を連結して一つにする
		/// </summary>
		public void ConbineAllContents()
		{
			this.AllContents = this.GetAllContents();
		}
		#endregion

		#region 全記事の取得処理
		/// <summary>
		/// 全記事の取得処理
		/// </summary>
		/// <returns>全記事をくっつけたもの</returns>
		public string GetAllContents()
		{
			StringBuilder content = new StringBuilder();
			foreach (var tmp in this.WpContents)
			{
				content.AppendLine(tmp.Post_content_View);
			}

			return content.ToString();
		}
		#endregion

		#region コンテンツの追加処理
		/// <summary>
		/// コンテンツの追加処理
		/// </summary>
		/// <param name="item"></param>
		public void Add(Wp_ContentsM item)
		{
			this.WpContents.Items.Add(item);
		}
		#endregion

		/// <summary>
		/// 各記事の形態素解析
		/// </summary>
		public void AnalysisMeCab(bool delimita)
		{
			foreach (var tmp in this.WpContents)
			{
				tmp.SetTopDeilimita(delimita);
				tmp.UseMecab();
			}
		}

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

		#region 記事に対しMeCabNodesの取得
		/// <summary>
		/// 記事に対しMeCabNodesの取得
		/// </summary>
		/// <param name="contents">記事</param>
		/// <returns>MeCabNodes</returns>
		public static ModelList<MeCab.MeCabNode> GetMeCabNodesForAllContents(string contents)
		{
			ModelList<MeCab.MeCabNode> ret = new ModelList<MeCabNode>();
			MeCabParam mp = new MeCabParam();
			var tagger = MeCabTagger.Create();

			List<string> noun = new List<string>();

			// 記事毎の内容をMeCabで分析
			foreach (var node in tagger.ParseToNodes(contents))
			{
				if (0 < node.CharType)
				{
					ret.Items.Add(node);
				}
			}

			return ret;
		}
		#endregion

		#region 名詞の頻出順に並べて返却する
		/// <summary>
		/// 名詞の頻出順に並べて返却する
		/// </summary>
		/// <returns>名詞リスト（頻出度順）</returns>
		public List<KeyValuePair<string, int>> GetNounRank()
		{
			string allcontents = GetAllContents();
			var mecab_nodes = GetMeCabNodesForAllContents(allcontents);
			List<string> noun = new List<string>();
			foreach (var mecab_node in mecab_nodes)
			{
				// Featureの内容を分解（品詞を限定する）
				var tmp = mecab_node.Feature.Split(",");

				if (tmp.ElementAt(0).Equals("名詞") && tmp.ElementAt(1).Equals("一般"))
				{
					noun.Add(mecab_node.Surface);
				}
			}

			var dict = new Dictionary<string, int>();

			foreach (var noun_tmp in noun)
			{
				if (dict.ContainsKey(noun_tmp))
					dict[noun_tmp]++;
				else
					dict[noun_tmp] = 1;
			}
			var ret = (from x in dict
					   orderby x.Value descending
					   select x).ToList<KeyValuePair<string, int>>();

			return ret;
		}
		#endregion

		#region カテゴリ設定数のセット
		/// <summary>
		/// カテゴリ設定数のセット
		/// </summary>
		public void SetCategoryCount()
		{
			int count = 0;
			foreach (var tmp in this.WpContents.Items)
			{
				if (!string.IsNullOrEmpty(tmp.Category))
				{
					count++;
				}
			}
			this.CategoryCount = count;
		}
		#endregion

		#region 未分類数
		/// <summary>
		/// 未分類数
		/// </summary>
		public int NoCategory
        {
			get
			{
				return this.ContentsCount - this.CategoryCount;
			}
		}
		#endregion

		#region トップの名詞カウント(重複なし）[TopNounCount]プロパティ
		/// <summary>
		/// トップの名詞カウント(重複なし）[TopNounCount]プロパティ
		/// </summary>
		public int TopNounCount
		{
			get
			{
				return (from x in this.WpContents.Items
						select x.TopNoun).Distinct().Count();
			}

		}
		#endregion

		#region カテゴリ設定数[CategoryCount]プロパティ
		/// <summary>
		/// カテゴリ設定数[CategoryCount]プロパティ用変数
		/// </summary>
		int _CategoryCount = 0;
		/// <summary>
		/// カテゴリ設定数[CategoryCount]プロパティ
		/// </summary>
		public int CategoryCount
		{
			get
			{
				return _CategoryCount;
			}
			set
			{
				if (!_CategoryCount.Equals(value))
				{
					_CategoryCount = value;
					NotifyPropertyChanged("CategoryCount");
					NotifyPropertyChanged("NoCategory");
				}
			}
		}
		#endregion

		#region コンテンツ数のセット
		/// <summary>
		/// コンテンツ数のセット
		/// </summary>
		public void SetContentsCount()
		{
			this.ContentsCount = this.WpContents.Items.Count;
		}
		#endregion

		#region 記事数[ContentsCount]プロパティ
		/// <summary>
		/// 記事数[ContentsCount]プロパティ用変数
		/// </summary>
		int _ContentsCount = 0;
		/// <summary>
		/// 記事数[ContentsCount]プロパティ
		/// </summary>
		public int ContentsCount
		{
			get
			{
				return _ContentsCount;
			}
			set
			{
				if (!_ContentsCount.Equals(value))
				{
					_ContentsCount = value;
					NotifyPropertyChanged("ContentsCount");
					NotifyPropertyChanged("NoCategory");
					NotifyPropertyChanged("TopNounCount");
				}
			}
		}
		#endregion


	}
}
