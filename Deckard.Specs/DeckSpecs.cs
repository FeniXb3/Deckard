using Machine.Specifications;
using System.Collections.Generic;

namespace Deckard.Specs
{
    [Subject("Deck")]
    public class when_shuffled
    {
        Establish context = () =>
        {
            shuffler = new RandomNumberSortShuffler();
            deck = new Deck(shuffler);
            deckBeforeShuffle = new Deck(shuffler);

            cards = new List<Card>();

            newCard = new Card();
            newCard.Attributes.Add("suit", "Spades");
            newCard.Attributes.Add("name", "Ace");
            cards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Hearts");
            newCard.Attributes.Add("name", "Queen");
            cards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Clubs");
            newCard.Attributes.Add("name", "King");
            cards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Dimonds");
            newCard.Attributes.Add("name", "Queen");
            cards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Spades");
            newCard.Attributes.Add("name", "1");
            cards.Add(newCard);

            deck.Cards = cards;
            deckBeforeShuffle.Cards = cards;
        };
        
        Because of = () =>
        {
            deck.Shuffle();
        };

        It should_be_different_than_before_shuffle = () =>
        {
            deck.ShouldNotEqual(deckBeforeShuffle);
        };

        static Deck deck;
        static Deck deckBeforeShuffle;
        static IShuffler shuffler;
        static List<Card> cards;
        static Card newCard;
    }

    [Subject("Deck")]
    public class when_compared_with_another_deck
    {
        Establish context = () =>
        {
            deck = new Deck(shuffler);
            deck2 = new Deck(shuffler);
        };

        Because of = () =>
        {
            cards = new List<Card>();

            newCard = new Card();
            newCard.Attributes.Add("suit", "Spades");
            cards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Clubs");
            cards.Add(newCard);

            deck.Cards = cards;
            deck2.Cards = cards;
        };

        It should_be_equal_if_cards_are_in_the_same_order = () =>
        {
            deck.ShouldEqual(deck2);
        };

        static Deck deck;
        static Deck deck2;
        static IShuffler shuffler;
        static List<Card> cards;
        static Card newCard;
    }
}