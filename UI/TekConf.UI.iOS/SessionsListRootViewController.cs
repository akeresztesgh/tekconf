// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using ConferencesIO.RemoteData.v1;
using ConferencesIO.RemoteData.Dtos.v1;
using ConferencesIO.LocalData.Shared;
using System.IO;
using ConferencesIO.Mappers.IO;

namespace ConferencesIO.UI.iOS
{
	public partial class SessionsListRootViewController : UITableViewController
	{

		private RemoteDataRepository _client;
		private string _baseUrl = "http://conferencesioapi.azurewebsites.net/v1/";
		const string MoveToMapSegueName = "showSessionDetail";

		public string SelectedSessionSlug {
			get;
			set;
		}

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SessionsListRootViewController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var backgroundImage = UIImage.FromBundle(@"images/appview/bg");
			//this.View.BackgroundColor = UIColor.FromPatternImage(backgroundImage);

			_client = new RemoteDataRepository (_baseUrl);

			var loading = new UIAlertView (" Downloading Sessions", "Please wait...", null, null, null);

			loading.Show ();

			var indicator = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.WhiteLarge); 
			indicator.Center = new System.Drawing.PointF (loading.Bounds.Width / 2, loading.Bounds.Size.Height - 40); 
			indicator.StartAnimating (); 
			loading.AddSubview (indicator);

//			_client.GetFullConference("CodeMash-2012", conference => {
//				var connectionString = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "conferences.db");
//				var localDatabase = new LocalDatabase(connectionString);
//				localDatabase.CreateDatabase();
//
//				var conferenceMapper = new ConferenceDtoToConferenceEntityMapper();
//				var conferenceEntity = conferenceMapper.Map(conference);
//
//				var sessionMapper = new SessionDtoToSessionEntityMapper();
//				var sessions = sessionMapper.MapAll(conferenceEntity.Id, conference.sessions.AsEnumerable());
//				//localDatabase.SaveSessions(sessions);
//
//				var speakersMapper = new SessionDtoToSessionEntityMapper();
//				var speakerEntities = speakersMapper.MapAll(conferenceEntity.Id, conference.speakers.AsEnumerable());
//				localDatabase.SaveConference(conferenceEntity, sessions, speakers);
//
//			});
			_client.GetSessions ("CodeMash-2012", sessions => 
			{ 
				InvokeOnMainThread (() => 
				{ 
					TableView.Source = new SessionsTableViewSource (this, sessions); 
					TableView.ReloadData (); 
					loading.DismissWithClickedButtonIndex (0, true); 
				});
			}
			);

			// Perform any additional setup after loading the view, typically from a nib.
			
			//this.TableView.Source = new DataSource (this);
			
			if (!UserInterfaceIdiomIsPhone)
				this.TableView.SelectRow (
					NSIndexPath.FromRowSection (0, 0),
					false,
					UITableViewScrollPosition.Middle
				);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == MoveToMapSegueName) {
				var view = (SessionDetailViewController)segue.DestinationViewController;
				view.SessionSlug = this.SelectedSessionSlug;
				//view.SelectedCity = SelectedCity;
			}
		}

		public class SessionsTableViewSource : UITableViewSource
		{ 
			private readonly IList<SessionsDto> _sessions;
			private const string SessionCell = "SessionCell";
			private SessionsListRootViewController _rootViewController;
			public static SessionDetailViewController _sessionDetailViewController;

			public SessionsTableViewSource (SessionsListRootViewController controller, IList<SessionsDto> sessions)
			{ 
				_rootViewController = controller;
				_sessions = sessions; 
			}

			public override int RowsInSection (UITableView tableView, int section)
			{ 
				return _sessions.Count; 
			}

			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{ 
				return 60; 
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{ 
				var cell = tableView.DequeueReusableCell (SessionCell) ?? new UITableViewCell (UITableViewCellStyle.Subtitle, SessionCell); 
				var session = _sessions [indexPath.Row]; 
				cell.TextLabel.Text = session.title; 
				cell.DetailTextLabel.Text = session.start.ToLocalTime ().ToString (); 
				return cell; 
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{ 
				SessionsDto selectedSession = _sessions [indexPath.Row]; 
				//new UIAlertView ("View Session", selectedSession.title, null, "Ok", null).Show (); 

				if (UserInterfaceIdiomIsPhone) {
					//_sessionDetailViewController = new SessionDetailViewController (selectedSession.slug);
					_rootViewController.SelectedSessionSlug = selectedSession.slug;
					_rootViewController.PerformSegue (MoveToMapSegueName, _rootViewController);
					// Pass the selected object to the new view controller.
					//_rootViewController.NavigationController.PushViewController (
//						_sessionDetailViewController,
//						true
//					);
				} else {
					// Navigation logic may go here -- for example, create and push another view controller.
				}
			} 
		}

		class DataSource : UITableViewSource
		{
			RootViewController controller;

			public DataSource (RootViewController controller)
			{
				this.controller = controller;
			}
			
			// Customize the number of sections in the table view.
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return 1;
			}
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				string cellIdentifier = "Cell";
				var cell = tableView.DequeueReusableCell (cellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
					if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
						cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					}
				}

				// Configure the cell.
				//cell.TextLabel.Text = NSBundle.MainBundle.LocalizedString (
				//	"Detail",
				//	"Detail"
				//);
				return cell;
			}

			/*
			// Override to support conditional editing of the table view.
			public override bool CanEditRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}
			*/
			
			/*
			// Override to support editing the table view.
			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					controller.TableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}
			*/
			
			/*
			// Override to support rearranging the table view.
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{
			}
			*/
			
			/*
			// Override to support conditional rearranging of the table view.
			public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the item to be re-orderable.
				return true;
			}
			*/
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				if (UserInterfaceIdiomIsPhone) {
					var DetailViewController = new SessionDetailViewController ();
					// Pass the selected object to the new view controller.
					controller.NavigationController.PushViewController (
						DetailViewController,
						true
					);
				} else {
					// Navigation logic may go here -- for example, create and push another view controller.
				}
			}
		}



	}
}
