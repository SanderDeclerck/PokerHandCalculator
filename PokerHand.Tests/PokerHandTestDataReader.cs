using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PokerHand.Domain;

namespace PokerHand.Tests
{
    public static class PokerHandTestDataReader
    {
        public static (Hand Hand, PokerHandRank.RankValue ExpectedRank)[] GetTestData()
        {
            return ReadData()
                .Where(input => !string.IsNullOrEmpty(input))
                .Select(MapToHandAndExpectedRank)
                .ToArray();
        }

        private static string[] ReadData()
        {
            var assembly = typeof(PokerHandRankTests_Dataset).Assembly;
            using var stream = assembly.GetManifestResourceStream("PokerHand.Tests.dataset.poker-hand-testing.data");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(Environment.NewLine);
        }

        private static (Hand Hand, PokerHandRank.RankValue ExpectedRank) MapToHandAndExpectedRank(string csvInput)
        {
            var cards = new List<PlayingCard>();
            var values = csvInput.Split(',').Select(value => int.Parse(value));
            var valueEnumerator = values.GetEnumerator();
            for (var i = 0; i < 5; i++)
            {
                valueEnumerator.MoveNext();
                var suit = valueEnumerator.Current;
                valueEnumerator.MoveNext();
                var rank = valueEnumerator.Current;
                cards.Add(MapPlayingCard(suit, rank));
            }
            valueEnumerator.MoveNext();
            return (new Hand(cards.ToArray()), (PokerHandRank.RankValue)valueEnumerator.Current);
        }

        private static PlayingCard MapPlayingCard(int suit, int rank)
        {
            return new PlayingCard(
                rank switch
                {
                    1 => Rank.Ace,
                    2 => Rank.Two,
                    3 => Rank.Three,
                    4 => Rank.Four,
                    5 => Rank.Five,
                    6 => Rank.Six,
                    7 => Rank.Seven,
                    8 => Rank.Eight,
                    9 => Rank.Nine,
                    10 => Rank.Ten,
                    11 => Rank.Jack,
                    12 => Rank.Queen,
                    _ => Rank.King,
                },
                suit switch
                {
                    1 => Suit.Clubs,
                    2 => Suit.Diamonds,
                    3 => Suit.Hearts,
                    _ => Suit.Spades,
                }
            );
        }
    }
}