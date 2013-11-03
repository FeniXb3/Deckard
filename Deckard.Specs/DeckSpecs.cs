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
            cards = new List<Card>();

            newCard = new Card();
            newCard.Attributes.Add("suit", "Spades");
            cards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Clubs");
            cards.Add(newCard);

            deck.Cards = cards;
            deck2 = deck.DeepCopy();

            smallerDeck = deck.DeepCopy();
            smallerDeck.Cards.RemoveAt(0);

            biggerDeck = deck.DeepCopy();
            newCard = new Card();
            newCard.Attributes.Add("suit", "Hearts");
            biggerDeck.Cards.Add(newCard);



            differentCards = new List<Card>();
            
            newCard = new Card();
            newCard.Attributes.Add("suit", "Dimonds");
            newCard.Attributes.Add("name", "Queen");
            differentCards.Add(newCard);
            newCard = new Card();
            newCard.Attributes.Add("suit", "Spades");
            newCard.Attributes.Add("name", "1");
            differentCards.Add(newCard);

            differentDeck.Cards = differentCards;
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
        static List<Card> cards;
        static Card newCard;
        static List<Card> differentCards;
    }
}