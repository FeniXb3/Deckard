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
            differentCard = new Card();
            cardWithLessAttributes = new Card();
            cardWithDifferentAttributes = new Card();
        };

        Because of = () =>
        {
            card1.Attributes.Add("suit", "Hearts");
            card1.Attributes.Add("name", "Queen");

            card2.Attributes.Add("suit", "Hearts");
            card2.Attributes.Add("name", "Queen");

            differentCard.Attributes.Add("suit", "Spades");
            differentCard.Attributes.Add("name", "Ace");

            cardWithLessAttributes.Attributes.Add("suit", "Hearts");
            
            cardWithDifferentAttributes.Attributes.Add("color", "Hearts");
            cardWithDifferentAttributes.Attributes.Add("value", "Queen");
        };


        It should_not_be_equal_if_quantity_of_attributes_is_different = () =>
        {
            card1.ShouldNotEqual(cardWithLessAttributes);
        };

        It should_not_be_equal_to_card_with_different_names_of_attributes = () =>
        {
            card1.ShouldNotEqual(cardWithDifferentAttributes);
        };

        It should_not_be_equal_to_card_with_different_values_of_attributes = () =>
        {
            card1.ShouldNotEqual(differentCard);
        };

        It should_be_equal_to_card_with_the_same_attributes = () =>
        {
            card1.ShouldEqual(card2);
        };

        static Card card2;
        static Card card1;
        static Card differentCard;
        static Card cardWithLessAttributes;
        static Card cardWithDifferentAttributes;
    }
}
