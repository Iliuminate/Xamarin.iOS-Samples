using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foundation;
using UIKit;

namespace SearchBarWithTableView
{
	public class TableSource : UITableViewSource
	{
		private List<TableItem> tableItems = new List<TableItem>();
		private List<TableItem> searchItems = new List<TableItem>();
		protected string cellIdentifier = "TableCell";

		public TableSource(List<TableItem> items)
		{
			this.tableItems = items;
			this.searchItems = items;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return searchItems.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);


			var cellStyle = UITableViewCellStyle.Default;

			// if there are no cells to reuse, create a new one
			if (cell == null)
			{
				cell = new UITableViewCell(cellStyle, cellIdentifier);
			}

			cell.TextLabel.Text = searchItems[indexPath.Row].Title;
			cell.ImageView.Image = UIImage.FromFile("Images/" + searchItems[indexPath.Row].ImageName);

			return cell;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public void PerformSearch(string searchText)
		{
			searchText = searchText.ToLower();
			this.searchItems = tableItems.Where(x => x.Title.ToLower().Contains(searchText)).ToList();
		}
	}
}

