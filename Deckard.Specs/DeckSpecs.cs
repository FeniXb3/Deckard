using Machine.Specifications;

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
    }

    [Subject("Deck")]
    public class when_compared_with_another_deck
    {
        Establish context = () =>
        {
            deck = new Deck(shuffler);
            deck2 = new Deck(shuffler);
        };

        It should_be_equal_if_cards_are_in_the_same_order = () =>
        {
            deck.ShouldEqual(deck2);
        };

        static Deck deck;
        static Deck deck2;
        static IShuffler shuffler;
    }
}