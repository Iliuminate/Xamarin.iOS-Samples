using System;
using UIKit;

namespace SearchBarWithTableView
{
	public class TableItem
	{
		public string Title { get; set; }

		public string ImageName { get; set; }

		public UITableViewCellStyle CellStyle
		{
			get { return cellStyle; }
			set { cellStyle = value; }
		}
		protected UITableViewCellStyle cellStyle = UITableViewCellStyle.Default;

		public UITableViewCellAccessory CellAccessory
		{
			get { return cellAccessory; }
			set { cellAccessory = value; }
		}
		protected UITableViewCellAccessory cellAccessory = UITableViewCellAccessory.None;

		public TableItem() { }

		public TableItem(string title)
		{ Title = title; }

	}
}

