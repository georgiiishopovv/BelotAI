using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BelotAlgorithm;


namespace BelotAI
{
    class Game
    {
        public enum AllTrumpsOrder
        {
            Seven = 0,
            Eight = 1,
            Queen = 2,
            King = 3,
            Ten = 4,
            Ace = 5,
            Nine = 6,
            Jack = 7
        }

        public Player south;
        public Player north;
        public Player west;
        public Player east;
        public string first;
        public List<Player> order;
        public Player winner;
        public enum BidStrength
        {
            Pass = 0,
            Clubs = 1,
            Diamonds = 2,
            Hearts = 3,
            Spades = 4,
            NoTrumps = 5,
            AllTrumps = 6
        }
        BidStrength gameBid;

        public Game(string first)
        {
            south = new Player("South");
            north = new Player("North");
            west = new Player("West");
            east = new Player("East");
            this.first = first;
            SetTurns();

        }


        public void SetTurns()
        {
            switch (first)
            {
                case "South":
                    order.Add(south);
                    order.Add(east);
                    order.Add(north);
                    order.Add(west);
                    break;
                case "East":
                    order.Add(east);
                    order.Add(north);
                    order.Add(west);
                    order.Add(south);
                    break;
                case "North":
                    order.Add(north);
                    order.Add(west);
                    order.Add(south);
                    order.Add(east);
                    break;
                case "West":
                    order.Add(west);
                    order.Add(south);
                    order.Add(east);
                    order.Add(north);
                    break;

            }
        }

        public BidStrength ConvertBid(string bid)
        {
            switch (bid)
            {
                case "Pass":
                    return Game.BidStrength.Pass;

                case "Clubs":
                    return Game.BidStrength.Clubs;

                case "Diamonds":
                    return Game.BidStrength.Diamonds;

                case "Hearts":
                    return Game.BidStrength.Hearts;

                case "Spades":
                    return Game.BidStrength.Spades;

                case "NoTrumps":
                    return Game.BidStrength.NoTrumps;

                case "AllTrumps":
                    return Game.BidStrength.AllTrumps;

                default:
                    return BidStrength.Pass;

            }
        }
        public void Bidding()
        {

            bool agreement = false;
            int currentPassCount = 0;

            while (!agreement)
            {

                if (currentPassCount == 4 || (gameBid != BidStrength.Pass && currentPassCount == 3))
                {
                    agreement = true;
                }

                if (order.ElementAt(0).Equals(south))
                {
                    order.ElementAt(0).Bid();
                }
                else
                {
                    Console.WriteLine("Input the bid");
                    order.ElementAt(0).bid = Console.ReadLine();
                }

                if (order.ElementAt(0).bid == "Pass")
                {
                    currentPassCount += 1;
                }
                else
                {
                    if ((int)ConvertBid(order.ElementAt(0).bid) >= (int)gameBid)
                    {
                        gameBid = ConvertBid(order.ElementAt(0).bid);
                        currentPassCount = 0;
                    }
                }

                Player hold = order.ElementAt(0);
                order.Remove(order.ElementAt(0));
                order.Add(hold);

            }
        }

        public int SuitsCount()
        {
            int spades = 0;
            int diamonds = 0;
            int hearts = 0;
            int clubs = 0;

            for (int i = 0; i < south.hand.Count; i++)
            {
                if (south.hand.ElementAt(i).CardSuit == Card.Suit.Spades)
                {
                    spades++;
                }
                else if (south.hand.ElementAt(i).CardSuit == Card.Suit.Hearts)
                {
                    hearts++;
                }
                else if (south.hand.ElementAt(i).CardSuit == Card.Suit.Diamonds)
                {
                    diamonds++;
                }
                else
                {
                    clubs++;
                }
            }
            if (spades >= 4)
            {
                return spades;
            }
            else if (hearts >= 4)
            {
                return hearts;
            }
            else if (clubs >= 4)
            {
                return clubs;
            }
            else
            {
                return diamonds;
            }
        }

