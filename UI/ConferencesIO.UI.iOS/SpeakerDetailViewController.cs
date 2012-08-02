// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ConferencesIO.RemoteData.v1;

namespace ConferencesIO.UI.iOS
{
	public partial class SpeakerDetailViewController : UIViewController
	{
		private RemoteDataRepository _client;
		private string _baseUrl = "http://conferencesioapi.azurewebsites.net/v1/";
		public string SpeakerSlug { get; set; }

		public SpeakerDetailViewController () : base()
		{
			
		}

		public SpeakerDetailViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			_client = new RemoteDataRepository (_baseUrl);

			var loading = new UIAlertView ("Downloading Speaker", "Please wait...", null, null, null);

			loading.Show ();

			var indicator = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.WhiteLarge); 
			indicator.Center = new System.Drawing.PointF (loading.Bounds.Width / 2, loading.Bounds.Size.Height - 40); 
			indicator.StartAnimating (); 
			loading.AddSubview (indicator);

			_client.GetSpeaker ("CodeMash-2012", this.SpeakerSlug, speaker => 
			{ 
				InvokeOnMainThread (() => 
				{ 
					//new UIAlertView ("Session", session.title, null, "Ok", null).Show ();
					this.speakerNameLabel.Text = speaker.firstName + " " + speaker.lastName;
					loading.DismissWithClickedButtonIndex (0, true); 
				}
				);
			}
			);

			// Perform any additional setup after loading the view, typically from a nib.
			
			//this.TableView.Source = new DataSource (this);
			
//			if (!UserInterfaceIdiomIsPhone) {
//				this.TableView.SelectRow (
//					NSIndexPath.FromRowSection (0, 0),
//					false,
//					UITableViewScrollPosition.Middle
//				);
//			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var backgroundImage = UIImage.FromBundle(@"images/appview/bg");
			//this.View.BackgroundColor = UIColor.FromPatternImage(backgroundImage);
		}

	}
}
