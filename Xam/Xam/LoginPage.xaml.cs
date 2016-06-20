using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
	public partial class LoginPage : ContentPage
    {
        ServerOperationManager<LoginData> serverOperations;
        public LoginPage()
        {
            InitializeComponent();
            serverOperations = new ServerOperationManager<LoginData>();
        }


        async void registerButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewUserDialog(), true);
        }

        async void signUpButton(object sender, EventArgs e)
        {


            if (userName.Text == null || pass.Text == null)
            {
                DisplayAlert("Erro", "All the fields are requiered", "ok");
            }
            else
            {
                string name = userName.Text;
                //move strings to Constants file
                //Get user and password from data base
                try
                {
                    List<LoginData> data = await serverOperations.consultInfo("login", "{\"name\":\"" + name + "\"}");

                    if (data.Count > 0 && data[0].pass.Equals(pass.Text))
                    {
                        LoginInformation.getInstance().setIsLogged(true); //Loging success
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error while trying to loggin, password or user name incorrect", "ok");
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
