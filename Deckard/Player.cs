using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Player
    {
        public Deck Hand { get; set; }
        public int CardsPlayed { get; set; }
        public Card CardInHand { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
        /// <summary>
        /// Get or set value of the given attribute
        /// </summary>
        /// <param name="attributeName">Name of the attribute</param>
        /// <returns>Value of the given attribute</returns>
        public object this[string attributeName]
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
            Attributes = new Dictionary<string, object>();
        }

        public void DrawFrom(Deck deck, int? cardIndex = null)
        {
            CardInHand = deck.TakeAndRemoveCard(cardIndex);
            CardInHand.OnDrawn(this, EventArgs.Empty);
        }

        public void PutCardIn(Deck deck)
        {
            deck.Cards.Add(CardInHand.DeepCopy());
            CardInHand = null;
        }

        public void PlayCard(Player targetPlayer = null, Deck targetDeck = null, CardActionEventArgs actionEventArgs = null)
        {
            if (targetPlayer == null)
                targetPlayer = this;

            CardActionEventArgs cae = actionEventArgs ?? new CardActionEventArgs(targetPlayer);
            CardInHand.OnPlayed(this, cae);

            if (targetDeck != null)
            {
                PutCardIn(targetDeck);
            }

            CardsPlayed++;
        }

        /// <summary>
        /// Draw card from deck and put it in hand
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cardIndex"></param>
        public void Draw(Deck source, int? cardIndex = null)
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
