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

            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Spades";
            deck.Cards[deck.Cards.Count - 1]["name"] = "Ace";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Hearts";
            deck.Cards[deck.Cards.Count - 1]["name"] = "Queen";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Clubs";
            deck.Cards[deck.Cards.Count - 1]["name"] = "King";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Dimonds";
            deck.Cards[deck.Cards.Count - 1]["name"] = "Queen";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Spades";
            deck.Cards[deck.Cards.Count - 1]["name"] = "1";

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
    }

    [Subject("Shuffler")]
    public class when_used_Knuth_Fisher_Yates_shuffler_to_shuffle_deck
    {
        Establish context = () =>
        {
            shuffler = new KnuthFisherYatesShuffler();
            deck = new Deck(shuffler);

            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Spades";
            deck.Cards[deck.Cards.Count - 1]["name"] = "Ace";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Hearts";
            deck.Cards[deck.Cards.Count - 1]["name"] = "Queen";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Clubs";
            deck.Cards[deck.Cards.Count - 1]["name"] = "King";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Dimonds";
            deck.Cards[deck.Cards.Count - 1]["name"] = "Queen";
            deck.Cards.Add(new Card());
            deck.Cards[deck.Cards.Count - 1]["suit"] = "Spades";
            deck.Cards[deck.Cards.Count - 1]["name"] = "1";

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
    }
}
