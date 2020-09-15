using System;
using System.Collections.Generic;
using System.IO;
using Data;

namespace EscapeMines
{
    public sealed class Game
    {
        private const int configFileLineCount = 5;

        static Game()
        {
        }

        private Game()
        {
        }

        public static Game Instance { get; } = new Game();

        public List<string> ConfigData { get; set; }

        public void LoadConfigDataFromFile(string path, IFileReader fileReader)
        {
            string errorMessage = String.Empty;

            try
            {
                ConfigData = fileReader.ReadLines(path, configFileLineCount);
            }
            catch (FileNotFoundException)
            {
                errorMessage += "The file on the given path does not exist.";
            }
            
        }
    }
}