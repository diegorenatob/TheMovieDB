using Prism.Commands;
using System;
using Prism.Navigation;
using Prism.Services;
using Standard.Domain;
using Standard.Repositories;

namespace TheMovieDB.ViewModels
{
    public class MoviePageViewModel : ViewModelBase
    {
        #region properties
        public int MovieID;

        private Movie _movie;
        public Movie Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        private string _genres;
        public string Genres
        {
            get { return _genres; }
            set { SetProperty(ref _genres, value); }
        }

        public DelegateCommand GobackCommand { get; set; }

        private IMovieRepository _movieRepository;
        private INavigationService _navigationService;

        #endregion

        #region ctor
        public MoviePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IMovieRepository movieRepository)
            : base(navigationService, dialogService)
        {
            _movieRepository = movieRepository;
            _navigationService = navigationService;
            GobackCommand = new DelegateCommand(Goback);



        }
        #endregion

        #region methods
        private async void Goback()
        {
            await _navigationService.GoBackAsync();

        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var id = parameters.GetValue<string>($"ID");
            MovieID = Convert.ToInt32(id);
            Movie = new Movie();
            LoadMovie();

        }

        private async void LoadMovie()
        {
            try
            {
                Movie = await _movieRepository.GetMovie(MovieID, "en-US");

                foreach (Genre genre in Movie.genres)
                {
                    Genres = Genres + genre.name + "\n";
                }

                if (!string.IsNullOrEmpty(Genres))
                    Genres = Genres.Substring(0, Genres.Length - 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await DialogService.DisplayAlertAsync("Error", e.Message, "Ok");

            }

        }
#endregion


    }
}
