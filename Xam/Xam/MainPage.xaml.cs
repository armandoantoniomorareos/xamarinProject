using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
    public partial class MainPage : ContentPage
    {

        private ObservableCollection<ListGamesData> listOfGames;
        private ServerOperationManager<ListGamesData> serverOperations;
        private List<ListGamesData> data;
        private string itemSelected = null;

        public MainPage()
        {
            InitializeComponent();
            serverOperations = new ServerOperationManager<ListGamesData>();

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            data = await serverOperations.consultInfo("games");
            listOfGames = new ObservableCollection<ListGamesData>(data);
            GamesView.ItemsSource = listOfGames;
            
        }


        async void onTap(object sender, ItemTappedEventArgs e)
        {
            // items = await request.RefreshDataAsync();
            await Navigation.PushAsync(new GameDetails(((ListGamesData)e.Item)));
            // await Navigation.PushAsync(new GameDetails(((ListGamesData)e.Item).game_name, ((ListGamesData)e.Item).players));
        }


        async void addGame(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewGamePage());
        }

        async void logout(object sender, EventArgs e)
        {

        }

        //TODO
        public async void delete(object sender, EventArgs e)
        {
            var menuItem =((MenuItem)sender);
            var id = ((ListGamesData)menuItem.CommandParameter);
           await serverOperations.deleteDocument("games", id._id.oid);
           DisplayAlert("Deleted", "asd", "ok");
        }
    }
}
