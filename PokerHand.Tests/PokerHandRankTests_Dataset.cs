using System;
using System.Linq;
using PokerHand.Domain;
using Xunit;

namespace PokerHand.Tests
{
    public class PokerHandRankTests_Dataset
    {
        [Fact]
        public void VerifyDataSet()
        {
            var testData = PokerHandTestDataReader.GetTestData();

            foreach (var testRecord in testData)
            {
                var rank = new PokerHandRank(testRecord.Hand.Cards);
                Assert.Equal(testRecord.ExpectedRank, rank.Value);
            }
        }
    }
}