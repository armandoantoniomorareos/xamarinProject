using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Xam
{
    class LoginViewModel : ContentPage, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        ServerOperationManager<LoginData> serverOperations;
        string usr, password;
        public ICommand signUpButton { get; set; }
        public ICommand registerButton { get; set; }

        public LoginViewModel()
        {
            signUpButton = new Command(singUp);
            registerButton = new Command(registerNewUsr);
            serverOperations = new ServerOperationManager<LoginData>();
        }


        public async void singUp()
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




        public async void registerNewUsr()
        {
			 await Navigation.PushAsync(new NewUserDialog(), true);
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
