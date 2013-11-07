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
            player.CardInHand.ShouldEqual(exTopCard);
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

    [Subject("Player")]
    public class when_drawing_card_from_the_inside_of_the_deck : WithFakes
    {
        Establish context = () =>
        {
            IShuffler shuffler = An<IShuffler>();

            player = new Player();
            deck = new Deck(shuffler);

            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Spades";
            deck.Top["name"] = "Ace";

            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Hearts";
            deck.Top["name"] = "Queen";

            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Spades";
            deck.Top["name"] = "King";

            cardsCount = deck.Size;
            cardIndex = 1;
            cardToDraw = deck[1].DeepCopy();
        };

        Because of = () =>
        {
            player.DrawFrom(deck, cardIndex);
        };

        It should_have_this_card_in_hand = () =>
        {
            player.CardInHand.ShouldEqual(cardToDraw);
        };

        It should_have_removed_the_card_from_deck = () =>
        {
            deck.Size.ShouldEqual(cardsCount - 1);
            deck.Cards.ShouldNotContain(cardToDraw);
        };

        static Player player;
        static Deck deck;
        static Card cardToDraw;
        static int cardsCount;
        static int cardIndex;
    }
    
    [Subject("Player")]
    public class when_putting_a_card_in_a_deck : WithFakes
    {
        Establish context = () =>
        {
            IShuffler shuffler = An<IShuffler>();

            player = new Player();
            deck = new Deck(shuffler);

            player.CardInHand = new Card();
            player.CardInHand["suit"] = "Spades";
            player.CardInHand["name"] = "Ace";

            cardsCount = deck.Size;
            cardToPut = player.CardInHand.DeepCopy();
        };

        Because of = () =>
        {
            player.PutCardIn(deck);
        };

        It should_have_no_card_in_hand = () =>
        {
            player.CardInHand.ShouldBeNull();
        };

        It should_have_putted_the_card_in_a_deck = () =>
        {
            deck.Size.ShouldEqual(cardsCount + 1);
            deck.Cards.ShouldContain(cardToPut);
        };

        static Player player;
        static Deck deck;
        static Card cardToPut;
        static int cardsCount;
    }
}
