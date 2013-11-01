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
    }
}
