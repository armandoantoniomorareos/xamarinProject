using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xam
{
    public class LoginViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ServerOperationManager<LoginData> serverOperations;
        string usr, password;
        public ICommand signUpButton { get; set; }
        public ICommand registerButton { get; set; }

        public LoginViewModel()
        {
            signUpButton = new Command(async () => await singUp());
            registerButton = new Command(async () => await registerNewUsr());
            serverOperations = new ServerOperationManager<LoginData>();
        }


        public string userName
        {
            set
            {
                if (usr != value)
                {
                    usr = value;
                    OnPropertyChanged("userName");
                }
            }
            get
            {
                return usr;
            }

        }

        public string pass
        {

            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("pass");
                }
            }

            get
            {
                return password;
            }
        }


        public async Task singUp()
        {
            if (usr == null || password == null)
            {
                DisplayAlert("Erro", "All the fields are requiered", "ok");
            }
            else
            {
                try
                {
                    List<LoginData> data = await serverOperations.consultInfo("login", "{\"name\":\"" + usr + "\"}");

                    if (data.Count > 0 && data[0].pass.Equals(password))
                    {
                        LoginInformation.getInstance().setIsLogged(true); //Loging success
                        await DisplayAlert("TODO", "Login success TODO: Implement Navigation page", "ok");
                        await Navigation.PushModalAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error while trying to loggin, password or user name incorrect", "ok");
                    }
                }
                catch (Exception err)
                {
                    //Check for intertet coneccion problems
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
        
        public async Task registerNewUsr()
        {
            await DisplayAlert("TODO", "Implement Navigation page", "ok");
            /*NavigationPage nav = new NavigationPage(new LoginPage());
            await nav.PushAsync(new MainPage());*/
        }
       
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}