using Machine.Specifications;
using System.Collections.Generic;

namespace Deckard.Specs
{
    [Subject("Deck")]
    public class when_compared_with_another_deck
    {
        Establish context = () =>
        {
            shuffler = new RandomNumberSortShuffler();

            deck = new Deck(shuffler);
            deck2 = new Deck(shuffler);
            differentDeck = new Deck(shuffler);
            smallerDeck = new Deck(shuffler);
            biggerDeck = new Deck(shuffler);
        };

        Because of = () =>
        {
            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Spades";
            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Dimonds";

            deck2 = deck.DeepCopy();

            smallerDeck = deck.DeepCopy();
            smallerDeck.Cards.RemoveAt(0);

            biggerDeck = deck.DeepCopy();
            biggerDeck.Cards.Add(new Card());
            biggerDeck.Top["suit"] = "Hearts";

            differentDeck.Cards.Add(new Card());
            differentDeck.Top["suit"] = "Dimonds";
            differentDeck.Top["name"] = "Queen";
            differentDeck.Cards.Add(new Card());
            differentDeck.Top["suit"] = "Spades";
            differentDeck.Top["name"] = "1";
        };

        It should_not_be_equal_if_cards_have_different_attributes = () =>
        {
            deck.ShouldNotEqual(differentDeck);
        };

        It should_not_be_equal_if_quantity_of_cards_is_different = () =>
        {
            deck.ShouldNotEqual(smallerDeck);
            deck.ShouldNotEqual(biggerDeck);
        };

        It should_be_equal_if_cards_are_in_the_same_order = () =>
        {
            deck.ShouldEqual(deck2);
        };

        static Deck deck;
        static Deck deck2;
        static Deck differentDeck;
        static Deck smallerDeck;
        static Deck biggerDeck;
        static IShuffler shuffler;
    }

    [Subject("Deck")]
    public class when_3_cards_are_added
    {
        Establish context = () =>
        {
            shuffler = new RandomNumberSortShuffler();
            deck = new Deck(shuffler);
        };

        Because of = () =>
        {
            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Dimonds";
            deck.Top["name"] = "Queen";
            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Spades";
            deck.Top["name"] = "1";
            deck.Cards.Add(new Card());
            deck.Top["suit"] = "Spades";
            deck.Top["name"] = "Ace";
        };
                
        It should_have_3_cards = () =>
        {
            deck.Cards.Count.ShouldEqual(3);
        };

        It should_have_the_last_card_on_top = () =>
        {
            deck.Top["suit"].ShouldEqual("Spades");
            deck.Top["name"].ShouldEqual("Ace");
        };

        It should_have_the_first_card_on_bottom = () =>
        {
            deck.Bottom["suit"].ShouldEqual("Dimonds");
            deck.Bottom["name"].ShouldEqual("Queen");
        };

        static Deck deck;
        static RandomNumberSortShuffler shuffler;
    }
}