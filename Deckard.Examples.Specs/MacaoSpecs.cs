using Machine.Fakes;
using Machine.Specifications;

namespace Deckard.Examples.Specs
{
    [Subject("Next player")]
    class when_current_player_plays_2 : WithFakes
    {
        Establish context = () => 
        {
            //game = new Game();
            IShuffler shuffler = An<IShuffler>();
            source = new Deck(shuffler);

            twoOfSpades = new Card();
            twoOfSpades["suit"] = "Spades";
            twoOfSpades["name"] = "2";

            twoOfSpades.Played += (o, e) =>
            {
                if (e.TargetPlayer == null)
                    throw new System.ArgumentException("Target player cannot be null.");

                e.TargetPlayer.DrawFrom(source);
                e.TargetPlayer.PutCardIn(e.TargetPlayer.Hand);
                e.TargetPlayer.DrawFrom(source);
                e.TargetPlayer.PutCardIn(e.TargetPlayer.Hand);
            };
            
            source.Cards.Add(An<Card>());
            source.Cards.Add(An<Card>());
            source.Cards.Add(twoOfSpades);

            player1 = new Player();
            player2 = new Player() { Hand = new Deck(shuffler) };

            player1.DrawFrom(source);
            oldsSourceSize = source.Size;
            oldsHandSize = player2.Hand.Size;
        };
        Because of = () => 
        {
            player1.PlayCard(player2);
        };
        It should_have_drawn_2_cards = () =>
        {
            source.Size.ShouldEqual(oldsSourceSize - 2);
            player2.Hand.Size.ShouldEqual(oldsHandSize + 2);
        };

        static  Player player1;
        static  Player player2;
        static  Deck source;
        static int oldsHandSize;
        static int oldsSourceSize;
        static  Card twoOfSpades;
    }
}
