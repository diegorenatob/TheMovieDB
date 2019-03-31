using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using Standard.Domain;
using Standard.Repositories;

namespace TheMovieDB.ViewModels
{
    public class MovieListPageViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<MovieResume> _movieResumeList;
        public ObservableCollection<MovieResume> MovieResumeList
        {
            get { return _movieResumeList; }
            set
            {
                SetProperty(ref _movieResumeList, value);
            }
        }

        private IMovieRepository _movieRepository;


        #endregion

        #region Ctor
        public MovieListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IMovieRepository movieRepository) 
            : base(navigationService, dialogService)
        {
            _movieRepository = movieRepository;
            MovieResumeList=new ObservableCollection<MovieResume>();
            LoadMovies();

          
        }
        #endregion

        #region methods

        public async void LoadMovies()
        {
            var moviesList = await _movieRepository.GetMovies(1, "en-US");
            if(moviesList.movies!=null)
            MovieResumeList=new ObservableCollection<MovieResume>(moviesList.movies as List<MovieResume>);
        }
        #endregion
    }
}
