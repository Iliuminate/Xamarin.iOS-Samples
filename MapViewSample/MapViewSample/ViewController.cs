using System;
using MapKit;
using UIKit;
using CoreLocation;
using CoreGraphics;

namespace MapViewSample
{
	public partial class ViewController : UIViewController
	{

		MKMapView mapView;
		UISegmentedControl mapTypeSelection;
		CLLocationManager location = new CLLocationManager();

		public ViewController (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			Title = "Map View Sample";

			mapView = new MKMapView(View.Bounds);	
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			View.AddSubview(mapView);

			// Request permission to access device location
			location.RequestWhenInUseAuthorization();
			// Indicates User Location
			mapView.ShowsUserLocation = true;

			//This snippet lets you toggle between Map Types
			int typesWidth=260, typesHeight=30, distanceFromBottom=60;
			mapTypeSelection = new UISegmentedControl(new CGRect((View.Bounds.Width-typesWidth)/2, View.Bounds.Height-distanceFromBottom, typesWidth, typesHeight));
			mapTypeSelection.InsertSegment("Standard", 0, false);
			mapTypeSelection.InsertSegment("Satellite", 1, false);
			mapTypeSelection.InsertSegment("Hybrid", 2, false);
			mapTypeSelection.SelectedSegment = 0; // Standard is the default selection
			mapTypeSelection.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
			mapTypeSelection.ValueChanged += (s, e) => {
				switch(mapTypeSelection.SelectedSegment) {
				case 0:
					mapView.MapType = MKMapType.Standard;
					break;
				case 1:
					mapView.MapType = MKMapType.Satellite;
					break;
				case 2:
					mapView.MapType = MKMapType.Hybrid;
					break;
				}
			};

			View.AddSubview(mapTypeSelection);

			mapView.DidUpdateUserLocation += (sender, e) => {
				if (mapView.UserLocation != null) {
					CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
					MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coords.Latitude));
					mapView.Region = new MKCoordinateRegion(coords, span);
				}
			};

			if (!mapView.UserLocationVisible) {
				// User denied permission or device doesn't have GPS/location ability
				// create our location and zoom to Chicago
				CLLocationCoordinate2D coords = new CLLocationCoordinate2D(41.8781, -87.6298); // Chicago
				MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coords.Latitude));
				// set the coords and zoom on the map
				mapView.Region = new MKCoordinateRegion(coords, span);
			}
		}

		// Helper method to calculate the area to show on the map
		public double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0; // in miles
			double radianToDegree = 180.0/Math.PI;
			return (miles/earthRadius) * radianToDegree;
		}

		// Helper method to calculate the area to show on the map
		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0; // in miles
			double degreeToRadian = Math.PI/180.0;
			double radianToDegree = 180.0/Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreeToRadian);
			return (miles / radiusAtLatitude) * radianToDegree;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

