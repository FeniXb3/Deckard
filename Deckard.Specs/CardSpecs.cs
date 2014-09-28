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

    [Subject(typeof(Card))]
    public class when_has_action_affecting_another_player_and_is_played
    {
        Establish context = () =>
        {
            lifesAttributeName = "lifes";
            player = new Player();
            player[lifesAttributeName] = 5;
            oneLifeLess = (int)player[lifesAttributeName] - 1;

            enemy = new Player();
            enemy[lifesAttributeName] = 1;
            oneLifeMore = (int)enemy[lifesAttributeName] + 1;
            enemy.CardInHand = new Card();
            enemy.CardInHand.Played += (o, e) => 
            {
                if (e.TargetPlayer != null)
                    e.TargetPlayer[lifesAttributeName] = (int)e.TargetPlayer[lifesAttributeName] - 1;
            };
        };

        Because of = () =>
        {
            enemy.PlayCard(player);
        };

        It should_affect_another_player = () =>
        {
            player[lifesAttributeName].ShouldEqual(oneLifeLess);
        };
        
        private static string lifesAttributeName;
        private static int oneLifeLess, oneLifeMore;
        private static Player player;
        private static Player enemy;
    }

    [Subject(typeof(Card))]
    public class when_has_action_affecting_player_who_plays_the_card_and_is_played
    {
        Establish context = () =>
        {
            lifesAttributeName = "lifes";

            player = new Player();
            player[lifesAttributeName] = 1;
            oneLifeMore = (int)player[lifesAttributeName] + 1;
            player.CardInHand = new Card();
            player.CardInHand.Played += (o, e) =>
            {
                (o as Player)[lifesAttributeName] = (int)(o as Player)[lifesAttributeName] + 1;
            };
        };

        Because of = () =>
        {
            player.PlayCard();
        };

        It should_affect_player_who_played_the_card = () =>
        {
            player[lifesAttributeName].ShouldEqual(oneLifeMore);
        };

        private static string lifesAttributeName;
        private static int oneLifeMore;
        private static Player player;
    }
}
