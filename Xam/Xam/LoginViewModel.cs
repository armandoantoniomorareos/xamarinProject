using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xam
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ServerOperationManager<LoginData> serverOperations;
        static string usr, password;
        public ICommand signUpButton { get; set; }
        public ICommand registerButton { get; set; }

        public void setPage(Type pageType,
                             Action<Type> gotoExecute)
        {
            this.PageType = pageType;
            this.PageName = pageType.Name;
            this.GoToCommand = new Command<Type>(gotoExecute);

            signUpButton = new Command(async () => await singUp());
            registerButton = new Command(async () => await registerNewUsr());
            serverOperations = new ServerOperationManager<LoginData>();
        }

        public LoginViewModel()
        {
            signUpButton = new Command(async () => await singUp());
            registerButton = new Command(async () => await registerNewUsr());
            serverOperations = new ServerOperationManager<LoginData>();
        }

        public Type PageType { private set; get; }

        public string PageName { private set; get; }

        public ICommand GoToCommand { private set; get; }

        public ICommand BrowseCommand { private set; get; }


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


        public async Task<string> singUp()
        {
            string msn = "";

            if (usr == null || password == null || usr =="" || password == "")
            {

                msn = "All the fields are requiered";
            }
            else
            {
                try
                {
                    List<LoginData> data = await serverOperations.consultInfo("login", "{\"name\":\"" + usr + "\"}");

                    if (data.Count > 0 && data[0].pass.Equals(password))
                    {
                        LoginInformation.getInstance().setIsLogged(true);
                    }
                    else
                    {
                        msn = "User name or password incorrect";
                    }
                }
                catch (Exception err)
                {
                    //Check for intertet coneccion problems
                    msn = "Please, check your internet conection";
                    if (err.Message.Contains("No address associated with hostname"))
                        return msn;
                    else
                    {
                        return err.Message;
                    }
                }

            }
            return msn;
        }
        
        public async Task registerNewUsr()
        {
           // await DisplayAlert("TODO", "Implement Navigation page", "ok");
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