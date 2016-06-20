using System;
using System.Collections.Generic;
using System.Text;

namespace Xam
{
    class LoginInformation
    {
        private static LoginInformation loginInformation;
        private static bool isLogged = false;
        static LoginInformation()
        {

        }

        public static LoginInformation getInstance()
        {
            object lockingObject = new object();

            if (loginInformation == null)
            {
                lock (lockingObject)
                {
                    if (loginInformation == null)
                    {
                        loginInformation = new LoginInformation();
                    }
                }
            }
            return loginInformation;

        }

        public void setIsLogged(bool logged)
        {
            isLogged = logged;
        }
        public bool getIsLogged()
        {
            return isLogged;
        }
    }
}
