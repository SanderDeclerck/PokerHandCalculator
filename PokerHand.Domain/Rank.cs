using System;

namespace PokerHand.Domain
{
    [Flags]
    public enum Rank : uint
    {
        None = 0,
        Two = 1 << 0,
        Three = 1 << 1,
        Four = 1 << 2,
        Five = 1 << 3,
        Six = 1 << 4,
        Seven = 1 << 5,
        Eight = 1 << 6,
        Nine = 1 << 7,
        Ten = 1 << 8,
        Jack = 1 << 9,
        Queen = 1 << 10,
        King = 1 << 11,
        Ace = 1 << 12,
    }
}
