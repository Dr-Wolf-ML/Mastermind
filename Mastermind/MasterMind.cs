using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Mastermind
{
    public class MasterMind
    {
        private bool NewGame { set; get; }
        private bool GameActive { set; get; }
        private Dictionary<int, string> ColourRange { get; }
        private List<int> ComputerColours { get; set; }
        private List<int> PlayerColours { get; set; }
        private Dictionary<string, int> ColourMatch { get; set; }
        private Enum MatchStatus { get; set; }
        private int Round { get; set; }
        private int MaximumRounds { get; }
        private int ColoursToPick { get; }
        private string[] ColoursToAdd  { get; }

        private readonly UiInput _uiInput;
        private readonly UiOutput _uiOutput;
        private readonly Utils _utils;

        public MasterMind()
        {
            // Initial Game Settings
            ColourRange = new Dictionary<int, string>();
            ColourMatch = new Dictionary<string, int>();

            ComputerColours = new List<int>();
            PlayerColours = new List<int>();

            NewGame = false;
            GameActive = false;

            Round = 1;
            ColoursToPick = 4;
            MaximumRounds = 60;
            //
            ColoursToAdd = new [] {"Red", "Blue", "Green", "Orange", "Purple", "Yellow"};            


            // Initialise Instances
            _uiInput = new UiInput();
            _uiOutput = new UiOutput();
            _utils = new Utils();
        }

        public void Start()
        {
            _uiOutput.NewGame();

            NewGame = _uiInput.NewGame();

            if (NewGame)
            {
                GameActive = true;
                AddColoursToColourRange(ColoursToAdd);

                // pick random ComputerColours
                ComputerColours = _utils.PickRandomColours(ColourRange, ColoursToPick);

                _uiOutput.ClearOutputScreen();
                MainGame();
            }
            else
            {
                _uiOutput.Bye();
            }
        }

        private void MainGame()
        {
            while (GameActive)
            {
                // tell player the colours the computer picked
                _uiOutput.AdviseColoursPicked(ComputerColours, ColourRange);

                // tell player which Round # it is
                _uiOutput.Round(Round);

                // tell player to pick 4 colours
                _uiOutput.RequestToPickColours(ColourRange);

                // get player to pick 4 colours
                PlayerColours = _uiInput.PickColours(ColoursToPick, ColourRange);

                // check PlayerColours for 'Black' and 'White' matches in ComputerColours
                ColourMatch = _utils.CheckForColourMatches(ComputerColours, PlayerColours);

                // get MatchStatus

                MatchStatus = _utils.EvalPlayerColours(ColourMatch, ColoursToPick);

                // tell player the result of this round
                _uiOutput.ShowResult(MatchStatus, ColourMatch);

                // branch based on result
                BranchOnResult(MatchStatus);
            }
        }

        // want more colours? add them to the string[]
        private void AddColoursToColourRange(string[] coloursToAdd)
        {
            var count = 1;
            foreach (var colour in coloursToAdd)
            {
                ColourRange.Add(count, colour);
                count++;
            }
        }

        private void BranchOnResult(Enum matchStatus)
        {
            switch (matchStatus)
            {
                case MatchStatusOptions.Win:
                    Exit();
                    break;
                
                
                default:
                    if (Round == MaximumRounds)
                    {
                        _uiOutput.OutOfRounds(MaximumRounds);
                        Exit();
                        break;
                    }

                    // ask player to try again?
                    _uiOutput.TryAgain();

                    // get player's answer
                    if (_uiInput.PlayAgain() && Round < MaximumRounds)
                    {
                        _uiOutput.ClearOutputScreen();
                        Round = _utils.IncrementRound(Round);
                        break;
                    }

                    Exit();
                    break;
            }
        }

        public static void Exit()
        {
            Environment.Exit(-1);
        }
    }
}