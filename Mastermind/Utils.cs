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

            for (var i = 1; i < coloursToPick + 1; i++)
            {
                var rnd = new Random();
                var pick = rnd.Next(1, colourRange.Count() + 1);
                randomColours.Add(pick);
            }

            return randomColours;
        }

        public Dictionary<string, int> CheckForColourMatches(List<int> computerColours, List<int> playerColours)
        {
            var colourMatch = new Dictionary<string, int> {{"Black", 0}, {"White", 0}};

            var cColours = new List<int>(computerColours);
            var pColours = new List<int>(playerColours);

            var colourIndexToBeRemoved = new List<int>();

            // find matching blacks
            for (var i = 0; i < pColours.Count; i++)
            {
                if (pColours[i] != cColours[i]) continue;
                // account for the match
                colourMatch["Black"] += 1;

                // add matching colour pair to this list for removal after this loop has finished
                colourIndexToBeRemoved.Add(i);
            }

            // remove those matching blacks from the list in each
            for (var i = colourIndexToBeRemoved.Count - 1; i > -1; i--)
            {
                pColours.Remove(pColours[colourIndexToBeRemoved[i]]);
                cColours.Remove(cColours[colourIndexToBeRemoved[i]]);
            }

            // find matching whites
            foreach (var colour in pColours)
            {
                var colour1 = colour;
                var match = cColours.FindIndex(i => i == colour1);

                if (match == -1) continue;
                colourMatch["White"] += 1;

                cColours.Remove(cColours[match]);
            }

            return colourMatch;
        }

        public Enum EvalPlayerColours(Dictionary<string, int> colourMatch, int coloursToPick)
        {
            if (colourMatch["Black"] == coloursToPick)
            {
                return MatchStatusOptions.Win;
            }

            if (colourMatch["Black"] == 0 && colourMatch["White"] == 0)
            {
                return MatchStatusOptions.None;
            }

            return MatchStatusOptions.Some;
        }

        public int IncrementRound(int round)
        {
            return round += 1;
        }
    }
}