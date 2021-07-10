using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PokerHand.Domain;
using PokerHand.Tests;

namespace PokerHand.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<HandEvaluationBenchmark>();
        }
    }

    [MemoryDiagnoser]
    public class HandEvaluationBenchmark
    {
        public static int ValidRoyalFlushItems { get; set; }
        public static int ValidRoyalFlushCurrent { get; set; }
        public Hand ValidRoyalFlush { get; set; }


        public static int ValidStraightFlushItems { get; set; }
        public static int ValidStraightFlushCurrent { get; set; }
        public Hand ValidStraightFlush { get; set; }

        public static int ValidFourOfAKindItems { get; set; }
        public static int ValidFourOfAKindCurrent { get; set; }
        public Hand ValidFourOfAKind { get; set; }

        public static int ValidFullHouseItems { get; set; }
        public static int ValidFullHouseCurrent { get; set; }
        public Hand ValidFullHouse { get; set; }

        public static int ValidFlushItems { get; set; }
        public static int ValidFlushCurrent { get; set; }
        public Hand ValidFlush { get; set; }

        public static int ValidStraightItems { get; set; }
        public static int ValidStraightCurrent { get; set; }
        public Hand ValidStraight { get; set; }

        public static int ValidThreeOfAKindItems { get; set; }
        public static int ValidThreeOfAKindCurrent { get; set; }
        public Hand ValidThreeOfAKind { get; set; }

        public static int ValidTwoPairItems { get; set; }
        public static int ValidTwoPairCurrent { get; set; }
        public Hand ValidTwoPair { get; set; }

        public static int ValidPairItems { get; set; }
        public static int ValidPairCurrent { get; set; }
        public Hand ValidPair { get; set; }

        public static int ValidHighCardItems { get; set; }
        public static int ValidHighCardCurrent { get; set; }
        public Hand ValidHighCard { get; set; }

        public Hand AnyCard { get; set; }
        public static int CurrentCard { get; set; }

        private static Hand[] AllHands;
        public static ILookup<PokerHandRank.RankValue, Hand> Dataset;

        static HandEvaluationBenchmark()
        {
            var data = PokerHandTestDataReader.GetTestData();
            AllHands = data.Select(x => x.Hand).ToArray();
            Dataset = data.ToLookup(x => x.ExpectedRank, x => x.Hand);

            ValidRoyalFlushItems = Dataset[PokerHandRank.RankValue.RoyalFlush].Count();

            ValidStraightFlushItems = Dataset[PokerHandRank.RankValue.StraightFlush].Count();

            ValidFourOfAKindItems = Dataset[PokerHandRank.RankValue.FourOfAKind].Count();

            ValidFullHouseItems = Dataset[PokerHandRank.RankValue.FullHouse].Count();

            ValidFlushItems = Dataset[PokerHandRank.RankValue.Flush].Count();

            ValidStraightItems = Dataset[PokerHandRank.RankValue.Straight].Count();

            ValidThreeOfAKindItems = Dataset[PokerHandRank.RankValue.ThreeOfAKind].Count();

            ValidTwoPairItems = Dataset[PokerHandRank.RankValue.TwoPair].Count();

            ValidPairItems = Dataset[PokerHandRank.RankValue.Pair].Count();

            ValidHighCardItems = Dataset[PokerHandRank.RankValue.HighCard].Count();
        }

        [GlobalSetup]
        public void Setup()
        {
            ValidRoyalFlush = Dataset[PokerHandRank.RankValue.RoyalFlush].ElementAt(ValidRoyalFlushCurrent++ % ValidRoyalFlushItems);
            ValidStraightFlush = Dataset[PokerHandRank.RankValue.StraightFlush].ElementAt(ValidStraightFlushCurrent++ % ValidStraightFlushItems);
            ValidFourOfAKind = Dataset[PokerHandRank.RankValue.FourOfAKind].ElementAt(ValidFourOfAKindCurrent++ % ValidFourOfAKindItems);
            ValidFullHouse = Dataset[PokerHandRank.RankValue.FullHouse].ElementAt(ValidFullHouseCurrent++ % ValidFullHouseItems);
            ValidFlush = Dataset[PokerHandRank.RankValue.Flush].ElementAt(ValidFlushCurrent++ % ValidFlushItems);
            ValidStraight = Dataset[PokerHandRank.RankValue.Straight].ElementAt(ValidStraightCurrent++ % ValidStraightItems);
            ValidThreeOfAKind = Dataset[PokerHandRank.RankValue.ThreeOfAKind].ElementAt(ValidThreeOfAKindCurrent++ % ValidThreeOfAKindItems);
            ValidTwoPair = Dataset[PokerHandRank.RankValue.TwoPair].ElementAt(ValidTwoPairCurrent++ % ValidTwoPairItems);
            ValidPair = Dataset[PokerHandRank.RankValue.Pair].ElementAt(ValidPairCurrent++ % ValidPairItems);
            ValidHighCard = Dataset[PokerHandRank.RankValue.HighCard].ElementAt(ValidHighCardCurrent++ % ValidHighCardItems);
            AnyCard = AllHands.ElementAt(CurrentCard++ % AllHands.Length);
        }

        [Benchmark]
        public void DetermineRoyalFlush() => PokerHandRank.Evaluate(ValidRoyalFlush.Cards);
        [Benchmark]
        public void DetermineStraightFlush() => PokerHandRank.Evaluate(ValidStraightFlush.Cards);
        [Benchmark]
        public void DetermineFourOfAKind() => PokerHandRank.Evaluate(ValidFourOfAKind.Cards);
        [Benchmark]
        public void DetermineFullHouse() => PokerHandRank.Evaluate(ValidFullHouse.Cards);
        [Benchmark]
        public void DetermineFlush() => PokerHandRank.Evaluate(ValidFlush.Cards);
        [Benchmark]
        public void DetermineStraight() => PokerHandRank.Evaluate(ValidStraight.Cards);
        [Benchmark]
        public void DetermineThreeOfAKind() => PokerHandRank.Evaluate(ValidThreeOfAKind.Cards);
        [Benchmark]
        public void DetermineTwoPair() => PokerHandRank.Evaluate(ValidTwoPair.Cards);
        [Benchmark]
        public void DeterminePair() => PokerHandRank.Evaluate(ValidPair.Cards);
        [Benchmark]
        public void DetermineHighCard() => PokerHandRank.Evaluate(ValidHighCard.Cards);
        [Benchmark]
        public void DetermineAnyCard() => PokerHandRank.Evaluate(AnyCard.Cards);
    }
}
