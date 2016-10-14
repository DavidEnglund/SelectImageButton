using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SelectableControls;

using Xamarin.Forms;

namespace roundControl
{
    public class App : Application
    {
        public App()
        {
            SelectImageButton showOff = new SelectImageButton();
           // showOff.WidthRequest = 10;
          //  showOff.HeightRequest = 50;
            showOff.HorizontalOptions = LayoutOptions.Center;
            showOff.Padding = 8;
           // showOff.Children.Add(new Label { Text = "out of bounds this text is to test the circular bounds", TextColor = Color.Black });
            //another to test that is a copy of the first
            SelectImageButton showOff2 = new SelectImageButton();
           // showOff2.Children.Add(new Label { Text = "out of bounds this text is to test the circular bounds", TextColor = Color.Black });
           // showOff2.HeightRequest = 70;
            showOff2.Padding = 100;
            showOff2.SelectedImage = "alex.png";
            showOff2.UnselectedImage = "alex.png";
            SelectImageButton showOff3 = new SelectImageButton();
            // showOff3.Children.Add(new Label { Text = "out of bounds this text is to test the circular bounds" ,TextColor = Color.Black});
            // showOff3.HeightRequest = 50;
            showOff3.Padding = 0;
            showOff3.Clicked += showoff3_clicked;
            // now to test the damned thing in a group - this will break in interesting and frustrating ways
            // first way to break - clicking the 1st button unselects the 2nd but the 2nd wont unselect the first
            // 2nd - the image does not display
            Button test = new Button();
            test.Clicked += showoff3_clicked;
            test.Text = "hello IOS";
            SelectButtonGroup greenGroup = new SelectButtonGroup();
            greenGroup.addButton(showOff);
            greenGroup.addButton(showOff2);
            greenGroup.addButton(showOff3);
            // and an ungrouped button for testing
            SelectImageButton unowned = new SelectImageButton();
            unowned.SelectedBorderColor = Color.Green;
            unowned.SelectedBorderWidth = 3;
            unowned.UnselectedBorderColor = Color.Black;
            unowned.UnselectedBorderWidth = 1;
            unowned.UnselectedBackgroundColor = Color.Blue;
            unowned.SelectedBackgroundColor = Color.Green;
            // testing for the damned ios click
            Label lbltest = new Label();
            lbltest.Text = "label";
            lbltest.BackgroundColor = Color.Purple;

            TapGestureRecognizer testtap = new TapGestureRecognizer();
            testtap.Tapped += showoff3_clicked;
            unowned.GestureRecognizers.Add(testtap);
            lbltest.GestureRecognizers.Add(testtap);
            // The root page of your application
            StackLayout layouttest;
            MainPage = new ContentPage
            {
                Content = layouttest=new StackLayout
                {
                    
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                        new Image {Source = "alex.png" },
                        showOff,showOff2,showOff3,unowned,test,lbltest
                    }
                }
            };
            MainPage.BackgroundColor = Color.Red;
           // layouttest.Children.Add(showOff2);
            showOff2.HeightRequest= layouttest.WidthRequest * 0.1;
            showOff2.WidthRequest = layouttest.WidthRequest * 0.1;
            greenGroup.Selected = unowned;
        }

        private void showoff3_clicked(object sender, EventArgs e)
        {
            MainPage.Navigation.PushModalAsync(new Page1());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
