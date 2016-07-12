using System;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;

namespace TableViewSample
{
	public partial class ViewController : UIViewController
	{
		UITableView table;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			Title = "Table View Sample";
			table = new UITableView(new CGRect(0, 20, View.Bounds.Width, View.Bounds.Height - 20));
			//table.AutoresizingMask = UIViewAutoresizing.All;
			string[] tableItems = new string[] { "Apple", "Motorola", "Microsoft", "Google", "Nokia" };
			table.Source = new TableSource(tableItems);

			table.SeparatorColor = UIColor.Blue;
			table.SeparatorStyle = UITableViewCellSeparatorStyle.DoubleLineEtched;
			table.SeparatorInset.InsetRect(new CGRect(4, 4, 150, 2));
			Add(table);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

