using Machine.Fakes;
using Machine.Specifications;

namespace Deckard.Examples.Specs
{
    [Subject("Next player")]
    class when_current_player_plays_2_of_any_suit : WithFakes
    {
        Establish context = () => 
        {
            IShuffler shuffler = An<IShuffler>();
            source = new Deck(shuffler);
            
            twoOfSpades = new Card();
            twoOfSpades["suit"] = "Spades";
            twoOfSpades["name"] = "2";

            cardsToTake = 2;

            twoOfSpades.Played += (o, e) =>
            {
                if (e.TargetPlayer == null)
                    throw new System.ArgumentException("Target player cannot be null.");


                for (int i = 0; i < cardsToTake; i++)
                    e.TargetPlayer.Draw(source);
            };


            for (int i = 0; i < cardsToTake; i++)
                source.Cards.Add(An<Card>());
            source.Cards.Add(twoOfSpades);

            player1 = new Player();
            player2 = new Player() { Hand = new Deck(shuffler) };
            oldsHandSize = player2.Hand.Size;
        };
        Because of = () =>
        {
            player1.DrawFrom(source);
            sizeBeforeSecondDraw = source.Size;
            player1.PlayCard(player2);
        };
        It should_have_drawn_2_cards = () =>
        {
            source.Size.ShouldEqual(sizeBeforeSecondDraw - cardsToTake);
            player2.Hand.Size.ShouldEqual(oldsHandSize + cardsToTake);
        };

        public static int cardsToTake;
        public static  Player player1;
        public static Player player2;
        public static Deck source;
        public static int oldsHandSize;
        public static int sizeBeforeSecondDraw;
        public static Card twoOfSpades;
    }

    [Subject("Next player")]
    class when_current_player_plays_3_of_any_suit : WithFakes
    {
        Establish context = () =>
        {
            IShuffler shuffler = An<IShuffler>();
            source = new Deck(shuffler);

            threeOfSpades = new Card();
            threeOfSpades["suit"] = "Clubs";
            threeOfSpades["name"] = "3";

            cardsToTake = 3;

            threeOfSpades.Played += (o, e) =>
            {
                if (e.TargetPlayer == null)
                    throw new System.ArgumentException("Target player cannot be null.");

                for (int i = 0; i < cardsToTake; i++ )
                    e.TargetPlayer.Draw(source);
            };


            for (int i = 0; i < cardsToTake; i++)
                source.Cards.Add(An<Card>());
            source.Cards.Add(threeOfSpades);

            player1 = new Player();
            player2 = new Player() { Hand = new Deck(shuffler) };
            oldsHandSize = player2.Hand.Size;
        };
        Because of = () =>
        {
            player1.DrawFrom(source);
            sizeBeforeSecondDraw = source.Size;
            player1.PlayCard(player2);
        };
        It should_have_drawn_3_cards = () =>
        {
            source.Size.ShouldEqual(sizeBeforeSecondDraw - cardsToTake);
            player2.Hand.Size.ShouldEqual(oldsHandSize + cardsToTake);
        };

        public static int cardsToTake;
        public static Player player1;
        public static Player player2;
        public static Deck source;
        public static int oldsHandSize;
        public static int sizeBeforeSecondDraw;
        public static Card threeOfSpades;
    }

    [Subject("Next player")]
    class when_current_player_plays_King_of_Hearts : WithFakes
    {
        Establish context = () =>
        {
            IShuffler shuffler = An<IShuffler>();
            source = new Deck(shuffler);

            kingOfHearts = new Card();
            kingOfHearts["suit"] = "Hearts";
            kingOfHearts["name"] = "King";

            cardsToTake = 5;

            kingOfHearts.Played += (o, e) =>
            {
                if (e.TargetPlayer == null)
                    throw new System.ArgumentException("Target player cannot be null.");

                for (int i = 0; i < cardsToTake; i++)
                    e.TargetPlayer.Draw(source);
            };


            for (int i = 0; i < cardsToTake; i++)
                source.Cards.Add(An<Card>());
            source.Cards.Add(kingOfHearts);

            player1 = new Player();
            player2 = new Player() { Hand = new Deck(shuffler) };
            oldsHandSize = player2.Hand.Size;
        };
        Because of = () =>
        {
            player1.DrawFrom(source);
            sizeBeforeSecondDraw = source.Size;
            player1.PlayCard(player2);
        };
        It should_have_drawn_5_cards = () =>
        {
            source.Size.ShouldEqual(sizeBeforeSecondDraw - cardsToTake);
            player2.Hand.Size.ShouldEqual(oldsHandSize + cardsToTake);
        };

        public static int cardsToTake;
        public static Player player1;
        public static Player player2;
        public static Deck source;
        public static int oldsHandSize;
        public static int sizeBeforeSecondDraw;
        public static Card kingOfHearts;
    }
}
