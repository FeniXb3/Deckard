using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Player
    {
        public Card CardInHand;
        public Dictionary<string, int> Attributes;
        /// <summary>
        /// Get or set value of the given attribute
        /// </summary>
        /// <param name="attributeName">Name of the attribute</param>
        /// <returns>Value of the given attribute</returns>
        public int this[string attributeName]
        {
            get { return Attributes[attributeName]; }
            set
            {
               // if (!Attributes.ContainsKey(attributeName))
               //     Attributes.Add(attributeName, value);
               // else
                    Attributes[attributeName] = value;
            }
        }

        public Player()
        {
            Attributes = new Dictionary<string, int>();
        }

        public void DrawFrom(Deck deck, int cardIndex = -1)
        {
            CardInHand = deck.TakeAndRemoveCard(cardIndex);
            CardInHand.OnDrawn(this, EventArgs.Empty);
        }

        public void PutCardIn(Deck deck)
        {
            deck.Cards.Add(CardInHand.DeepCopy());
            CardInHand = null;
        }

        public void PlayCard(Player targetPlayer = null)
        {
            if (targetPlayer == null)
                targetPlayer = this;

            CardActionEventArgs cae = new CardActionEventArgs(targetPlayer);
            CardInHand.OnPlayed(this, cae);
        }

        public Deck Hand { get; set; }
    }
}
