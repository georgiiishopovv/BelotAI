using System;
using System.Collections.Generic;
using System.Text;

namespace BelotAlgorithm
{
    class Card
    {
        public int pointValue;
        public bool trump;
        public bool inKare;
        public int eval;
        public bool vlastna;
        public bool sama;

        public enum Suit
        {
            Clubs = 0,
            Diamonds = 1,
            Hearts = 2,
            Spades = 3,
            Pass = 4
        }
        private Suit cardSuit;

        public enum Type
        {
            Seven = 0,
            Eight = 1,
            Nine = 2,
            Ten = 3,
            Jack = 4,
            Queen = 5,
            King = 6,
            Ace = 7
        }
        private Type cardType;

        public Card(Type cardType, Suit cardSuit)
        {
            this.trump = false;
            this.inKare = false;
            this.cardType = cardType;
            this.cardSuit = cardSuit;

            AssignValue();

        }

        public void MakeTrump()
        {
            this.trump = true;
            AssignValue();
        }

        public void AssignValue()
        {
            switch (this.cardType)
            {
                case Type.Seven:
                    this.pointValue = 0;
                    break;

                case Type.Eight:
                    this.pointValue = 0;
                    break;

                case Type.Nine:
                    if (this.trump)
                    {
                        this.pointValue = 14;
                    }
                    else
                    {
                        this.pointValue = 0;
                    }
                    break;

                case Type.Ten:
                    this.pointValue = 10;
                    break;

                case Type.Jack:
                    if (this.trump)
                    {
                        this.pointValue = 20;
                    }
                    else
                    {
                        this.pointValue = 2;
                    }
                    break;

                case Type.Queen:
                    this.pointValue = 3;
                    break;

                case Type.King:
                    this.pointValue = 4;
                    break;

                case Type.Ace:
                    this.pointValue = 11;
                    break;
            }
        }

        public bool InKare
        {
            get
            {
                return this.inKare;
            }
            set
            {
                this.inKare = value;
            }
        }
        public Suit CardSuit
        {
            get
            {
                return this.cardSuit;
            }
            set
            {
                this.cardSuit = value;
            }
        }

        public Type CardType
        {
            get
            {
                return this.cardType;
            }
            set
            {
                this.cardType = value;
            }
        }
        public override string ToString()
        {
            return $"{cardType} of {cardSuit}";
        }


    }

}