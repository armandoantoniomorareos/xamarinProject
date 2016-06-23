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
    }
}
