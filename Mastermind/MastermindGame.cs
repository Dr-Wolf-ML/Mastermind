using System;

namespace Mastermind
{
    public class MastermindGame
    {
        private bool GameActive { get; set; }
        private int Round { get; set; }

        public void Start()
        {
            // init new Game
            var colourRange = new SelectColours().ColourRange;
            var selectColours = new SelectColours();
            var game = new Game();
            Round = 1;

            Console.WriteLine("New game? y/n");

            if (Console.ReadLine() == "y")
            {
                game.Initialise();
                
                // Option 1: "the computer picked..."
                Console.WriteLine(
                    $"\nThe Computer picked " +
                    $"{colourRange[game.GameColours[0]]}, " +
                    $"{colourRange[game.GameColours[1]]}, " +
                    $"{colourRange[game.GameColours[2]]}, " +
                    $"{colourRange[game.GameColours[3]]}"
                );

                // Option 2: "the computer picked..."
                string awesomeString = $"\nThe Computer picked ";
                foreach (var colour in game.GameColours)
                {
                    string colourName = colourRange[colour];
                    awesomeString += $"{colourName}, ";
                }
                Console.WriteLine(awesomeString.Remove(awesomeString.Length - 2));
                
                GameActive = true;
            }
            else
            {
                GameActive = false;
                return;
            }

            while (GameActive)
            {
                Console.WriteLine($"\n***** Round {Round} *****");

                // select an array of 4 colours
                selectColours.PickColours();

                // compare ColourPick and game
                game.Check(selectColours.ColourPick);

                // show results
                if (game.QueryResult == "B B B B ")
                {
                    Console.WriteLine($"\nYou Win!\n");
                    GameActive = false;
                }
                else if (Round < 60)
                {
                    PublishResult(game);
                    Console.WriteLine($"\nWould you like to try again? y/n");
                    if (Console.ReadLine() == "y")
                    {
                        Round++;
                        GameActive = true;
                    }
                    else
                    {
                        GameActive = false;
                    }
                }
                else if (Round == 60)
                {
                    PublishResult(game);
                    Console.WriteLine($"\nYou ran out of rounds - you loose!\n");
                    GameActive = false;
                }
            }

            Start();
        }

        private void PublishResult(Game game)
        {
            if (game.QueryResult == "")
                game.QueryResult = "no match";

            Console.WriteLine($"\nYour pick returned {game.QueryResult}");
        }
    }
}