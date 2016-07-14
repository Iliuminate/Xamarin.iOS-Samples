using System;
using System.Collections.Generic;
using System.IO;
using Foundation;
using UIKit;

namespace PullToRefresh
{
	public class TableSource : UITableViewSource
	{
		List<TableItem> tableItems;
		protected string cellIdentifier = "TableCell";

		public TableSource(List<TableItem> items)
		{
			tableItems = items;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Count;
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

			cell.TextLabel.Text = tableItems[indexPath.Row].Title;
		    cell.ImageView.Image = UIImage.FromFile("Images/" + tableItems[indexPath.Row].ImageName);

			return cell;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}


		#region -= editing methods =-

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Insert:
					tableItems.Insert(indexPath.Row, new TableItem("(inserted)"));
					tableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					break;

				case UITableViewCellEditingStyle.None:
					Console.WriteLine("CommitEditingStyle:None called");
					break;
			}
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true; 
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing)
			{
				if (indexPath.Row == tableView.NumberOfRowsInSection(0) - 1)
					return UITableViewCellEditingStyle.Insert;
				else
					return UITableViewCellEditingStyle.Delete;
			}
			else  // not in editing mode, enable swipe-to-delete for all rows
				return UITableViewCellEditingStyle.Delete;
		}

		public void WillBeginTableEditing(UITableView tableView)
		{
			tableView.BeginUpdates();

			tableView.InsertRows(new NSIndexPath[] {
					NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0)
				}, UITableViewRowAnimation.Fade);
			tableItems.Add(new TableItem("(add new)"));

			tableView.EndUpdates();
		}

		#endregion
	}
}

