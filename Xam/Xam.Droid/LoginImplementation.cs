
using Android.Content;
using System;
using Xam.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(LoginImplementation))]
namespace Xam.Droid
{
    class LoginImplementation : InterfaceLogin
    {
        ISharedPreferences pref;
        public LoginImplementation()
        {
            pref = Xamarin.Forms.Forms.Context.GetSharedPreferences("info.settings", FileCreationMode.Private);
        }
        public string getToken()
        {
            return pref.GetString("pass", string.Empty);
        }

        public void setToken(string value)
        {
            var editor = pref.Edit();
            editor.PutString("pass", value); //encrypt pass
            editor.Apply();
        }
        
    }
}