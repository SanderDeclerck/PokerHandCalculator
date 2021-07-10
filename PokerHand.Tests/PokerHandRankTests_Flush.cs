using PokerHand.Domain;
using Xunit;

namespace PokerHand.Tests
{
    public class PokerHandRankTests_Flush
    {
        [Theory]
        [MemberData(nameof(ValidFlush))]
        public void IsFlush(Hand hand)
        {
            var pokerHandRank = new PokerHandRank(hand.Cards);

            Assert.Equal(PokerHandRank.RankValue.Flush, pokerHandRank.Value);
        }

        [Theory]
        [MemberData(nameof(InvalidFlush))]
        public void IsNotFlush(Hand hand)
        {
            var pokerHandRank = new PokerHandRank(hand.Cards);

            Assert.NotEqual(PokerHandRank.RankValue.Flush, pokerHandRank.Value);
        }

        public static TheoryData<Hand> ValidFlush => new TheoryData<Hand>
            {
                new Hand(new[]
                {
                    new PlayingCard(Rank.Ace, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Ten, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Spades),
                    new PlayingCard(Rank.King, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Spades),
                    new PlayingCard(Rank.Ten, Suit.Spades),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Diamonds),
                    new PlayingCard(Rank.Nine, Suit.Diamonds),
                    new PlayingCard(Rank.Queen, Suit.Diamonds),
                    new PlayingCard(Rank.Queen, Suit.Diamonds),
                    new PlayingCard(Rank.Ten, Suit.Diamonds),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Clubs),
                    new PlayingCard(Rank.Five, Suit.Clubs),
                    new PlayingCard(Rank.Queen, Suit.Clubs),
                    new PlayingCard(Rank.Jack, Suit.Clubs),
                    new PlayingCard(Rank.Ten, Suit.Clubs),
                }),
            };

        public static TheoryData<Hand> InvalidFlush => new TheoryData<Hand>
            {
                new Hand(new[]
                {
                    new PlayingCard(Rank.Ace, Suit.Hearts),
                    new PlayingCard(Rank.King, Suit.Hearts),
                    new PlayingCard(Rank.Queen, Suit.Spades),
                    new PlayingCard(Rank.Jack, Suit.Hearts),
                    new PlayingCard(Rank.Two, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Four, Suit.Hearts),
                    new PlayingCard(Rank.Three, Suit.Hearts),
                    new PlayingCard(Rank.Two, Suit.Spades),
                    new PlayingCard(Rank.Ace, Suit.Hearts),
                    new PlayingCard(Rank.King, Suit.Hearts),
                }),
            };
    }
}