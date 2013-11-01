using Machine.Specifications;

namespace Deckard.Specs
{
    [Subject("Deck")]
    public class when_shuffled
    {
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
    }
}