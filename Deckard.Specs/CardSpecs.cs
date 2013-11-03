using Machine.Specifications;
using System.Collections.Generic;

namespace Deckard.Specs
{
    [Subject("Card")]
    public class when_attributes_are_set
    {
        Establish context = () => 
        {
            card = new Card();
        };

        Because of = () =>
        {
            card["suit"] = "Spades";
            card["name"] = "Ace";
        };

        It should_contain_attributes_with_the_values_set = () =>
        {
            card["suit"].ShouldEqual("Spades");
            card["name"].ShouldEqual("Ace");
        };

        static Card card;
    }


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
            card1["suit"] = "Hearts";
            card1["name"] = "Queen";

            card2 = card1.DeepCopy();
            differentCard = card1.DeepCopy();

            differentCard["suit"] = "Spades";
            differentCard["name"] = "Ace";
            
            cardWithLessAttributes = card1.DeepCopy();
            cardWithLessAttributes.Attributes.Remove("name");

            cardWithDifferentAttributes["color"] = "Hearts";
            cardWithDifferentAttributes["value"] = "Queen";
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
