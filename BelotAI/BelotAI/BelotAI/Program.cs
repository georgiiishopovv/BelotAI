using BelotAlgorithm;
using System;

namespace BelotAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Card karta = new Card(Card.Type.Nine, Card.Suit.Hearts);
            Card karta2 = karta;
            karta.eval = 7;
            karta2.eval = 15;

            Console.WriteLine(karta.eval);
        }
    }
}