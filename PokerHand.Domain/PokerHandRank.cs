using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand.Domain
{
    public class PokerHandRank
    {
        private IEnumerable<PlayingCard> _rankedCards;
        private IEnumerable<PlayingCard> _cards;
        public RankValue Value { get; private set; }
        public PlayingCard[] RankedCards => _rankedCards.ToArray();

        public static PokerHandRank Evaluate(IEnumerable<PlayingCard> cards)
        {
            return new PokerHandRank(cards);
        }

        public PokerHandRank(IEnumerable<PlayingCard> cards)
        {
            _cards = cards;
            Initialize();
        }

        private void Initialize()
        {
            var rankCounts = GetRankCounts(_cards);
            var flushCards = GetFlushCards(_cards);
            if (flushCards != null)
            {
                var straightFlushCards = GetStraightCards(flushCards, out bool hasStraightFlush);

                if (hasStraightFlush)
                {
                    _rankedCards = straightFlushCards;
                    if (straightFlushCards.First().Rank == Rank.Ace)
                    {
                        Value = RankValue.RoyalFlush;
                        return;
                    }

                    Value = RankValue.StraightFlush;
                    return;
                }

            }
            if (rankCounts.HasFourOfAKind)
            {
                Value = RankValue.FourOfAKind;
                // TODO
                _rankedCards = Enumerable.Empty<PlayingCard>();
                return;
            }
            if (rankCounts.HasFullHouse)
            {
                Value = RankValue.FullHouse;
                // TODO
                _rankedCards = Enumerable.Empty<PlayingCard>();
                return;
            }
            if (flushCards != null)
            {
                _rankedCards = flushCards;
                Value = RankValue.Flush;
                return;
            }
            var straightCards = GetStraightCards(_cards, out var hasStraight);
            if (hasStraight)
            {
                _rankedCards = straightCards;
                Value = RankValue.Straight;
                return;
            }
            if (rankCounts.HasThreeOfAKind)
            {
                Value = RankValue.ThreeOfAKind;
                // TODO
                _rankedCards = Enumerable.Empty<PlayingCard>();
                return;
            }
            if (rankCounts.HasTwoPair)
            {
                Value = RankValue.TwoPair;
                // TODO
                _rankedCards = Enumerable.Empty<PlayingCard>();
                return;
            }
            if (rankCounts.HasPair)
            {
                Value = RankValue.Pair;
                // TODO
                _rankedCards = Enumerable.Empty<PlayingCard>();
                return;
            }

            Value = RankValue.HighCard;
            _rankedCards = Enumerable.Empty<PlayingCard>();
        }

        private struct RankCounts
        {
            private uint singleRanks;
            private uint pairRanks;
            private uint tripRanks;
            private uint quadRanks;

            private uint exclTripRanks => (tripRanks ^ quadRanks) & tripRanks;
            private uint exclPairRanks => (pairRanks ^ tripRanks) & pairRanks;

            public bool HasTwoPair => System.Runtime.Intrinsics.X86.Popcnt.PopCount(exclPairRanks) == 2;
            public bool HasPair => exclPairRanks != 0;
            public bool HasFullHouse => HasThreeOfAKind && HasPair;
            public bool HasThreeOfAKind => exclTripRanks != 0;
            public bool HasFourOfAKind => quadRanks != 0;

            public void AddRank(Rank rank)
            {
                var previous = singleRanks;
                singleRanks |= (uint)rank;
                if (singleRanks != previous)
                {
                    return;
                }

                previous = pairRanks;
                pairRanks |= (uint)rank;
                if (pairRanks != previous)
                {
                    return;
                }

                previous = tripRanks;
                tripRanks |= (uint)rank;
                if (tripRanks != previous)
                {
                    return;
                }

                previous = quadRanks;
                quadRanks |= (uint)rank;
                if (quadRanks != previous)
                {
                    return;
                }
            }
        }

        private static RankCounts GetRankCounts(IEnumerable<PlayingCard> cards)
        {
            var rankCounts = new RankCounts();
            foreach (var card in cards)
            {
                rankCounts.AddRank(card.Rank);
            }
            return rankCounts;
        }

        private static IEnumerable<PlayingCard> GetFlushCards(IEnumerable<PlayingCard> cards)
        {
            uint clubCount = 0, spadesCount = 0, diamondCount = 0, heartCount = 0;

            foreach (var card in cards)
            {
                clubCount += (uint)card.Suit & 1;
                diamondCount += (uint)card.Suit >> 1 & 1;
                heartCount += (uint)card.Suit >> 2 & 1;
                spadesCount += (uint)card.Suit >> 3 & 1;
            }

            if (clubCount > 4)
            {
                return cards.Where(card => card.Suit == Suit.Clubs);
            }
            if (diamondCount > 4)
            {
                return cards.Where(card => card.Suit == Suit.Diamonds);
            }
            if (heartCount > 4)
            {
                return cards.Where(card => card.Suit == Suit.Hearts);
            }
            if (spadesCount > 4)
            {
                return cards.Where(card => card.Suit == Suit.Spades);
            }

            return null;
        }

        private static IEnumerable<PlayingCard> GetStraightCards(IEnumerable<PlayingCard> cards, out bool hasStraight)
        {
            byte serie = default;
            uint currentMask = default;
            uint rankBits = default;

            hasStraight = false;

            foreach (var card in cards)
            {
                rankBits |= (uint)card.Rank;
            }

            for (var i = 12; i >= -1; i--)
            {
                currentMask = i == -1 ? (uint)1 << 12 : (uint)1 << i;
                if ((currentMask & rankBits) == 0)
                {
                    serie = 0;
                    continue;
                }

                serie++;
                if (serie != 5)
                {
                    continue;
                }

                hasStraight = true;
                return getStraightCardsInOrder(i);
            }

            return null;

            IEnumerable<PlayingCard> getStraightCardsInOrder(int lowestPosition)
            {
                if (lowestPosition != -1)
                {
                    yield return cards.First(card => ((uint)card.Rank & (currentMask << 4)) != 0);
                    yield return cards.First(card => ((uint)card.Rank & (currentMask << 3)) != 0);
                    yield return cards.First(card => ((uint)card.Rank & (currentMask << 2)) != 0);
                    yield return cards.First(card => ((uint)card.Rank & (currentMask << 1)) != 0);
                    yield return cards.First(card => ((uint)card.Rank & currentMask) != 0);
                    yield break;
                }

                yield return cards.First(card => card.Rank == Rank.Five);
                yield return cards.First(card => card.Rank == Rank.Four);
                yield return cards.First(card => card.Rank == Rank.Three);
                yield return cards.First(card => card.Rank == Rank.Two);
                yield return cards.First(card => card.Rank == Rank.Ace);
                yield break;
            }
        }

        public enum RankValue : uint
        {
            HighCard = 0,
            Pair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            Straight = 4,
            Flush = 5,
            FullHouse = 6,
            FourOfAKind = 7,
            StraightFlush = 8,
            RoyalFlush = 9
        }
    }
}