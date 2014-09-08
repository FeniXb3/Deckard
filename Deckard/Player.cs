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

        public void PlayCard(Player targetPlayer = null, Deck targetDeck = null)
        {
            if (targetPlayer == null)
                targetPlayer = this;

            CardActionEventArgs cae = new CardActionEventArgs(targetPlayer);
            CardInHand.OnPlayed(this, cae);

            if (targetDeck != null)
            {
                PutCardIn(targetDeck);
            }

            CardsPlayed++;
        }

        public Deck Hand { get; set; }

        /// <summary>
        /// Draw card from deck and put it in hand
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cardIndex"></param>
        public void Draw(Deck source, int cardIndex = -1)
        {
            DrawFrom(source, cardIndex);
            PutCardIn(Hand);
        }

        public Card ChooseCardToPlay(Predicate<Card> match)
        {
            if (!Hand.Cards.Exists(match))
                throw new ArgumentException("The player does not have such card.");

            CardInHand = Hand.TakeAndRemoveCard(Hand.Cards.FindIndex(match));

            return CardInHand;
        }

        public int CardsPlayed { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("  Attributes count: {0}\n", Attributes.Count);

            foreach (var attr in Attributes)
            {

                sb.AppendFormat("  + {0} = {1}\n", attr.Key, attr.Value);
            }

            return sb.ToString();
        }
    }
}
