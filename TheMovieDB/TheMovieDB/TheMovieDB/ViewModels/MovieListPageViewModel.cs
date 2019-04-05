using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                SetProperty(ref _search, value);
                LoadMovies("en-US");
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
                    try
                    {
                        IsBusy = true;
                        int page;

                        page = MovieResumeList.Count / PageSize;

                        var items = await LoadMoreMovies(page, "en-US");
                        IsBusy = false;

                        // return the items that need to be added
                        return items;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                },
                OnCanLoadMore = () =>
                {
                    return (MovieResumeList.Count / PageSize < Pages);
                }
            };
            _navigationService = navigationService;
            LoadMovies("en-US");
            MovieTappedCommand = new Command<object>(MovieTapped);


        }

        #endregion

        #region methods

        private async void LoadMovies(string language)
        {
            try
            {
                MovieResumeList.Clear();
                if (string.IsNullOrEmpty(Search))
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
                else
                {
                    var moviesList = await _movieRepository.SearchMovies(Search, 1, language);
                    Pages = moviesList.total_pages;
                    if (moviesList.movies != null)
                    {
                        InfiniteScrollCollection<MovieResume> result =
                            new InfiniteScrollCollection<MovieResume>(moviesList.movies as List<MovieResume>);
                        MovieResumeList.AddRange(result);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               await  DialogService.DisplayAlertAsync("Error",e.Message, "Ok");
            }

        }

        public async Task<InfiniteScrollCollection<MovieResume>> LoadMoreMovies(int page, string language)
        {
            try
            {
                if (string.IsNullOrEmpty(Search))
                {

                    var moviesList = await _movieRepository.GetMovies(page, language);
                    if (moviesList.movies != null)
                    {
                        InfiniteScrollCollection<MovieResume> result =
                            new InfiniteScrollCollection<MovieResume>(moviesList.movies as List<MovieResume>);
                        return result;
                    }
                }
                else
                {
                    var moviesList = await _movieRepository.SearchMovies(Search, page, language);
                    Pages = moviesList.total_pages;
                    if (moviesList.movies != null)
                    {
                        InfiniteScrollCollection<MovieResume> result =
                            new InfiniteScrollCollection<MovieResume>(moviesList.movies as List<MovieResume>);
                        return result;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await DialogService.DisplayAlertAsync("Error", e.Message, "Ok");

            }
            return null;

        }

        private async void MovieTapped(object Movie)
        {
            try
            {
                if (Movie == null) return;
                var mov = Movie as MovieResume;
                var navigationParams = new NavigationParameters();
                if (mov != null) navigationParams.Add("ID", mov.id);
                await _navigationService.NavigateAsync("MoviePage", navigationParams);
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
