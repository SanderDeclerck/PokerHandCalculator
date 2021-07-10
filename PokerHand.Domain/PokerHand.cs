namespace PokerHand.Domain
{
    public class Hand
    {
        public PlayingCard[] Cards { get; }

        public Hand(PlayingCard[] cards)
        {
            Cards = cards;
        }
    }
}