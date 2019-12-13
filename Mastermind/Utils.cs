using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
    public class Utils
    {
        // no constructor needed
        
        public List<int> PickRandomColours(Dictionary<int, string> colourRange, int coloursToPick)
        {
            List<int> randomColours = new List<int>();

            for (int i = 1; i < coloursToPick + 1; i++)
            {
                Random rnd = new Random();
                int pick = rnd.Next(1, colourRange.Count() + 1);
                randomColours.Add(pick);
            }

            return randomColours;
        }

        public Dictionary<string, int> CheckForColourMatches(List<int> computerColours, List<int> playerColours)
        {
            Dictionary<string, int> colourMatch = new Dictionary<string, int>();
            colourMatch.Add("Black", 0);
            colourMatch.Add("White", 0);
            
            List<int> cColours = new List<int>(computerColours);
            List<int> pColours = new List<int>(playerColours);

            List<int> colourIndexToBeRemoved = new List<int>();

            // find matching blacks
            for (int i = 0; i < pColours.Count; i++)
            {
                if (pColours[i] == cColours[i])
                {
                    // account for the match
                    colourMatch["Black"] += 1;
                    
                    // add matching colour pair to this list for removal after this loop has finished
                    colourIndexToBeRemoved.Add(i);
                }
            }
            
            // remove those matching blacks from the list in each
            for (int i = colourIndexToBeRemoved.Count - 1; i > - 1 ; i--)
            {
                pColours.Remove(pColours[colourIndexToBeRemoved[i]]);
                cColours.Remove(cColours[colourIndexToBeRemoved[i]]);
            }
            
            // find matching whites
            foreach (int colour in pColours)
            {
                int match;
                
                match = cColours.FindIndex(i => i == colour);

                if (match != - 1)
                {
                    colourMatch["White"] += 1;
                    
                    cColours.Remove(cColours[match]);
                }
            }

            return colourMatch;
        }

        public string EvalPlayerColours(Dictionary<string, int> colourMatch)
        {
            if (colourMatch["Black"] == 4)
            {
                return "win";
            }

            if (colourMatch["Black"] == 0 && colourMatch["White"] == 0)
            {
                return "none";
            }

            return "some";
        }
    }
}