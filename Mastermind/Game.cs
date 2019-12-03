using System;
   using System.Linq;
   
   namespace Mastermind
   {
       public class Game
       {
           public int[] GameColours { get; }
           public string QueryResult { get; set; }
           private int[] PickCheck { get; set; }
           private int[] GameCheck { get; set; }
   
           public Game()
           {
               GameColours = new int[4];
           }
   
           // generate 4 colours for a new game
           public void Initialise()
           {
               var colourRange = new SelectColours().ColourRange;
   
               for (int i = 0; i < 4; i++)
               {
                   Random rnd = new Random();
                   int pick = rnd.Next(colourRange.Length);
                   GameColours[i] = pick;
               }
           }
   
           // helper function to remove index# from array
           private int[] RemoveIndex(int[] items, int i)
           {
               items = items.Where((val, idx) => idx != i).ToArray();
               return items;
           }
   
           // helper function to check for matching pairs with same index (B's)
           private void PickTheBs()
           {
               for (int i = 0; i < GameCheck.Length; i++)
               {
                   if (GameCheck[i] == PickCheck[i])
                   {
                       QueryResult = QueryResult + "B ";
   
                       GameCheck = RemoveIndex(GameCheck, i);
                       PickCheck = RemoveIndex(PickCheck, i);
                       i--;
                   }
               }
           }
   
           // helper function to check for W's in remaining index
           private void PickTheWs()
           {
               for (int i = 0; i < PickCheck.Length; i++)
               {
                   for (int j = 0; j < GameCheck.Length; j++)
                   {
                       if (PickCheck[i] == GameCheck[j])
                       {
                           QueryResult = QueryResult + "W ";
   
                           PickCheck = RemoveIndex(PickCheck, i);
                           GameCheck = RemoveIndex(GameCheck, j);
                           i--;
                           break;
                       }
                   }
               }
           }
   
           // check colour selections of player against computer (game)
           public void Check(int[] colourPick)
           {
               // inti vars
               QueryResult = "";
               GameCheck = GameColours; // create local copy of array
               PickCheck = colourPick; // create local copy of array
   
               // This is a 2-step check:
               // 1st Step:  Look for black pairs in pickCheck and gameCheck, then,
               //            if true: add a "B " to QueryResult and
               //            and eliminate that index from both arrays
               PickTheBs();
   
               // 2nd Step:  Loop through remaining local pickCheck looking for match in gameCheck, then,
               //            if true: add a "W " to QueryResult
               PickTheWs();
           }
       }
   }