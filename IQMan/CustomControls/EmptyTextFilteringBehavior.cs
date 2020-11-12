using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace IQMan.CustomControls
{
   	public class EmptyTextFilteringBehavior : FilteringBehavior
	{
		public override IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
		{
			if (string.IsNullOrWhiteSpace(searchText))
			{
				return items.OfType<object>().Where(x => !escapedItems.Contains(x));
			}
			else
			{
				return base.FindMatchingItems(searchText, items, escapedItems, textSearchPath, textSearchMode);
			}
		}
	}
}
