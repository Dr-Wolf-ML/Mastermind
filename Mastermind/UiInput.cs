using System;
using System.Collections.Generic;

namespace Mastermind
{
    public class UiInput
    {
        // no constructor needed

        private string ReadKey()
        {
            var key = Console.ReadKey(true).KeyChar.ToString();
            return key;
        }

        public bool NewGame()
        {
            return ReadKey() == "y" ? true : false;
        }

        public List<int> PickColours(int coloursToPick, Dictionary<int, string> colourRange)
        {
            var masterMind = new MasterMind();
            var messages = new UiOutput();

            var playerPick = new List<int>();

            // pick any 4 numbers between 1 - 6
            while (playerPick.Count < coloursToPick)
            {
                var keyPressed = ReadKey();

                if (keyPressed == "x")
                {
                    MasterMind.Exit();
                }

                int.TryParse(keyPressed, out var validKey);

                if (validKey < 1 || validKey > colourRange.Count) continue;
                playerPick.Add(validKey);

                messages.ShowProgressivePlayerInput(playerPick, colourRange, coloursToPick);
            }

            return playerPick;
        }

        public bool PlayAgain()
        {
            return ReadKey() == "y" ? true : false;
        }
    }
}