﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ArtekSoftware.Conference.Mobile.SL.Views
{
  public partial class Sessions : Page
  {
    public Sessions()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
      string baseUrl = "http://conference.azurewebsites.net/api/";
      var client = new RemoteData.Shared.RemoteData(baseUrl);
      client.GetConferences(conferences =>
      {
        Deployment.Current.Dispatcher.BeginInvoke(() =>
        {
          DataContext = conferences;
          //Loading.Visibility = Visibility.Collapsed;
        });
      });
    }

    private void ConferenceSelected(object sender, SelectionChangedEventArgs e)
    {
      var conference = (RemoteData.Shared.Conference)e.AddedItems[0];
      MessageBox.Show(conference.Name, "Full Conference", MessageBoxButton.OK);
    }

  }
}