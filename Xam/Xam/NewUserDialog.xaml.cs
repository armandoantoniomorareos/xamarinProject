using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
	public partial class NewUserDialog : ContentPage
	{
        ServerOperationManager<LoginData> serverOperations;
		public NewUserDialog ()
		{
			InitializeComponent ();
            serverOperations = new ServerOperationManager<LoginData>();
		}


        async void registerNewUser(object sender, EventArgs e)
        {
            if (userName.Text == null || pass.Text == null)
            {
                DisplayAlert("Erro", "All the fields are requiered", "ok");
            }
            else
            {
                try
                {
                LoginData usr = new LoginData();
                usr.name = userName.Text;
                usr.pass = pass.Text;
                Id id = new Id();
                List<LoginData> key = await serverOperations.getLatestId("login");
                var orderList = key.OrderByDescending(LoginData => LoginData._id.oid).ToList();
                id.oid = (int.Parse(orderList[0]._id.oid) + 1).ToString();
                usr._id = id;

                if(await serverOperations.addDocument("login", usr, true))
                    {
                        LoginInformation.getInstance().setIsLogged(true);
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        LoginInformation.getInstance().setIsLogged(false);
                        DisplayAlert("Error", "Error trying to crear new user, plese try again later", "ok");
                    }
                
                    
                }
                catch (Exception err)
                {
                    //Cechk for intertet coneccion problems
                    string msn = "Please, check your internet conection";
                    if (err.Message.Contains("No address associated with hostname"))
                        await DisplayAlert("Error", msn, "ok");
                    else
                    {
                        await DisplayAlert("Error", err.Message, "ok");
                    }
                }
            }
            
        }

    }
}
