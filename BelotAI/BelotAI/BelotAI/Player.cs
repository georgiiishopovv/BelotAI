using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace BelotAlgorithm
{
    class Player
    {
        public string position;
        public List<Card> hand;
        public string betting;
        public string calls;
        public string declarations;
        public string bid;
        public bool underHand;
        public int turn;
        public Card currentCard;

        Random rand = new Random();
        private List<Card> usedCards;

        public Player(string position)
        {
            this.position = position;
            this.bid = "Pass";
            this.underHand = true;

            this.usedCards = new List<Card>();

            this.hand = new List<Card>();
            hand.Add(new Card(Card.Type.Eight, Card.Suit.Clubs));
            hand.Add(new Card(Card.Type.Nine, Card.Suit.Clubs));
            hand.Add(new Card(Card.Type.Seven, Card.Suit.Clubs));
            hand.Add(new Card(Card.Type.Ten, Card.Suit.Clubs));
            hand.Add(new Card(Card.Type.Eight, Card.Suit.Spades));
            //DealRandomTillAmountOfCards(8);
        }

        public void InitialColourBetting()
        {
            int spades = 0;
            int diamonds = 0;
            int hearts = 0;
            int clubs = 0;

            for (int i = 0; i < hand.Count; i++)
            {
                if (hand.ElementAt(i).CardSuit == Card.Suit.Spades)
                {
                    spades++;
                }
                else if (hand.ElementAt(i).CardSuit == Card.Suit.Hearts)
                {
                    hearts++;
                }
                else if (hand.ElementAt(i).CardSuit == Card.Suit.Diamonds)
                {
                    diamonds++;
                }
                else
                {
                    clubs++;
                }
            }
            if (spades >= hearts && spades >= clubs && spades >= diamonds)
            {
                betting = "spades";
            }
            else if (hearts > spades && hearts >= diamonds && hearts >= clubs)
            {
                betting = "hearts";
            }
            else if (clubs > hearts && clubs > spades && clubs > diamonds)
            {
                betting = "clubs";
            }
            else
            {
                betting = "diamonds";
            }
        }

        public void Declare()
        {
            List<Card> spades = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> diamonds = new List<Card>();
            List<Card> clubs = new List<Card>();

            int[] spadesDeclarations = new int[3];
            int[] heartsDeclarations = new int[3];
            int[] diamondsDeclarations = new int[3];
            int[] clubsDeclarations = new int[3];

            for (int i = 0; i < hand.Count; i++)
            {
                switch (hand.ElementAt(i).CardSuit)
                {
                    case Card.Suit.Spades:
                        spades.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Hearts:
                        hearts.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Diamonds:
                        diamonds.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Clubs:
                        clubs.Add(hand.ElementAt(i));
                        break;
                }
            }

            List<Card.Type> kareTypes = KareCheck();
            for (int i = 0; i < kareTypes.Count; i++)
            {
                foreach (Card card in hand)
                {
                    if (card.CardType == kareTypes.ElementAt(i))
                    {
                        card.InKare = true;
                    }
                }
            }

            spadesDeclarations = CountDeclarations(spades);
            heartsDeclarations = CountDeclarations(hearts);
            diamondsDeclarations = CountDeclarations(diamonds);
            clubsDeclarations = CountDeclarations(clubs);

            DeclareKare(kareTypes);
            DeclareSpecificSuit(spadesDeclarations);
            DeclareSpecificSuit(heartsDeclarations);
            DeclareSpecificSuit(diamondsDeclarations);
            DeclareSpecificSuit(clubsDeclarations);


        }

        public void SortHand()
        {

            List<Card> spades = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> diamonds = new List<Card>();
            List<Card> clubs = new List<Card>();


            for (int i = 0; i < hand.Count; i++)
            {
                switch (hand.ElementAt(i).CardSuit)
                {
                    case Card.Suit.Spades:
                        spades.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Hearts:
                        hearts.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Diamonds:
                        diamonds.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Clubs:
                        clubs.Add(hand.ElementAt(i));
                        break;
                }
            }

            if (spades.Count != 0)
            {
                SortSuit(spades);
            }
            if (hearts.Count != 0)
            {
                SortSuit(hearts);
            }
            if (diamonds.Count != 0)
            {
                SortSuit(diamonds);
            }
            if (clubs.Count != 0)
            {
                SortSuit(clubs);
            }

            hand.Clear();
            hand.AddRange(spades);
            hand.AddRange(hearts);
            hand.AddRange(diamonds);
            hand.AddRange(clubs);

        }

        public void DealRandomTillAmountOfCards(int amount)
        {
            while (hand.Count < amount)
            {
                Card randomCard = new Card((Card.Type)rand.Next(8), (Card.Suit)rand.Next(4));

                for (int i = 0; i < usedCards.Count; i++)
                {
                    if (randomCard.CardType.Equals(usedCards.ElementAt(i).CardType))
                    {
                        if (randomCard.CardSuit.Equals(usedCards.ElementAt(i).CardSuit))
                        {
                            goto NextCard;
                        }
                    }
                }

                hand.Add(randomCard);
                usedCards.Add(randomCard);

            NextCard:;
            }

        }

        public int[] CountDeclarations(List<Card> currentCards)
        {
            int currentContinuousCount = 1;
            int tierceCount = 0;
            int quartaCount = 0;
            int quinteCount = 0;

            if (currentCards.Count > 2)
            {
                for (int i = 0; i < currentCards.Count - 1; i++)
                {
                    if ((int)currentCards.ElementAt(i + 1).CardType + 1 == (int)currentCards.ElementAt(i).CardType && currentCards.ElementAt(i).InKare == false)
                    {
                        currentContinuousCount++;
                    }
                    else
                    {
                        switch (currentContinuousCount)
                        {
                            case 3:
                                tierceCount++;
                                break;
                            case 4:
                                quartaCount++;
                                break;
                            case 5:
                                quinteCount++;
                                break;
                            case 6:
                                quinteCount++;
                                break;
                            case 7:
                                quinteCount++;
                                break;
                            case 8:
                                tierceCount++;
                                quinteCount++;
                                break;
                        }
                        currentContinuousCount = 1;
                    }
                }
                switch (currentContinuousCount)
                {
                    case 3:
                        tierceCount++;
                        break;
                    case 4:
                        quartaCount++;
                        break;
                    case 5:
                        quinteCount++;
                        break;
                    case 6:
                        quinteCount++;
                        break;
                    case 7:
                        quinteCount++;
                        break;
                    case 8:
                        tierceCount++;
                        quinteCount++;
                        break;
                }
            }
            return new int[] { tierceCount, quartaCount, quinteCount };
        }

        public void SortSuit(List<Card> givenCards)
        {

            for (int i = 0; i < givenCards.Count; i++)
            {
                for (int j = 0; j < givenCards.Count - 1; j++)
                {
                    if ((int)(givenCards.ElementAt(j).CardType) < (int)(givenCards.ElementAt(j + 1).CardType))
                    {
                        Card hold = givenCards.ElementAt(j + 1);
                        givenCards.RemoveAt(j + 1);
                        givenCards.Insert(j, hold);
                    }
                }
            }
        }

        public Card.Suit DoubleNineSuit()
        {
            Card.Suit nineSuit;
            foreach (var card in hand)
            {
                if (card.CardType.Equals(Card.Type.Nine))
                {
                    nineSuit = card.CardSuit;
                    foreach (var secondCard in hand)
                    {
                        if (secondCard.CardSuit == nineSuit && secondCard.CardType != card.CardType && secondCard.CardType != Card.Type.Jack)
                        {
                            return nineSuit;
                        }
                    }
                }
            }
            return Card.Suit.Pass;
        }

        public void DeclareSpecificSuit(int[] currentDeclarations)
        {
            for (int i = 0; i < currentDeclarations.Length; i++)
            {
                for (int j = 0; j < currentDeclarations[i]; j++)
                {
                    switch (i)
                    {
                        case 0:
                            Console.WriteLine("Tierce");
                            break;
                        case 1:
                            Console.WriteLine("50");
                            break;
                        case 2:
                            Console.WriteLine("100");
                            break;
                    }
                }
            }
        }
        public void DeclareKare(List<Card.Type> kareTypes)
        {
            for (int i = 0; i < kareTypes.Count; i++)
            {
                Console.WriteLine("Kare");
            }
        }

        public bool BelotCheck(Card.Suit currentSuit)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < hand.Count(); i++)
            {
                if (hand.ElementAt(i).CardSuit == currentSuit)
                {
                    cards.Add(hand.ElementAt(i));
                }
            }

            if (cards.Contains(new Card(Card.Type.King, currentSuit)) && cards.Contains(new Card(Card.Type.Queen, currentSuit)))
            {
                return true;
            }
            return false;
        }

        public List<Card.Type> KareCheck()
        {
            int[] cards = new int[8];
            List<Card.Type> types = new List<Card.Type>();

            for (int i = 0; i < this.hand.Count; i++)
            {
                int index = (int)this.hand.ElementAt(i).CardType;
                cards[index]++;
            }
            for (int i = 2; i < cards.Length; i++)
            {
                if (cards[i] == 4)
                {
                    types.Add((Card.Type)i);
                }
            }

            return types;
        }

        public Card.Suit LongestSuit()
        {
            int clubs = 0;
            int diamonds = 0;
            int hearts = 0;
            int spades = 0;
            for (int i = 0; i < hand.Count(); i++)
            {
                switch (hand.ElementAt(i).CardSuit)
                {
                    case Card.Suit.Clubs:
                        clubs++;
                        break;
                    case Card.Suit.Hearts:
                        hearts++;
                        break;
                    case Card.Suit.Diamonds:
                        diamonds++;
                        break;
                    case Card.Suit.Spades:
                        spades++;
                        break;
                }
            }

            int max = Math.Max(Math.Max(Math.Max(clubs, diamonds), hearts), spades);

            if (max == 2)
            {
                return Card.Suit.Pass;
            }

            if (clubs == max)
            {
                return Card.Suit.Clubs;
            }
            if (diamonds == max)
            {
                return Card.Suit.Diamonds;
            }
            if (hearts == max)
            {
                return Card.Suit.Hearts;
            }
            if (spades == max)
            {
                return Card.Suit.Spades;
            }
            return Card.Suit.Pass;

        }


        public int HandSize()
        {
            return this.hand.Count();
        }

        public void PrintCard(int i)
        {
            Console.WriteLine(hand.ElementAt(i).ToString());
        }


        public void AllTrumpsCheck()
        {
            int jackCount = 0;
            int[] declarations = new int[3];

            List<Card> spades = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> diamonds = new List<Card>();
            List<Card> clubs = new List<Card>();


            for (int i = 0; i < hand.Count; i++)
            {
                switch (hand.ElementAt(i).CardSuit)
                {
                    case Card.Suit.Spades:
                        spades.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Hearts:
                        hearts.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Diamonds:
                        diamonds.Add(hand.ElementAt(i));
                        break;
                    case Card.Suit.Clubs:
                        clubs.Add(hand.ElementAt(i));
                        break;
                }
            }

            if (spades.Count >= 4)
            {
                declarations = CountDeclarations(spades);
            }
            else if (hearts.Count >= 4)
            {
                declarations = CountDeclarations(hearts);
            }
            else if (diamonds.Count >= 4)
            {
                declarations = CountDeclarations(diamonds);
            }
            else if (clubs.Count >= 4)
            {
                declarations = CountDeclarations(clubs);
            }

            foreach (var card in hand)
            {
                if (card.CardType.Equals((Card.Type)4))
                {
                    jackCount++;
                }
            }


            if (jackCount >= 3 || (jackCount >= 2 && (JackNineCheck() || DoubleNineCheck() || JackKingQueenCheck())) || (jackCount >= 1 && (declarations[1] == 1 || declarations[2] == 1)))
            {
                bid = "All Trumps";
            }
        }

        public bool JackNineCheck()
        {
            bool hasJack = false;
            bool hasNine = false;
            bool suited = false;

            Card jack = null;
            Card nine = null;
            foreach (var card in hand)
            {
                if (card.CardType.Equals((Card.Type)4))
                {
                    hasJack = true;
                    jack = card;
                }

                if (card.CardType.Equals((Card.Type)2))
                {
                    hasNine = true;
                    nine = card;
                }

                if (hasJack && hasNine && jack.CardSuit.Equals(nine.CardSuit))
                {
                    suited = true;
                }
            }
            return suited;
        }
        public Card.Suit JackNineSuitCheck()
        {
            bool hasJack = false;
            bool hasNine = false;
            bool suited = false;

            Card jack = null;
            Card nine = null;
            foreach (var card in hand)
            {
                if (card.CardType.Equals((Card.Type)4))
                {
                    hasJack = true;
                    jack = card;
                }

                if (card.CardType.Equals((Card.Type)2))
                {
                    hasNine = true;
                    nine = card;
                }

                if (hasJack && hasNine && jack.CardSuit.Equals(nine.CardSuit))
                {
                    suited = true;
                }
            }
            if (suited)
            {
                return jack.CardSuit;
            }
            return Card.Suit.Pass;
            
        }

        public bool DoubleNineCheck()
        {
            Card.Suit nineSuit;
            foreach (var card in hand)
            {
                if (card.CardType.Equals(Card.Type.Nine))
                {
                    nineSuit = card.CardSuit;
                    foreach (var secondCard in hand)
                    {
                        if (secondCard.CardSuit == nineSuit && secondCard.CardType != card.CardType && secondCard.CardType != Card.Type.Jack)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool DoubleNineCheck(Card.Suit card)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand.ElementAt(i).CardType == Card.Type.Nine)
                {
                    Card.Suit a = hand.ElementAt(i).CardSuit;
                    foreach (var secondCard in hand)
                    {
                        if (secondCard.CardSuit == a && secondCard.CardType != Card.Type.Nine && secondCard.CardType != Card.Type.Jack && a != card)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public List<Card.Suit> ReturnAllDoubleNines()
        {
            List<Card> nines = new List<Card>();
            List<Card.Suit> doubleNineSuits = new List<Card.Suit>();
            foreach (var card in hand)
            {
                if (card.CardType == Card.Type.Nine)
                {
                    nines.Add(card);
                }
            }

            for (int i = 0; i < nines.Count; i++)
            {
                foreach (var secondCard in hand)
                {
                    if (secondCard.CardSuit == nines.ElementAt(i).CardSuit && secondCard.CardType != Card.Type.Jack && secondCard.CardType != Card.Type.Nine)
                    {
                        doubleNineSuits.Add(nines.ElementAt(i).CardSuit);
                    }
                }
            }

            return doubleNineSuits;
        }
        public bool JackKingQueenCheck()
        {

            for (int i = 2; i < hand.Count(); i++)
            {
                Card card = hand.ElementAt(i);
                if (card.CardType.Equals(Card.Type.Jack))
                {
                    Card kingCheck = hand.ElementAt(i - 2);
                    if (kingCheck.CardType.Equals(Card.Type.King) && kingCheck.CardSuit.Equals(card.CardSuit))
                    {
                        Card queenCheck = hand.ElementAt(i - 1);
                        if (queenCheck.CardType.Equals(Card.Type.Queen) && queenCheck.CardSuit.Equals(card.CardSuit))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        public bool TripleAceCheck()
        {
            for (int i = 0; i < hand.Count(); i++)
            {
                Card aceCheck = hand.ElementAt(i);
                if (aceCheck.CardType.Equals(Card.Type.Ace))
                {
                    Card tenCheck = hand.ElementAt(i + 1);
                    if (tenCheck.CardType != Card.Type.Nine && tenCheck.CardType != Card.Type.Jack && tenCheck.CardSuit.Equals(aceCheck.CardSuit))
                    {
                        Card zeroCheck = hand.ElementAt(i + 2);
                        if (zeroCheck.CardType != Card.Type.Nine && zeroCheck.CardType != Card.Type.Jack && zeroCheck.CardSuit.Equals(aceCheck.CardSuit))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool TripleAceCheck(Card.Suit card)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand.ElementAt(i).CardType == Card.Type.Ace)
                {
                    Card.Suit a = hand.ElementAt(i).CardSuit;
                    Card aceCheck = hand.ElementAt(i);
                    if (aceCheck.CardType.Equals(Card.Type.Ace))
                    {
                        Card tenCheck = hand.ElementAt(i + 1);
                        if (tenCheck.CardType != Card.Type.Nine && tenCheck.CardType != Card.Type.Jack && tenCheck.CardSuit.Equals(a))
                        {
                            Card zeroCheck = hand.ElementAt(i + 2);
                            if (zeroCheck.CardType != Card.Type.Nine && zeroCheck.CardType != Card.Type.Jack && zeroCheck.CardSuit.Equals(a))
                            {
                                return true;
                            }

                        }

                    }

                }
                return false;
            }
            return false;
        }

        public List<Card.Suit> ReturnAllTripleAces()
        {
            List<Card> aces = new List<Card>();
            List<Card.Suit> suits = new List<Card.Suit>();

            foreach (var card in hand)
            {
                if (card.CardType == Card.Type.Ace)
                {
                    aces.Add(card);
                }
            }
            for (int i = 0; i < aces.Count; i++)
            {
                Card second = null;
                Card third = null;
                
                foreach(var sc in hand)
                {
                    if (sc.CardSuit == aces.ElementAt(i).CardSuit && sc.CardType != Card.Type.Nine && sc.CardType != Card.Type.Jack)
                    {
                        second = sc;
                    }
                }
                foreach (var tc in hand)
                {
                    if (tc.CardSuit == aces.ElementAt(i).CardSuit && tc.CardType != Card.Type.Nine && tc.CardType != Card.Type.Jack && tc.CardType != second.CardType)
                    {
                        third = tc;
                    }
                }

                if (second != null && third != null)
                {
                    suits.Add(aces.ElementAt(i).CardSuit);
                }
            }

            return suits;
        }

        public string Bid()
        {
            SuitCheck();
            NoTrumpsCheck();
            AllTrumpsCheck();
            return this.bid;
        }

        public void SuitCheck()
        {
            List<Card> jacks = new List<Card>();
            List<Card> nines = new List<Card>();
            List<Card> aces = new List<Card>();
            List<Card> tens = new List<Card>();
            List<Card> kings = new List<Card>();
            List<Card> queens = new List<Card>();

            foreach (Card card in hand)
            {
                switch (card.CardType)
                {
                    case Card.Type.Jack:
                        jacks.Add(card);
                        break;
                    case Card.Type.Nine:
                        nines.Add(card);
                        break;
                    case Card.Type.Ace:
                        aces.Add(card);
                        break;
                    case Card.Type.Ten:
                        tens.Add(card);
                        break;
                    case Card.Type.King:
                        kings.Add(card);
                        break;
                    case Card.Type.Queen:
                        queens.Add(card);
                        break;
                }
            }


            if (jacks.Count >= 1 && nines.Count >= 1)
            {
                for (int i = 0; i < jacks.Count; i++)
                {
                    for (int j = 0; j < nines.Count; j++)
                    {
                        if (jacks.ElementAt(i).CardSuit == nines.ElementAt(j).CardSuit)
                        {
                            bid = jacks.ElementAt(i).CardSuit.ToString();
                        }
                    }
                }
            }



            if (jacks.Count >= 1 && tens.Count >= 1 && aces.Count >= 1)
            {
                for (int i = 0; i < jacks.Count; i++)
                {
                    for (int j = 0; j < aces.Count; j++)
                    {
                        for (int a = 0; a < tens.Count; a++)
                        {
                            if (jacks.ElementAt(i).CardSuit == aces.ElementAt(j).CardSuit && jacks.ElementAt(i).CardSuit == tens.ElementAt(a).CardSuit)
                            {
                                bid = jacks.ElementAt(i).CardSuit.ToString();
                            }
                        }
                    }
                }
            }



            if (jacks.Count >= 1 && kings.Count >= 1 && queens.Count >= 1)
            {
                for (int i = 0; i < jacks.Count; i++)
                {
                    for (int j = 0; j < kings.Count; j++)
                    {
                        for (int a = 0; a < queens.Count; a++)
                        {
                            if (jacks.ElementAt(i).CardSuit == kings.ElementAt(j).CardSuit && jacks.ElementAt(i).CardSuit == queens.ElementAt(a).CardSuit)
                            {
                                bid = jacks.ElementAt(i).CardSuit.ToString();
                            }
                        }
                    }
                }
            }

            List<Card> spades = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> diamonds = new List<Card>();
            List<Card> clubs = new List<Card>();

            foreach (Card card in hand)
            {
                switch (card.CardSuit)
                {
                    case Card.Suit.Spades:
                        spades.Add(card);
                        break;
                    case Card.Suit.Hearts:
                        hearts.Add(card);
                        break;
                    case Card.Suit.Diamonds:
                        diamonds.Add(card);
                        break;
                    case Card.Suit.Clubs:
                        clubs.Add(card);
                        break;
                }

            }
            if (spades.Count >= 4)
            {
                if (CountDeclarations(spades)[1] >= 1 || CountDeclarations(spades)[2] >= 1)
                {
                    bid = "Spades";
                }
            }
            else if (hearts.Count >= 4)
            {
                if (CountDeclarations(hearts)[1] >= 1 || CountDeclarations(hearts)[2] >= 1)
                {
                    bid = "Hearts";
                }
            }
            else if (diamonds.Count >= 4)
            {
                if (CountDeclarations(diamonds)[1] >= 1 || CountDeclarations(diamonds)[2] >= 1)
                {
                    bid = "Diamonds";
                }
            }
            else if (clubs.Count >= 4)
            {
                if (CountDeclarations(clubs)[1] >= 1 || CountDeclarations(clubs)[2] >= 1)
                {
                    bid = "Clubs";
                }
            }


        }

        public void NoTrumpsCheck()
        {
            List<Card> aces = new List<Card>();
            List<Card> tens = new List<Card>();
            List<Card> kings = new List<Card>();

            foreach (var card in hand)
            {
                if (card.CardType.Equals((Card.Type)7))
                {
                    aces.Add(card);
                }
                else if (card.CardType.Equals(Card.Type.Ten))
                {
                    tens.Add(card);
                }
                else if (card.CardType.Equals(Card.Type.King))
                {
                    kings.Add(card);
                }
            }
            if (aces.Count >= 3)
            {
                bid = "No Trumps";
            }

            foreach (var card in hand)
            {
                if (aces.Count >= 2 && tens.Count >= 1 & underHand)
                {
                    for (int i = 0; i < tens.Count; i++)
                    {
                        for (int j = 0; j < aces.Count; j++)
                        {
                            if (aces.ElementAt(j).CardSuit == tens.ElementAt(i).CardSuit)
                            {
                                bid = "No Trumps";
                            }
                        }

                    }
                }
            }

            foreach (var card in hand)
            {

                if (tens.Count >= 1 && aces.Count >= 2 && underHand)
                {
                    for (int i = 0; i < tens.Count; i++)
                    {
                        if (tens.ElementAt(i).CardSuit == card.CardSuit && underHand)
                        {
                            bid = "No Trumps";
                        }
                    }
                }
            }

            foreach (var card in hand)
            {
                bool aceTen = false;
                bool tenOne = false;
                Card ten = null;
                if (tens.Count >= 2 && aces.Count >= 1)
                {
                    for (int i = 0; i < tens.Count; i++)
                    {
                        for (int j = 0; j < aces.Count; j++)
                        {
                            if (aces.ElementAt(j).CardSuit.Equals(tens.ElementAt(i).CardSuit))
                            {
                                aceTen = true;
                                ten = card;
                            }
                            if (tens.ElementAt(i).CardSuit == card.CardSuit && tens.ElementAt(i) != ten)
                            {
                                tenOne = true;
                            }
                        }

                    }
                }
                if (aceTen && tenOne && underHand)
                {
                    bid = "No Trumps";
                }
            }


            foreach (var card in hand)
            {
                if (tens.Count >= 1 && aces.Count >= 1 && kings.Count >= 1)
                {
                    for (int i = 0; i < aces.Count; i++)
                    {
                        for (int j = 0; j < tens.Count; j++)
                        {
                            for (int a = 0; a < kings.Count; a++)
                            {
                                if (aces.ElementAt(i).CardSuit == tens.ElementAt(j).CardSuit && aces.ElementAt(i).CardSuit == kings.ElementAt(a).CardSuit && underHand)
                                {
                                    bid = "No Trumps";
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SuitSuitAllTrumps()
        {
            string teammateBid = Console.ReadLine();
            if (teammateBid != "No Trumps" && teammateBid != "All Trumps" && teammateBid != "Pass" && bid != "Pass" && teammateBid != bid)
            {
                bid = "All Trumps";
            }
        }
        public void BelotCheck(Card card)
        {

            if (card.CardType.Equals(Card.Type.King) && card.CardType.Equals(Card.Type.Queen))
            {
                string belotCheck = "Belot";
            }
        }


        public int Turn
        {
            get
            {
                return turn;
            }
            set
            {
                turn = value;
            }
        }


        public string PlayerBid
        {
            get
            {
                return bid;
            }
            set
            {
                bid = value;
            }
        }


        public void PointsCount(Card card)
        {
            if (bid == "All Trumps")
            {
                int points = 0;
                switch (card.CardType)
                {
                    case Card.Type.Seven:
                        points += 0;
                        break;
                    case Card.Type.Eight:
                        points += 0;
                        break;
                    case Card.Type.Nine:
                        points += 14;
                        break;
                    case Card.Type.Ten:
                        points += 10;
                        break;
                    case Card.Type.Jack:
                        points += 20;
                        break;
                    case Card.Type.Queen:
                        points += 3;
                        break;
                    case Card.Type.King:
                        points += 4;
                        break;
                    case Card.Type.Ace:
                        points += 11;
                        break;
                }
                Console.WriteLine($"The total points in your hand are: {points}");
            }
            if (bid == "No Trumps")
            {
                int points = 0;
                switch (card.CardType)
                {
                    case Card.Type.Seven:
                        points += 0;
                        break;
                    case Card.Type.Eight:
                        points += 0;
                        break;
                    case Card.Type.Nine:
                        points += 0;
                        break;
                    case Card.Type.Ten:
                        points += 10;
                        break;
                    case Card.Type.Jack:
                        points += 2;
                        break;
                    case Card.Type.Queen:
                        points += 3;
                        break;
                    case Card.Type.King:
                        points += 4;
                        break;
                    case Card.Type.Ace:
                        points += 11;
                        break;
                }
                Console.WriteLine($"The total points in your hand are: {points}");
            }
            else
            {
                int points = 0;
                switch (card.CardType)
                {
                    case Card.Type.Seven:
                        points += 0;
                        break;
                    case Card.Type.Eight:
                        points += 0;
                        break;
                    case Card.Type.Nine:
                        if (card.CardSuit.ToString() == bid)
                        {
                            points += 14;
                        }
                        else
                        {
                            points += 0;
                        }
                        break;
                    case Card.Type.Ten:
                        points += 10;
                        break;
                    case Card.Type.Jack:
                        if (card.CardSuit.ToString() == bid)
                        {
                            points += 20;
                        }
                        else
                        {
                            points += 0;
                        }
                        break;
                    case Card.Type.Queen:
                        points += 3;
                        break;
                    case Card.Type.King:
                        points += 4;
                        break;
                    case Card.Type.Ace:
                        points += 11;
                        break;
                }
                Console.WriteLine($"The total points in your hand are: {points}");
            }
        }
    }
}


/*
public int BiddingProbability()
{
    int allTrumpsValue = 0;
    int noTrumpsValue = 0;
    int spadesValue = 0;
    int diamondsValue = 0;
    int heartsValue = 0;
    int clubsValue = 0;


    for (int i = 0; i < hand.Count; i++)
    {
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Jack, Card.Suit.Clubs))
        {
            clubsValue += 7;
            allTrumpsValue += 5;
            noTrumpsValue -= 1;

        }
         if (hand.ElementAt(i).CardType.Equals(Card.Type.Jack, Card.Suit.Spades))
        {
            spadesValue += 7;
            allTrumpsValue += 5;
            noTrumpsValue -= 1;
        }
         if (hand.ElementAt(i).CardType.Equals(Card.Type.Jack, Card.Suit.Hearts))
        {
            heartsValue += 7;
            allTrumpsValue += 5;
            noTrumpsValue -= 1;
        }
         if (hand.ElementAt(i).CardType.Equals(Card.Type.Jack, Card.Suit.Diamonds))
        {
            diamondsValue += 7;
            allTrumpsValue += 5;
            noTrumpsValue -= 1;
        }
         if (hand.ElementAt(i).CardType.Equals(Card.Type.Nine, Card.Suit.Clubs))
        {
            clubsValue += 5;
            allTrumpsValue += 3;
            noTrumpsValue -= 2;
        }
          if (hand.ElementAt(i).CardType.Equals(Card.Type.Nine, Card.Suit.Hearts))
        {
            heartsValue += 5;
            allTrumpsValue += 3;
            noTrumpsValue -= 2;
        }
          if (hand.ElementAt(i).CardType.Equals(Card.Type.Nine, Card.Suit.Spades))
        {
            spadesValue += 5;
            allTrumpsValue += 3;
            noTrumpsValue -= 2;
        }
          if (hand.ElementAt(i).CardType.Equals(Card.Type.Nine, Card.Suit.Diamonds))
        {
            diamondsValue += 5;
            allTrumpsValue += 3;
            noTrumpsValue -= 2;
        }

        if (hand.ElementAt(i).CardType.Equals(Card.Type.Ace, Card.Suit.Spades))
        {
            noTrumpsValue += 7;
            clubsValue, heartsValue, diamondsValue += 2;
            spadesValue += 3;
        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Ace, Card.Suit.Hearts))
        {
            noTrumpsValue += 7;
            clubsValue, spadesValue, diamondsValue += 2;
            heartsValue += 3;
        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Ace, Card.Suit.Clubs))
        {
            noTrumpsValue += 7;
            spadesValue, heartsValue, diamondsValue += 2;
             clubsValue += 3;
        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Ace, Card.Suit.Diamonds))
        {
            noTrumpsValue += 7;
            clubsValue, heartsValue, spadesValue += 2;
             diamondsValue += 3;
        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Queen, Card.Suit.Clubs))
        {
            clubsValue += 2;

        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Queen, Card.Suit.Spades))
        {
            spadesValue += 2;

        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Queen, Card.Suit.Hearts))
        {
            heartsValue += 2;

        }
        if (hand.ElementAt(i).CardType.Equals(Card.Type.Queen, Card.Suit.Diamonds))
        {
               if (hand.ElementAt(i).CardType.Equals(Card.Type.Queen, Card.Suit.Diamonds) && hand.ElementAt(i).CardType.Equals(Card.Type.King, Card.Suit.Diamonds && hand.ElementAt(i).CardType.Equals(Card.Type.Ace, Card.Suit.Diamonds)
               {
                    diamondsValue += 7;
                    noTrumpsValue += 1;
               }
               else if (hand.ElementAt(i).CardType.Equals(Card.Type.Queen, Card.Suit.Diamonds) && hand.ElementAt(i).CardType.Equals(Card.Type.King, Card.Suit.Diamonds)
               {
                    diamondsValue += 4;
               }


        }


    }
    return 0;
}
*/