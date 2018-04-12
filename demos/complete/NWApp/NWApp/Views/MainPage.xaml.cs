﻿using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NWApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) => this.ViewModel = e.NewValue as ViewModels.MainPageViewModel;
        }

        public ViewModels.MainPageViewModel ViewModel { get; set; }
    }
}
