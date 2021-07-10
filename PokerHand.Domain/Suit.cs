using System;

namespace PokerHand.Domain
{
    [Flags]
    public enum Suit : uint
    {
        None = 0,
        Clubs = 1 << 0,
        Diamonds = 1 << 1,
        Hearts = 1 << 2,
        Spades = 1 << 3
    }
}
