using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel m;

        public LoginPage()
        {
            InitializeComponent();
            
            if(DependencyService.Get<InterfaceLogin>().getToken() != "") //TODO move this to loginViewModel
            {
                Navigation.PushAsync(new MainPage());
            }
            m = new LoginViewModel();
        }


        public void registerButton(object sender, EventArgs e)
        {
            m.setPage(typeof(NewUserDialog), NavigateTo);// TODO: set all the necessary pages only once to improve performance
            m.GoToCommand.Execute(typeof(NewUserDialog));
        }
        

        public async void signupButton(object sender, EventArgs e)
        {

            m.setPage(typeof(MainPage), NavigateTo); // TODO: set all the necessary pages only once to improve performance
            string msg = await m.singUp();
            if (LoginInformation.getInstance().getIsLogged() == true)
            {
                m.GoToCommand.Execute(typeof(MainPage));
            }
            else
            {
                await DisplayAlert("Error", msg, "ok");
            }
        }

        public async void NavigateTo(Type pageType)
        {
            // Get all the constructors of the page type.
            IEnumerable<ConstructorInfo> constructors =
                    pageType.GetTypeInfo().DeclaredConstructors;

            foreach (ConstructorInfo constructor in constructors)
            {
                // Check if the constructor has no parameters.
                if (constructor.GetParameters().Length == 0)
                {
                    // If so, instantiate it, and navigate to it.
                    Page page = (Page)constructor.Invoke(null);
                    await this.Navigation.PushAsync(page);
                    break;
                }
            }


        }
    }
}
