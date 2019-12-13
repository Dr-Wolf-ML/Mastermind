using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Mastermind
{
    public class MasterMind
    {
        public bool NewGame { set; get; }
        public bool GameActive { set; get; }
        public Dictionary<int, string> ColourRange { get; set; }
        public List<int> ComputerColours { get; set; }
        public List<int> PlayerColours { get; set; }
        public Dictionary<string, int> ColourMatch { get; set; }
        public string MatchStatus { get; set; }
        public int Round { get; set; }
        public int MaximumRounds { get; set; }
        public int ColoursToPick { get; set; }

        private readonly ConsoleIo _consoleIo;
        private readonly Messages _messages;
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


            // Initialise Instances
            _consoleIo = new ConsoleIo();
            _messages = new Messages();
            _utils = new Utils();
        }

        public void Start()
        {
            _messages.NewGame();

            NewGame = _consoleIo.NewGame();

            if (NewGame)
            {
                GameActive = true;
                AddColoursToColourRange();

                // pick random ComputerColours
                ComputerColours = _utils.PickRandomColours(ColourRange, ColoursToPick);

                Console.Clear();
                MainGame();
            }
            else
            {
                _messages.Bye();
            }
        }

        public void MainGame()
        {
            while (GameActive)
            {
                // tell player the colours the computer picked
                _messages.AdviseColoursPicked(ComputerColours, ColourRange);

                // tell player which Round # it is
                _messages.Round(Round);

                // tell player to pick 4 colours
                _messages.RequestToPickColours(ColourRange);

                // get player to pick 4 colours
                PlayerColours = _consoleIo.PickColours(ColoursToPick, ColourRange);

                // check PlayerColours for 'Black' and 'White' matches in ComputerColours
                ColourMatch = _utils.CheckForColourMatches(ComputerColours, PlayerColours);

                // get MatchStatus
                MatchStatus = _utils.EvalPlayerColours(ColourMatch);

                // tell player the result of this round
                _messages.ShowResult(MatchStatus, ColourMatch);

                // branch based on result
                BranchOnResult(MatchStatus);
            }
        }

        // want more colours? add them to the string[]
        private void AddColoursToColourRange()
        {
            string[] coloursToAdd = {"Red", "Blue", "Green", "Orange", "Purple", "Yellow"};
            int count = 1;
            foreach (string colour in coloursToAdd)
            {
                ColourRange.Add(count, colour);
                count++;
            }
        }

        public void BranchOnResult(string matchStatus)
        {
            switch (matchStatus)
            {
                case "win":
                    Exit();
                    break;
                default:
                    if (Round == MaximumRounds)
                    {
                        _messages.OutOfRounds(MaximumRounds);
                        Exit();
                        break;
                    }

                    // ask player to try again?
                    _messages.TryAgain();

                    // get player's answer
                    if (_consoleIo.PlayAgain() && Round < MaximumRounds)
                    {
                        Console.Clear();
                        Round++;
                        break;
                    }

                    Exit();
                    break;
            }
        }

        public void Exit()
        {
            Environment.Exit(-1);
        }
    }
}