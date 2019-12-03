using System;

namespace Mastermind
{
    public class SelectColours
    {
        public string[] ColourRange { get; }
        public int[] ColourPick { get; }


        public SelectColours()
        {
            ColourRange = new string[6] {"Red", "Blue", "Green", "Orange", "Purple", "Yellow"};
            ColourPick = new int[4];
        }

        public void PickColours()
        {
            Console.WriteLine("Pick form the following colours: ");
            var colourNumber = 1;

            foreach (var colour in ColourRange)
            {
                Console.WriteLine($"{colourNumber} - {colour}");
                colourNumber++;
            }

            Console.WriteLine("");

            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine($"Your Colour #{i + 1}");

                bool inputNotValid = true;

                while (inputNotValid)
                {
                    var input = Console.ReadLine();

                    if (int.TryParse(input, out var value) && (1 <= value && value <= 6))
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
}