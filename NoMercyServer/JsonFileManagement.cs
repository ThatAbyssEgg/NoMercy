using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NoMercyServer
{
    public class JsonFileManagement
    {
        public static void Save(string jsonStr, string charToken)
        {
            string path = Path.GetFullPath(@$"profiles\{charToken}.json");  
            TextWriter writer = new StreamWriter(path);
            writer.Write(jsonStr);
            writer.Close();

        }
        public static PlayerCharacter Load(string charToken)
        {
            string path = Path.GetFullPath(@$"profiles\{charToken}.json");
            string jsonStr = File.ReadAllText(path);

            PlayerCharacter? playerData = JsonSerializer.Deserialize<PlayerCharacter>(jsonStr);
            return playerData;
        }
    }
}
