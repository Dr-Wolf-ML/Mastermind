namespace Mastermind
{
    // got all Black's? =>  Win (for "You win.")
    // got some Black's and/or White's? => Some (for "you found some.")
    // got no matches at all? => None (for "You found none.")
    public enum MatchStatusOptions
    {
        Win,
        Some,
        None
    };
}