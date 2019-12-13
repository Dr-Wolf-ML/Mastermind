using System;
using System.Collections.Generic;

namespace Mastermind
{
    public class Messages
    {
        // no constructor needed

        public void NewGame()
        {
            Console.Clear();
            
            Console.WriteLine("New game? y/n");
        }

        public void Bye()
        {
            Console.WriteLine("sGood Bye...\n");
        }

        public void AdviseColoursPicked(List<int> computerColours, Dictionary<int, string> colourRange)
        {
            Console.Write($"The Computer picked: ");

            foreach (var colour in computerColours)
            {
                Console.Write($"  {colourRange[colour]}");
            }

            Console.WriteLine("");
        }

        public void Round(int round)
        {
            Console.WriteLine($"\n   ***** Round #{round} *****");
        }

        public void RequestToPickColours(Dictionary<int, string> colourRange)
        {
            Console.WriteLine("\nPick form the following colours:  (or 'X' to exit)");


            foreach (var colour in colourRange)
            {
                Console.Write($"{colour.Key} - {colour.Value} ");
                if (colour.Key < colourRange.Count)
                {
                    Console.Write("| ");
                }
            }

            Console.WriteLine("\n");
        }

        public void ShowProgressivePlayerInput(List<int> playerPick, Dictionary<int, string> colourRange, int coloursToPick)
        {
            // write over the top of the same line...
            Console.SetCursorPosition(0, Console.CursorTop -0);
            
            Console.Write("You picked: ");

            foreach (var pick in playerPick)
            {
                Console.Write($"  {colourRange[pick]}");
            }

            if (playerPick.Count == coloursToPick)
            {
                Console.WriteLine("\n");   
            }
        }

        public void ShowResult(string matchStatus, Dictionary<string, int> colourMatch)
        {
            switch (matchStatus)
            {
                case "win":
                    Console.WriteLine("*** You Win ***\n");
                    break;
                case "none":
                    Console.WriteLine("Your picked none...\n");
                    break;
                default:
                    Console.WriteLine($"You scored {colourMatch["Black"]} Blacks and {colourMatch["White"]} Whites\n");
                    break;
            }
        }
        
        public void TryAgain()
        {
            Console.WriteLine("Would you like to try again? y/n\n");
        }

        public void OutOfRounds(int maximumRounds)
        {
            Console.WriteLine($"You ran out of rounds - only {maximumRounds} allowed.\n");
        }
    }
}