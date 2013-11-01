using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Deck
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
    }
}
