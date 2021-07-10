using PokerHand.Domain;
using Xunit;

namespace PokerHand.Tests
{
    public class PokerHandRankTests_Straight
    {
        [Theory]
        [MemberData(nameof(ValidStraights))]
        public void IsStraight(Hand hand)
        {
            var pokerHandRank = new PokerHandRank(hand.Cards);

            Assert.Equal(PokerHandRank.RankValue.Straight, pokerHandRank.Value);
        }

        [Theory]
        [MemberData(nameof(InvalidStraights))]
        public void IsNotStraight(Hand hand)
        {
            var pokerHandRank = new PokerHandRank(hand.Cards);

            Assert.NotEqual(PokerHandRank.RankValue.Straight, pokerHandRank.Value);
        }

        public static TheoryData<Hand> ValidStraights => new TheoryData<Hand>
            {
                new Hand(new[]
                {
                    new PlayingCard(Rank.Ace, Suit.Hearts),
                    new PlayingCard(Rank.King, Suit.Hearts),
                    new PlayingCard(Rank.Queen, Suit.Spades),
                    new PlayingCard(Rank.Jack, Suit.Hearts),
                    new PlayingCard(Rank.Ten, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Hearts),
                    new PlayingCard(Rank.King, Suit.Hearts),
                    new PlayingCard(Rank.Queen, Suit.Spades),
                    new PlayingCard(Rank.Jack, Suit.Hearts),
                    new PlayingCard(Rank.Ten, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Queen, Suit.Spades),
                    new PlayingCard(Rank.Jack, Suit.Hearts),
                    new PlayingCard(Rank.Ten, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Seven, Suit.Spades),
                    new PlayingCard(Rank.Jack, Suit.Hearts),
                    new PlayingCard(Rank.Ten, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Seven, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Hearts),
                    new PlayingCard(Rank.Ten, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Nine, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Seven, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Hearts),
                    new PlayingCard(Rank.Five, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Four, Suit.Hearts),
                    new PlayingCard(Rank.Eight, Suit.Hearts),
                    new PlayingCard(Rank.Seven, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Hearts),
                    new PlayingCard(Rank.Five, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Four, Suit.Hearts),
                    new PlayingCard(Rank.Three, Suit.Hearts),
                    new PlayingCard(Rank.Seven, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Hearts),
                    new PlayingCard(Rank.Five, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Four, Suit.Hearts),
                    new PlayingCard(Rank.Three, Suit.Hearts),
                    new PlayingCard(Rank.Two, Suit.Spades),
                    new PlayingCard(Rank.Six, Suit.Hearts),
                    new PlayingCard(Rank.Five, Suit.Hearts),
                }),
                new Hand(new[]
                {
                    new PlayingCard(Rank.Four, Suit.Hearts),
                    new PlayingCard(Rank.Three, Suit.Hearts),
                    new PlayingCard(Rank.Two, Suit.Spades),
                    new PlayingCard(Rank.Ace, Suit.Hearts),
                    new PlayingCard(Rank.Five, Suit.Hearts),
                }),
            };

        public static TheoryData<Hand> InvalidStraights => new TheoryData<Hand>
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