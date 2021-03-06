﻿using Prism;
using Prism.Ioc;
using Standard.Infrastructure;
using Standard.Repositories;
using TheMovieDB.ViewModels;
using TheMovieDB.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TheMovieDB
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Current.Properties["NavigationService"] = NavigationService;
            Current.Properties["AppName"] = "THe Movie DB";
           
            await NavigationService.NavigateAsync("NavigationPage/MovieListPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Registering Views vs ViewModels
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MovieListPage, MovieListPageViewModel>();



            //Registering Repositories
            containerRegistry.Register(typeof(IMovieRepository), typeof(MovieRepository));

            containerRegistry.RegisterForNavigation<MoviePage, MoviePageViewModel>();
        }
    }
}
