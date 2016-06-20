using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
	public partial class NewGamePage : ContentPage
	{
        private ServerOperationManager<ListGamesData> serverOperations;
        private const string DEFAULT_GAME_IMG = "https://drive.google.com/uc?id=0BzpgCnXmnQZmR1ZCcFhLZTJZdjA";
        public NewGamePage ()
		{
			InitializeComponent ();
            serverOperations = new ServerOperationManager<ListGamesData>();
		}

        async void addNewGame(object sender, EventArgs e)
        {
            ListGamesData newGame = new ListGamesData();
            try
            {
                if (gameName.Text != null)
                {

                    newGame.game_name = gameName.Text;
                    newGame.players = new List<Player>();
                    //Check if the user selected his own image, in case of not
                    //set a default image
                    if (imageUrl.Text == null)
                    {
                        newGame.image_url = DEFAULT_GAME_IMG;
                    }
                    else
                    {
                        newGame.image_url = imageUrl.Text;
                    }
                    //TODO
                    //Encapsulate this code in a class, it is frecuenlty use in another classes
                    Id id = new Id();
                    List<ListGamesData> key = await serverOperations.getLatestId("games");
                    var orderList = key.OrderByDescending(ListGamesData => ListGamesData._id.oid).ToList();

                    id.oid = (int.Parse(orderList[0]._id.oid) + 1).ToString();
                    newGame._id = id;
                    await serverOperations.addDocument("games", newGame, true);
                    await DisplayAlert("", "New game added", "ok");
                    await Navigation.PopAsync(true);
                    await Navigation.PushAsync(new MainPage(), true);
                }
                else
                {
                    await DisplayAlert("Error", "You need to write a Game Name", "ok");
                }
            }
            catch (Exception err) {
               await DisplayAlert("Error", "An error happend triyng to add the new Game","ok");
            }


        }

    }
}
