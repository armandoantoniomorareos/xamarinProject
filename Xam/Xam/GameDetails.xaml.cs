using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
	public partial class GameDetails : ContentPage
	{
        private ObservableCollection<Player> listOfResults;
        private ServerOperationManager<ListGamesData> serverOperations;
        private List<Player> players;
        private ListGamesData gameInfo = null;

        public GameDetails(ListGamesData data)
        {
            InitializeComponent();
            ScoreListView.ItemsSource = listOfResults;
            this.players = new List<Player>(data.players);
            gameInfo = data;

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listOfResults = new ObservableCollection<Player>(gameInfo.players);
            ScoreListView.ItemsSource = listOfResults;
            
        }


        public async void addNewMatch(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMatchPage(gameInfo));
        }


        async public void bestPlayer(object sender, EventArgs e)
        {

            List<Player> orderList = players.OrderByDescending(Player => Player.score).ToList();
            await Navigation.PushAsync(new BestPlayer(orderList));
        }
    }
}