        public void EvaluateHandAllTrumps()
        {

            int suitCount = SuitsCount();
            for (int i = 0; i < 8; i++)
            {
                switch (south.hand.Count)
                {
                    case 8:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 10;
                        }
                        break;
                    case 7:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 11;
                        }
                        break;
                    case 6:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 12;
                        }
                        break;
                    case 5:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 13;
                        }
                        break;
                    case 4:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 14;
                        }
                        break;
                    case 3:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 15;
                        }
                        break;
                    case 2:
                        if (south.hand.ElementAt(i).vlastna)
                        {
                            south.hand.ElementAt(i).eval += 16;
                        }
                        break;

                }

            }
            if (south.hand.Count > 6)
            {
                for (int i = 0; i < south.hand.Count; i++)
                {
                    if (south.hand.ElementAt(i).CardSuit.Equals(south.LongestSuit()))
                    {
                        south.hand.ElementAt(i).eval += 12;
                    }
                }
            }

            if (south.Equals(first))
            {
                int nineCount = 0;
                for (int i = 0; i < south.hand.Count; i++)
                {

                    if (south.hand.ElementAt(i).sama)
                    {
                        south.hand.ElementAt(i).eval += 30;
                    }


                }
                for (int i = 0; i < south.hand.Count; i++)
                {
                    if (south.hand.ElementAt(i).CardType == Card.Type.Nine)
                    {
                        nineCount++;
                    }
                }
                for (int j = 0; j < south.ReturnAllDoubleNines().Count; j++)
                {

                    Card.Suit nineSuit = south.ReturnAllDoubleNines().ElementAt(j);
                    bool aceExists = false;

                    List<Card.Type> typeList = new List<Card.Type>();

                    foreach (var card in south.hand)
                    {
                        if (card.CardSuit == nineSuit)
                        {
                            typeList.Add(card.CardType);
                        }
                        if (card.CardType == Card.Type.Ace && card.CardSuit == nineSuit)
                        {
                            aceExists = true;
                        }
                    }

                    if (aceExists)
                    {
                        Card.Type smallest = Card.Type.Ace;
                        for (int i = 0; i < typeList.Count; i++)
                        {
                            if (typeList.ElementAt(i) == Card.Type.Ten)
                            {
                                if (typeList.ElementAt(i) == Card.Type.King)
                                {
                                    if (typeList.ElementAt(i) == Card.Type.Queen)
                                    {
                                        smallest = Card.Type.Queen;
                                    }
                                    smallest = Card.Type.King;
                                }
                                smallest = Card.Type.Ten;
                            }
                        }
                        foreach (var card in south.hand)
                        {
                            if (card.CardSuit == nineSuit && card.CardType == smallest)
                            {
                                card.eval += 20;
                            }
                        }
                    }
                    else
                    {
                        Card smallest = null;
                        bool first = true;
                        bool king = false;
                        bool queen = false;
                        foreach (var card in south.hand)
                        {

                        }
                        for (int i = 0; i < south.hand.Count; i++)
                        {
                            if (south.hand.ElementAt(i).CardSuit == nineSuit)
                            {
                                if (first)
                                {
                                    smallest = south.hand.ElementAt(i);
                                    first = false;
                                }
                                else
                                {
                                    if (south.hand.ElementAt(i).pointValue < smallest.pointValue)
                                    {
                                        smallest = south.hand.ElementAt(i);
                                    }
                                }
                            }
                        }
                        foreach (var card in south.hand)
                        {
                            if (card.CardSuit == smallest.CardSuit && card.CardType == smallest.CardType)
                            {
                                card.eval += 15;
                            }
                        }
                    }

                }

            }
            else if (north.Equals(first))
            {
                for (int i = 0; i < south.hand.Count; i++)
                {
                    if (north.currentCard.vlastna)
                    {
                        if (south.BelotCheck(north.currentCard.CardSuit))
                        {
                            if (south.hand.ElementAt(i).CardType == Card.Type.King && north.currentCard.CardSuit == south.hand.ElementAt(i).CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 15;
                            }
                            else if (south.hand.ElementAt(i).CardType == Card.Type.Queen && north.currentCard.CardSuit == south.hand.ElementAt(i).CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 14;
                            }
                        }
                        else
                        {
                            List<Card> cards = new List<Card>();
                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (south.hand.ElementAt(j).CardSuit == north.currentCard.CardSuit)
                                {
                                    cards.Add(south.hand.ElementAt(0));
                                }
                            }

                            if (cards.Count == 0)
                            {

                            }

                            Card mostPoints = cards.ElementAt(0);
                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (south.hand.ElementAt(i + 1).pointValue > south.hand.ElementAt(i).pointValue)
                                {
                                    mostPoints = south.hand.ElementAt(i + 1);
                                }
                            }

                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (south.hand.ElementAt(j) == mostPoints)
                                {
                                    south.hand.ElementAt(j).eval += 12;
                                }
                            }

                        }
                    }
                }
                for (int i = 0; i < south.hand.Count; i++)
                {
                    Card.Suit a = north.currentCard.CardSuit;
                    if (!north.currentCard.vlastna)
                    {
                        if (south.hand.ElementAt(i).vlastna && south.hand.ElementAt(i).CardSuit == a)
                        {
                            south.hand.ElementAt(i).eval += 10;
                            if (south.hand.ElementAt(i + 1).CardSuit == a)
                            {
                                south.hand.ElementAt(i + 1).eval += 9;
                            }
                        }
                    }
                }

            }

            else if (west.Equals(first))
            {
                bool canAnswer = false;
                foreach (var card in south.hand)
                {
                    if (card.CardSuit == west.currentCard.CardSuit)
                    {
                        canAnswer = true;
                    }
                }
                if (!canAnswer)
                {
                    List<Card> dontPlay = new List<Card>();
                    if (south.DoubleNineCheck())
                    {
                        for (int i = 0; i < south.ReturnAllDoubleNines().Count; i++)
                        {
                            Card.Suit currentSuit = south.ReturnAllDoubleNines().ElementAt(i);
                            int counter = 0;
                            foreach (var card in south.hand)
                            {
                                if (card.CardSuit == currentSuit)
                                {
                                    counter++;
                                }
                                if (card.CardSuit == currentSuit && card.CardType == Card.Type.Nine)
                                {
                                    dontPlay.Add(card);
                                }
                            }
                            if (counter <= 2)
                            {
                                foreach (var card in south.hand)
                                {
                                    if (card.CardSuit == currentSuit && card.CardType != Card.Type.Nine)
                                    {
                                        dontPlay.Add(card);
                                    }
                                }
                                continue;
                            }
                            Card highest = null;
                            bool first = true;
                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (first && south.hand.ElementAt(j).CardSuit == currentSuit && south.hand.ElementAt(j).CardType != Card.Type.Nine)
                                {
                                    highest = south.hand.ElementAt(j);
                                    first = false;
                                }
                                else
                                {
                                    if (south.hand.ElementAt(j).pointValue != 14 && south.hand.ElementAt(j).pointValue > highest.pointValue)
                                    {
                                        highest = south.hand.ElementAt(j);
                                    }
                                }
                            }
                            dontPlay.Add(highest);

                        }
                        Card smallest = south.hand.ElementAt(0);
                        for (int j = 1; j < south.hand.Count; j++)
                        {
                            if (smallest.pointValue > south.hand.ElementAt(j).pointValue)
                            {
                                smallest = south.hand.ElementAt(j);
                            }
                        }
                        smallest.eval += 30;
                    }
                    if (south.TripleAceCheck())
                    {
                        for (int i = 0; i < south.ReturnAllTripleAces().Count; i++)
                        {
                            Card.Suit currentSuit = south.ReturnAllTripleAces().ElementAt(i);
                            int counter = 0;
                            foreach (var card in south.hand)
                            {
                                if (card.CardSuit == currentSuit)
                                {
                                    counter++;
                                }
                                if (card.CardSuit == currentSuit && card.CardType == Card.Type.Ace)
                                {
                                    dontPlay.Add(card);
                                }
                            }
                            if (counter <= 3)
                            {
                                foreach (var card in south.hand)
                                {
                                    if (card.CardSuit == currentSuit && card.CardType != Card.Type.Ace)
                                    {
                                        dontPlay.Add(card);
                                    }
                                }
                                continue;
                            }
                            Card highest = null;
                            Card secondHighest = null;
                            bool first = true;
                            bool second = true;
                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (first && south.hand.ElementAt(j).CardSuit == currentSuit && south.hand.ElementAt(j).CardType != Card.Type.Ace)
                                {
                                    highest = south.hand.ElementAt(j);
                                    first = false;
                                }
                                else
                                {
                                    if (south.hand.ElementAt(j).pointValue != 11 && south.hand.ElementAt(j).pointValue > highest.pointValue)
                                    {
                                        highest = south.hand.ElementAt(j);
                                    }
                                }
                            }
                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (second && south.hand.ElementAt(j).CardSuit == currentSuit && south.hand.ElementAt(j).CardType != Card.Type.Ace && south.hand.ElementAt(j).CardType != highest.CardType)
                                {
                                    secondHighest = south.hand.ElementAt(j);
                                    second = false;
                                }
                                else
                                {
                                    if (south.hand.ElementAt(j).pointValue != 11 && south.hand.ElementAt(j).pointValue != highest.pointValue && south.hand.ElementAt(j).pointValue > secondHighest.pointValue)
                                    {
                                        secondHighest = south.hand.ElementAt(j);
                                    }
                                }
                            }
                            dontPlay.Add(highest);
                            dontPlay.Add(secondHighest);
                        }
                        Card smallest = south.hand.ElementAt(0);
                        for (int j = 1; j < south.hand.Count; j++)
                        {
                            if (smallest.pointValue > south.hand.ElementAt(j).pointValue)
                            {
                                smallest = south.hand.ElementAt(j);
                            }
                        }
                        smallest.eval += 30;

                        foreach (var card in dontPlay)
                        {
                            card.eval = 0;
                        }
                    }
                    return;
                }
                if (west.currentCard.vlastna)
                {
                    if (south.BelotCheck(west.currentCard.CardSuit))
                    {
                        for (int i = 0; i < south.hand.Count; i++)
                        {
                            if (south.hand.ElementAt(i).CardType == Card.Type.King && west.currentCard.CardSuit == south.hand.ElementAt(i).CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 15;
                            }
                            else if (south.hand.ElementAt(i).CardType == Card.Type.Queen && west.currentCard.CardSuit == south.hand.ElementAt(i).CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 14;
                            }
                        }

                    }
                    if (south.DoubleNineCheck())
                    {
                        for (int i = 0; i < south.hand.Count; i++)
                        {
                            if (south.hand.ElementAt(i).CardType != Card.Type.Nine && south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 15;
                            }
                            else if (south.hand.ElementAt(i).CardType == Card.Type.Nine && south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 1;
                            }
                        }

                    }
                    if (south.TripleAceCheck())
                    {
                        for (int i = 0; i < south.hand.Count; i++)
                        {
                            if (south.hand.ElementAt(i).CardType == Card.Type.Ace && south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 1;
                            }
                            else if (south.hand.ElementAt(i).CardType != Card.Type.Ace && south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                            {
                                south.hand.ElementAt(i).eval += 15;
                            }
                        }

                    }


                    List<Card> lowerWest = new List<Card>();
                    for (int i = 0; i < south.hand.Count; i++)
                    {
                        if (south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                        {
                            lowerWest.Add(south.hand.ElementAt(i));
                        }

                    }
                    Card smallest = lowerWest.ElementAt(0);
                    for (int j = 1; j < lowerWest.Count; j++)
                    {
                        if (smallest.pointValue < lowerWest.ElementAt(j).pointValue)
                        {
                            smallest = lowerWest.ElementAt(j);
                        }
                    }
                    smallest.eval += 30;



                }
                else
                {

                    List<Card> westList = new List<Card>();
                    List<Card> lowerWest = new List<Card>();
                    for (int i = 0; i < south.hand.Count; i++)
                    {
                        if (south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                        {
                            if (south.hand.ElementAt(i).pointValue > west.currentCard.pointValue)
                            {
                                westList.Add(south.hand.ElementAt(i));
                            }
                            else if (west.currentCard.CardType == Card.Type.Seven)
                            {
                                westList.Add(south.hand.ElementAt(i));
                            }
                        }
                    }
                    if (westList.Count > 0)
                    {
                        Card smallest = westList.ElementAt(0);
                        for (int j = 1; j < westList.Count; j++)
                        {
                            if (smallest.pointValue < westList.ElementAt(j).pointValue)
                            {
                                smallest = westList.ElementAt(j);
                            }
                        }
                        smallest.eval += 30;
                    }
                    else
                    {
                        for (int i = 0; i < south.hand.Count; i++)
                        {
                            if (south.hand.ElementAt(i).CardSuit == west.currentCard.CardSuit)
                            {
                                lowerWest.Add(south.hand.ElementAt(i));
                            }

                        }
                        Card smallest = lowerWest.ElementAt(0);
                        for (int j = 1; j < lowerWest.Count; j++)
                        {
                            if (smallest.pointValue < lowerWest.ElementAt(j).pointValue)
                            {
                                smallest = lowerWest.ElementAt(j);
                            }
                        }
                        smallest.eval += 30;
                    }
                }



            }
            else if (east.Equals(first))
            {
                bool canAnswer = false;
                bool winning = false;
                Card highest;
                foreach (var card in south.hand)
                {
                    if (card.CardSuit == east.currentCard.CardSuit)
                    {
                        canAnswer = true;
                    }
                }
                if (!canAnswer)
                {
                    if (winning == true)
                    {
                        if (!south.DoubleNineCheck && !south.AllTrumpsCheck)
                        {
                            for (int i = 0; i < south.hand.Count; i++)
                            {
                                for (int j = 1; j < south.hand.Count; j++)
                                {
                                    if (south.hand.ElementAt(i).pointValue > south.hand.ElementAt(j).pointValue)
                                    {
                                        highest = south.hand.ElementAt(i);
                                    }
                                }
                            }
                            highest.eval = 30;
                        }
                    }
                }
                for (int i = 0; i < east.hand.Count; i++)
                {
                    if (east == winner || west == winner)
                    {
                        List<Card> cards = new List<Card>();
                        for (int j = 0; j < south.hand.Count; j++)
                        {
                            if (south.hand.ElementAt(j).CardSuit == north.currentCard.CardSuit)
                            {
                                cards.Add(south.hand.ElementAt(0));
                            }
                        }

                        if (cards.Count == 0)
                        {
                            Card leastPointsNoSuit = south.hand.ElementAt(0);
                            for (int j = 0; j < south.hand.Count; j++)
                            {
                                if (south.hand.ElementAt(i + 1).pointValue < south.hand.ElementAt(i).pointValue)
                                {
                                    leastPointsNoSuit = south.hand.ElementAt(i + 1);
                                }
                            }

                        }
                        Card leastPoints = cards.ElementAt(0);
                        for (int j = 0; j < south.hand.Count; j++)
                        {
                            if (south.hand.ElementAt(i + 1).pointValue < south.hand.ElementAt(i).pointValue)
                            {
                                leastPoints = south.hand.ElementAt(i + 1);
                            }
                        }

                        for (int j = 0; j < south.hand.Count; j++)
                        {
                            if (south.hand.ElementAt(j) == leastPoints)
                            {
                                south.hand.ElementAt(j).eval += 12;
                            }
                        }
                    }
                }
            }

        }

        public void AllTrumpsGamePlay()
        {
            for (int i = 0; i < 8; i++)
            {
                south.hand.ElementAt(i).MakeTrump();
            }

            for (int i = 0; i < south.hand.Count; i++)
            {
                EvaluateHandAllTrumps();
                Card mostPoints = south.hand.ElementAt(0);
                for (int j = 1; j < south.hand.Count; j++)
                {
                    if (south.hand.ElementAt(j).eval > mostPoints.eval)
                    {
                        mostPoints = south.hand.ElementAt(j);
                    }
                }
                Console.WriteLine($"Play {mostPoints}");
                south.hand.Remove(mostPoints);
                for (int j = 0; j < south.hand.Count; j++)
                {
                    south.hand.ElementAt(j).eval = 0;
                }
            }


        }
    }
}
