using System;

namespace PokerHand.Domain
{
    public struct PlayingCard
    {
        public const uint RankMask = 0b11111111111110000;
        public const uint SuitMask = 0b00000000000001111;
        public const int RankShift = 4;
        private readonly uint _value;

        public PlayingCard(Rank rank, Suit suit)
        {
            if (System.Runtime.Intrinsics.X86.Popcnt.PopCount((uint)rank) != 1)
            {
                throw new ArgumentException("A playing card must have a single rank");
            }
            if (System.Runtime.Intrinsics.X86.Popcnt.PopCount((uint)suit) != 1)
            {
                throw new ArgumentException("A playing card must have a single rank");
            }

            _value = (uint)suit;
            _value += ((uint)rank) << RankShift;
        }

        public Rank Rank
        {
            get
            {
                var rank = (_value & RankMask);
                return (Rank)(rank >> RankShift);
            }
        }

        public Suit Suit
        {
            get
            {
                var value = _value & SuitMask;
                return (Suit)value;
            }
        }

        public static bool operator ==(PlayingCard first, PlayingCard second) => first._value == second._value;
        public static bool operator !=(PlayingCard first, PlayingCard second) => first._value != second._value;
    }
}
