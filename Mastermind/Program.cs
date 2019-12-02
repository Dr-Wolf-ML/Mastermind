using System;
using System.Linq;

namespace Mastermind
{
    public class Program
    {
        static void Main(string[] args)
        {
            // starting game
            MastermindGame.Start();
        }
    }

    public class ArgumentTypes
    {
        public static readonly string[] ColourRange = new string[6] {"Red", "Blue", "Green", "Orange", "Purple", "Yellow"};
        protected static int[] ColourPick = new int[4];
    }

    public class SelectColours : ArgumentTypes
    {
        public string Test = "";
        
        public static void PickColours()
        {
            Console.WriteLine("Pick form the following colours: ");
            var colourNumber = 1;

            foreach (var colour in ColourRange)
            {
                Console.WriteLine($"{colourNumber} - {colour}");
                colourNumber++;
            }

            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine($"Your Colour #{i + 1}");

                bool inputNotValid = true;
                
                while (inputNotValid)
                {
                    var input = Console.ReadLine();
                    int value;
                    if (int.TryParse(input, out value) && (1 <= value && value <= 6))
                    {
                        ColourPick[i] = value - 1;
                            inputNotValid = false;
                    }
                    else
                    {
                        Console.WriteLine("Input restricted to numbers 1 to 6 - please try again.");
                    }
                    
                }
            }
        }
    }

    public class MastermindGame : SelectColours
    {
        private static bool _gameActive;
        private static int _round = 1;

        public static void Start()
        {
            Console.WriteLine("New game? y/n");

            if (Console.ReadLine() == "y")
            {
                Game.Initialise();
                Console.WriteLine(
                    $"\nThe Computer picked {ColourRange[Game.GameColours[0]]}, {ColourRange[Game.GameColours[1]]}, {ColourRange[Game.GameColours[2]]}, {ColourRange[Game.GameColours[3]]} ");

                _gameActive = true;
            }
            
            while (_gameActive)
            {
                Console.WriteLine($"\n***** Round {_round} *****");

                // select an array of 4 colours
                PickColours();

                // compare ColourPick and game
                Game.Check(ColourPick);

                // show results
                if (Game.QueryResult == "B B B B ")
                {
                    Console.WriteLine($"\nYou Win!\n");
                    _gameActive = false;
                }
                else if (_round < 60)
                {
                    PublishResult();
                    Console.WriteLine($"\nWould you like to try again? y/n");
                    if (Console.ReadLine() == "y")
                    {
                        _round++;
                        _gameActive = true;
                    }
                    else
                    {
                        _gameActive = false;
                    }
                }
                else if (_round == 60)
                {
                    PublishResult();
                    Console.WriteLine($"\nYou ran out of rounds - you loose!\n");
                    _gameActive = false;
                }
            }
        }

        private static void PublishResult()
        {
            if (Game.QueryResult == "")
                Game.QueryResult = "no match";

            Console.WriteLine($"\nYour pick returned {Game.QueryResult}");
        }
    }
}