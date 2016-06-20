using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
	public partial class NewMatchPage : ContentPage
	{
        private ServerOperationManager<ListGamesData> serverOpertationsManager;
        private ListGamesData item;
        private string resultT = null;
		public NewMatchPage (ListGamesData data)
		{
			InitializeComponent ();
            item = data;
            serverOpertationsManager = new ServerOperationManager<ListGamesData>();

		}

        public async void selectedIndexChanged(object sender, EventArgs e)
        {
            var pickerT = ((Picker)sender);
            resultT = pickerT.Items[ pickerT.SelectedIndex];
        }

        public  async void addNewMatch(Object sender, EventArgs e)
        {
           
            Player p = new Player();
            if (userName.Text == null || score.Text == null || resultT == null)
            {
                DisplayAlert("Erro", "All the information is necessary","ok");
            }
            else
            {
                
                p.usr_name = userName.Text;
                p.score = int.Parse(score.Text);
                p.result = resultT;
                item.players.Add(p);

                if (await serverOpertationsManager.addDocument("games", item, false))
                {
                    //TODO clean field userName, score
                    await DisplayAlert("", "New Match added successfuly", "ok");
                }
                else
                {
                    await DisplayAlert("Error", "Error adding new match", "ok");
                }
            }
        }

	}
}
