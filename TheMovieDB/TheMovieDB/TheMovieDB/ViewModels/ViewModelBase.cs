using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace TheMovieDB.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService DialogService { get; private set; }
        protected INavigationParameters navigationParameters { get; private set; }



        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }
        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService,INavigationParameters parameters)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            navigationParameters = parameters;
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public string AppName
        {
            get { return App.Current.Properties["AppName"] as string; }
        }


        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
