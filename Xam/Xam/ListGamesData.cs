using System;
using System.Collections.Generic;
using System.Text;

namespace Xam
{
    public class ListGamesData
    {
        public Id _id { get; set; }
        public string game_name { get; set; }
        public string image_url { get; set; }
        public List<Player> players { get; set; }

    }
    public class Player
    {
        public string usr_name { get; set; }
        public int score { get; set; }
        public string result { get; set; }
    }
}
