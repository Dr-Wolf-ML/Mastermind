using System;
using System.Collections.Generic;

namespace Mastermind
{
    public class ConsoleIo
    {
        // no constructor needed
        
        public string ReadKey()
        {
            var key = Console.ReadKey(true).KeyChar.ToString();
            return key;
        }

        public bool NewGame()
        {
            return ReadKey() == "y" ? true : false ;
        }

        public List<int> PickColours(int coloursToPick, Dictionary<int, string> colourRange)
        {
            var masterMind = new MasterMind();
            var messages = new Messages();

            List<int> playerPick = new List<int>();
            string keyPressed;
            int validKey;

            // pick any 4 numbers between 1 - 6
            while (playerPick.Count < coloursToPick)
            {
                keyPressed = ReadKey();
                
                if (keyPressed == "x")
                {
                    masterMind.Exit();
                }

                int.TryParse(keyPressed, out validKey);

                if (validKey >= 1 && validKey <= colourRange.Count)
                {
                    playerPick.Add(validKey);
                    
                    messages.ShowProgressivePlayerInput(playerPick, colourRange, coloursToPick);
                }
            }

            return playerPick;
        }
        public bool PlayAgain()
        {
            return ReadKey() == "y" ? true : false ;
        }
    }
}