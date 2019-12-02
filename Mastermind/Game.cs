using System;
using System.Linq;

namespace Mastermind
{
    public class Game
    {
        public static int[] GameColours = new int[4];
        public static string QueryResult;

        // generate 4 colours for a new game
        public static void Initialise()
        {
            for (int i = 0; i < 4; i++)
            {
                Random rnd = new Random();
                int pick = rnd.Next(ArgumentTypes.ColourRange.Length);
                GameColours[i] = pick;
            }

            // for testing only
            // GameColours = new int[] {3, 1, 1, 1};
        }

        // helper function to remove index# from array
        private static int[] RemoveIndex(int[] items, int i)
        {
            items = items.Where((val, idx) => idx != i).ToArray();
            return items;
        }

        // check player selections against game colours
        public static void Check(int[] colourPick)
        {
            // inti vars
            QueryResult = ""; // reset QueryResult
            int[] gameCheck = GameColours; // create local copy of array
            int[] pickCheck = colourPick; // create local copy of array
            bool notCheckedAll = true; // initialise while loop
            bool notCheckedAll1 = true; // initialise while loop
            bool notCheckedAll2 = true; // initialise while loop

            // This is a 2-step check:
            // 1st Step:  Look for black pairs, if true: B++ and eliminate that index from both arrays
            while (notCheckedAll)
            {
                for (int i = 0; i < gameCheck.Length; i++)
                {
                    if (gameCheck[i] == pickCheck[i])
                    {
                        QueryResult = QueryResult + "B ";

                        gameCheck = RemoveIndex(gameCheck, i);
                        pickCheck = RemoveIndex(pickCheck, i);

                        if (gameCheck.Length == 0)
                            notCheckedAll = false;

                        break;
                    }

                    if (i == gameCheck.Length - 1)
                        notCheckedAll = false;
                }
            }

            // 2nd Step:  Loop through remaining local pickCheck looking for match in gameCheck
            //  - if true:  W++, eliminate that matching index in gameCheck
            //  keep looping until all pickCheck members have been checked
            if (gameCheck.Length > 0)
            {
                while (notCheckedAll1)
                {
                    for (int i = 0; i < pickCheck.Length; i++)
                    {
                        if (i < pickCheck.Length - 1)
                            notCheckedAll2 = true;

                        while (notCheckedAll2)
                        {
                            for (int j = 0; j < gameCheck.Length; j++)
                            {
                                if (pickCheck[i] == gameCheck[j])
                                {
                                    QueryResult = QueryResult + "W ";

                                    pickCheck = RemoveIndex(pickCheck, i);
                                    gameCheck = RemoveIndex(gameCheck, j);

                                    if (gameCheck.Length == 0)
                                    {
                                        notCheckedAll1 = false;
                                        notCheckedAll2 = false;
                                    }

                                    break;
                                }

                                if (j == gameCheck.Length - 1)
                                    notCheckedAll2 = false;
                            }
                        }

                        if (gameCheck.Length == 0 || i == pickCheck.Length - 1)
                            notCheckedAll1 = false;
                    }
                }
            }
        }
    }
}