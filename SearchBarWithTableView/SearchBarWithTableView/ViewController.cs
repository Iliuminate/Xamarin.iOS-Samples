using System;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;

namespace SearchBarWithTableView
{
	public partial class ViewController : UIViewController
	{
		UITableView table;
		TableSource tableSource;
		List<TableItem> tableItems;
		UISearchBar searchBar;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//Declare the search bar and add it to the header of the table
			searchBar = new UISearchBar();
			searchBar.SizeToFit();
			searchBar.AutocorrectionType = UITextAutocorrectionType.No;
			searchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
			searchBar.TextChanged += (sender, e) =>
			{
				//this is the method that is called when the user searches
				searchTable();
			};

			Title = "SearchBarWithTableView Sample";
			table = new UITableView(new CGRect(0, 20, View.Bounds.Width, View.Bounds.Height - 20));
			//table.AutoresizingMask = UIViewAutoresizing.All;
			tableItems = new List<TableItem>();

			tableItems.Add(new TableItem("Vegetables") { ImageName = "Vegetables.jpg" });
			tableItems.Add(new TableItem("Fruits") { ImageName = "Fruits.jpg" });
			tableItems.Add(new TableItem("Flower Buds") { ImageName = "Flower Buds.jpg" });
			tableItems.Add(new TableItem("Legumes") { ImageName = "Legumes.jpg" });
			tableItems.Add(new TableItem("Tubers") { ImageName = "Tubers.jpg" });
			tableSource = new TableSource(tableItems);
			table.Source = tableSource;
			table.TableHeaderView = searchBar;
			Add(table);
		}

		private void searchTable()
		{
			//perform the search, and refresh the table with the results
			tableSource.PerformSearch(searchBar.Text);
			table.ReloadData();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}