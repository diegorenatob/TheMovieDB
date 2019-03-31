using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace TheMovieDB.Views
{
    public partial class MovieListPage : ContentPage
    {
        public MovieListPage()
        {
            InitializeComponent();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!TextChangeEnable)
            //    return;

            //var search = sender as SearchBar;
            //string text = search?.Text;
            //if (ItemsSource is ObservableCollection<GenericComboResult>)
            //{
            //    if (_itemSourceFull == null)
            //        _itemSourceFull = ItemsSource as ObservableCollection<GenericComboResult>;

            //    var colecaoFiltrada = new ObservableCollection<GenericComboResult>(_itemSourceFull.Where(x => x.Descricao.ToUpper().Contains(text.ToUpper())).ToList());
            //    ItemsSource = colecaoFiltrada;
            //}

        }


        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            //var search = sender as SearchBar;
            //string text = search?.Text;

            //if (ItemsSource is ObservableCollection<GenericComboResult>)
            //{
            //    if (_itemSourceFull == null)
            //        _itemSourceFull = ItemsSource as ObservableCollection<GenericComboResult>;

            //    var colecaoFiltrada = new ObservableCollection<GenericComboResult>(_itemSourceFull.Where(x => x.Descricao.ToUpper().Contains(text.ToUpper())).ToList());
            //    ItemsSource = colecaoFiltrada;
            //}
        }
    }
}
