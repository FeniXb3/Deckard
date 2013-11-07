using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Deck : IEquatable<Deck>
    {
        /// <summary>
        /// Gets the top card from a deck
        /// </summary>
        public Card Top
        {
            get { return (Cards.Count > 0) ? Cards[Cards.Count - 1] : null; }
        }

        /// <summary>
        /// Gets the bottom card from a deck
        /// </summary>
        public Card Bottom
        {
            get { return (Cards.Count > 0) ? Cards[0] : null; }
        }


        public int Size
        {
            get { return Cards.Count; }
        }

        public IShuffler Shuffler
        {
            set { shuffler = value; }
            get { return shuffler; }
        }
        public List<Card> Cards;

        private IShuffler shuffler;

        public Deck(IShuffler shuffler)
        {
            this.shuffler = shuffler;

            Cards = new List<Card>();
        }
        
        public void Shuffle()
        {
            Shuffler.Shuffle(this);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;

            sb.AppendFormat("Cards count: {0}\n", Cards.Count);
            foreach (var c in Cards)
            {
                sb.AppendFormat("- Card no. {0}:\n", index++);
                sb.AppendLine(c.ToString());
            }

            return sb.ToString();
        }

        public override bool Equals(object a)
        {
            if (a == null)
                return false;

            Deck other = a as Deck;

            if (this.Cards.Count != other.Cards.Count)
                return false;

            for (int i = 0; i < this.Cards.Count; i++)
            {
                if (!this.Cards[i].Equals(other.Cards[i]))
                    return false;
            }

            return true;
        }

        public bool Equals(Deck other)
        {
            if (other == null)
                return false;

            if (this.Cards.Count != other.Cards.Count)
                return false;

            for (int i = 0; i < this.Cards.Count; i++)
            {
                if (!this.Cards[i].Equals(other.Cards[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 23;

            foreach (var card in Cards)
            {
                hashCode += 3 * card.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Create a deep copy of deck
        /// </summary>
        /// <returns>Deep copy of deck</returns>
        public Deck DeepCopy()
        {
            Deck newDeck = this.MemberwiseClone() as Deck;
            newDeck.Cards = new List<Card>();

            foreach (var card in this.Cards)
            {
                newDeck.Cards.Add(card.DeepCopy());
            }

            return newDeck;
        }

    }
}
