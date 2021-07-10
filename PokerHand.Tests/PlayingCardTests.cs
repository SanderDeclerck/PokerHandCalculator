using System;
using System.Linq;
using PokerHand.Domain;
using Xunit;

namespace PokerHand.Tests
{
    public class PlayingCardTests
    {
        public static MatrixTheoryData<Rank, Suit> MatrixData
            = new MatrixTheoryData<Rank, Suit>(
                Enum.GetValues(typeof(Rank)).Cast<Rank>().Except(new[] { Rank.None }),
                Enum.GetValues(typeof(Suit)).Cast<Suit>().Except(new[] { Suit.None }));

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void PlayingCard_CanRepresentAllCards(Rank rank, Suit suit)
        {
            var card = new PlayingCard(rank, suit);

            Assert.Equal(rank, card.Rank);
            Assert.Equal(suit, card.Suit);
        }

        [Fact]
        public void PlayingCard_CanHaveOnlyOneRank()
        {
            Assert.Throws<ArgumentException>(() => new PlayingCard(Rank.Ace | Rank.King, Suit.Spades));
        }

        [Fact]
        public void PlayingCard_MustHaveARankValue()
        {
            Assert.Throws<ArgumentException>(() => new PlayingCard(Rank.None, Suit.Spades));
        }

        [Fact]
        public void PlayingCard_CanHaveOnlyOneSuit()
        {
            Assert.Throws<ArgumentException>(() => new PlayingCard(Rank.Ace, Suit.None));
        }

        [Fact]
        public void PlayingCard_MustHaveASuitValue()
        {
            Assert.Throws<ArgumentException>(() => new PlayingCard(Rank.Five, Suit.None));
        }
    }
}
