using Machine.Specifications;

namespace Deckard.Specs
{
    [Subject("Card")]
    public class when_compared_with_another_card
    {
        Establish context = () =>
        {
            card1 = new Card();
            card2 = new Card();
        };

        It should_be_equal_to_card_with_same_attributes = () =>
        {
            card1.ShouldEqual(card2);
        };

        static Card card2;
        static Card card1;
    }
}
