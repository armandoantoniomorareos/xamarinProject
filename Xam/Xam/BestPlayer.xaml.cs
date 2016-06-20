using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
	public partial class BestPlayer : ContentPage
	{
        private ObservableCollection<Player> sortplayers;
        private List<Player> players;
        public BestPlayer(List<Player> players)
        {

            //TODO: show matches won and lost
            InitializeComponent();
            this.players = players;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            sortplayers = new ObservableCollection<Player>(players);
            BestPlayersList.ItemsSource = sortplayers;

        }
    }
}
