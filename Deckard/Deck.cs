using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Deck : IEquatable<Deck>
    {
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
            Cards = Shuffler.Shuffle(this);
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
    }
}
