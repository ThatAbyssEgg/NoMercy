using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercyServer
{
    public class PlayerCharacter
    {
        public PlayerCharacter() { }
        public PlayerCharacter(string charName, string charToken, string charClass)
        {
            Name = charName;
            Token = charToken;
            Class = charClass;
        }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Class { get; set; }
    }
}
