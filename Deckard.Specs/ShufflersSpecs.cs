using Machine.Specifications;
using System.Collections.Generic;

namespace Deckard.Specs
{
    [Subject("Shuffler")]
    public class when_used_random_number_sort_shuffler_to_shuffle_deck
    {
        Establish context = () =>
        {
            shuffler = new RandomNumberSortShuffler();
            deck = new Deck(shuffler);
            deckBeforeShuffle = deck.DeepCopy();

            cards = new List<Card>();

            newCard = new Card();
            newCard["suit"] = "Spades";
            newCard["name"] = "Ace";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Hearts";
            newCard["name"] = "Queen";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Clubs";
            newCard["name"] = "King";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Dimonds";
            newCard["name"] = "Queen";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Spades";
            newCard["name"] = "1";
            cards.Add(newCard);

            deck.Cards = cards;
            deckBeforeShuffle = deck.DeepCopy();
        };

        Because of = () =>
        {
            deck.Shuffle();
        };

        It should_change_the_card_order_in_deck = () =>
        {
            deck.ShouldNotEqual(deckBeforeShuffle);
        };

        static Deck deck;
        static Deck deckBeforeShuffle;
        static IShuffler shuffler;
        static List<Card> cards;
        static Card newCard;
    }

    [Subject("Shuffler")]
    public class when_used_Knuth_Fisher_Yates_shuffler_to_shuffle_deck
    {
        Establish context = () =>
        {
            shuffler = new KnuthFisherYatesShuffler();
            deck = new Deck(shuffler);
            deckBeforeShuffle = deck.DeepCopy();

            cards = new List<Card>();

            newCard = new Card();
            newCard["suit"] = "Spades";
            newCard["name"] = "Ace";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Hearts";
            newCard["name"] = "Queen";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Clubs";
            newCard["name"] = "King";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Dimonds";
            newCard["name"] = "Queen";
            cards.Add(newCard);
            newCard = new Card();
            newCard["suit"] = "Spades";
            newCard["name"] = "1";
            cards.Add(newCard);

            deck.Cards = cards;
            deckBeforeShuffle = deck.DeepCopy();
        };

        Because of = () =>
        {
            deck.Shuffle();
        };

        It should_change_the_card_order_in_deck = () =>
        {
            deck.ShouldNotEqual(deckBeforeShuffle);
        };

        static Deck deck;
        static Deck deckBeforeShuffle;
        static IShuffler shuffler;
        static List<Card> cards;
        static Card newCard;
    }
}
