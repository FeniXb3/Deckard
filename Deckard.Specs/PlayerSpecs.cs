using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Machine.Fakes;
using Deckard;

namespace Deckard.Specs
{
    [Subject("Player")]
    public class when_drawing_card_from_top : WithFakes
    {
        Establish context = () =>
        {
            IShuffler shuffler = An<IShuffler>();

            player = new Player();
            deck = new Deck(shuffler);

            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Spades";
            deck.Top["name"] = "Ace";
            
            cardsCount = deck.Size;
            exTopCard = deck.Top.DeepCopy();
        };

        Because of = () =>
        {
            player.DrawFrom(deck);
        };
        
        It should_have_this_card_in_hand = () =>
        {
            player.DrawnCard.ShouldEqual(exTopCard);
        };

        It should_have_removed_the_card_from_deck = () =>
        {
            deck.Size.ShouldEqual(cardsCount - 1);
            deck.Cards.ShouldNotContain(exTopCard);
        };

        static Player player;
        static Deck deck;
        static Card exTopCard;
        static int cardsCount;
    }
}
