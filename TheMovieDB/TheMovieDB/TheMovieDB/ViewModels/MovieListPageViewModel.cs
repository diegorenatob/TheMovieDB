using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Standard.Domain;
using Standard.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace TheMovieDB.ViewModels
{
    public class MovieListPageViewModel : ViewModelBase
    {
        #region Properties

        public Command<object> MovieTappedCommand { get; set; }

        private InfiniteScrollCollection<MovieResume> _movieResumeList;
        public InfiniteScrollCollection<MovieResume> MovieResumeList
        {
            get { return _movieResumeList; }
            set
            {
                SetProperty(ref _movieResumeList, value);
            }
        }

        private int Pages;
        private const int PageSize = 20;

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private IMovieRepository _movieRepository;
        private INavigationService _navigationService;


        #endregion

        #region Ctor
        public MovieListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IMovieRepository movieRepository) 
            : base(navigationService, dialogService)
        {
            _movieRepository = movieRepository;
            MovieResumeList = new InfiniteScrollCollection<MovieResume>()
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    int page;

                    page = MovieResumeList.Count / PageSize;

                    var items = await LoadMoreMovies(page,"en-US" ); 
                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return (MovieResumeList.Count / PageSize < Pages);
                }
            };
            _navigationService = navigationService;
            LoadMovies("en-US");
            MovieTappedCommand=new Command<object>(MovieTapped);


        }

        #endregion

        #region methods

        private async void LoadMovies(string language)
        {
            var moviesList = await _movieRepository.GetMovies(1, language);
            Pages = moviesList.total_pages;
            if (moviesList.movies != null)
            {
                InfiniteScrollCollection<MovieResume> result =
                    new InfiniteScrollCollection<MovieResume>(moviesList.movies as List<MovieResume>);
                MovieResumeList.AddRange(result);
            }
        }

        public async Task<InfiniteScrollCollection<MovieResume>> LoadMoreMovies(int page,string language)
        {
            var moviesList = await _movieRepository.GetMovies(page, language);
            if (moviesList.movies != null)
            {
                InfiniteScrollCollection<MovieResume> result =
                    new InfiniteScrollCollection<MovieResume>(moviesList.movies as List<MovieResume>);
                return result;
            }

            return null;
        }

        private async void MovieTapped(object Movie)
        {
            if (Movie == null) return;
            var mov = Movie as MovieResume;
            var navigationParams = new NavigationParameters();
            if (mov != null) navigationParams.Add("ID", mov.id);
           await _navigationService.NavigateAsync("MoviePage",navigationParams);

          
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
