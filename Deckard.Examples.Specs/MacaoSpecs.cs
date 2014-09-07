using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;

namespace Deckard.Examples.Specs
{
    [Subject("Next player")]
    class when_current_player_plays_2_of_any_suit : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 2;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "2" && c["suit"] == "Clubs");
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };
        It should_have_drawn_2_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
        
        public static int sourceSizeBeforeAction;
        public static int handSizeBeforeAction;
        public static int cardsToTake;
    }

    [Subject("Next player")]
    class when_current_player_plays_3_of_any_suit : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 3;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "3" && c["suit"] == "Diamonds");
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };
        It should_have_drawn_3_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };

        public static int sourceSizeBeforeAction;
        public static int handSizeBeforeAction;
        public static int cardsToTake;
    }
    
    [Subject("Next player")]
    class when_current_player_plays_King_of_Hearts : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 5;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "King" && c["suit"] == "Hearts");
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };
        It should_have_drawn_5_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };

        public static int sourceSizeBeforeAction;
        public static int handSizeBeforeAction;
        public static int cardsToTake;
    }
    
    /*
    [Subject("Current player")]
    public class when_plays_2_of_any_suit_and_second_player_counters_with_King_of_Hearts : when_game_is_established_and_started
    {
        Establish context = () =>
        {
            IShuffler shuffler = An<IShuffler>();
            source = new Deck(shuffler);

            twoOfSpades = new Card();
            twoOfSpades["suit"] = "Spades";
            twoOfSpades["name"] = "2";

            cardsToTakeFor2 = 2;


            twoOfSpades.Played += (o, e) =>
            {
                if (e.TargetPlayer == null)
                    throw new System.ArgumentException("Target player cannot be null.");


                Deckard.Player.PlayerActionEventHandler action = null;
                action = (ao, ae) =>
                {
                    for (int i = 0; i < cardsToTakeFor2; i++)
                        e.TargetPlayer.Draw(source); 
                    
                    e.TargetPlayer.Action -= action;
                };

                e.TargetPlayer.Action += action;
            };

            kingOfHearts = new Card();
            kingOfHearts["suit"] = "Hearts";
            kingOfHearts["name"] = "King";

            cardsToTakeForKing = 5;

            kingOfHearts.Played += (o, e) =>
            {
                if (e.TargetPlayer == null)
                    throw new System.ArgumentException("Target player cannot be null.");
                
                Deckard.Player.PlayerActionEventHandler action = null;
                action = (ao, ae) =>
                {
                    for (int i = 0; i < cardsToTakeForKing; i++)
                        e.TargetPlayer.Draw(source);

                    e.TargetPlayer.Action -= action;
                };

                e.TargetPlayer.Action += action;
            };

            
            for (int i = 0; i < cardsToTakeForKing + cardsToTakeFor2; i++)
                source.Cards.Add(An<Card>());
            source.Cards.Add(kingOfHearts);
            source.Cards.Add(twoOfSpades);

            player1 = new Player() { Hand = new Deck(shuffler) };
            player2 = new Player() { Hand = new Deck(shuffler) };
            oldsHandSize = player2.Hand.Size;
        };
        Because of = () =>
        {
            player1.DrawFrom(source);
            player2.DrawFrom(source);
            sizeBeforeSecondDraw = source.Size;
            player1.PlayCard(player2);
            player2.PlayCard(player1);
            player1.TakeAction();
        };
        It should_have_drawn_2_cards = () =>
        {
            source.Size.ShouldEqual(sizeBeforeSecondDraw - (cardsToTakeForKing + cardsToTakeFor2));
            player1.Hand.Size.ShouldEqual(oldsHandSize + cardsToTakeForKing + cardsToTakeFor2);
        };

        public static int cardsToTakeFor2;
        public static int cardsToTakeForKing;
        public static Player player1;
        public static Player player2;
        public static Deck source;
        public static int oldsHandSize;
        public static int sizeBeforeSecondDraw;
        public static Card twoOfSpades;
        public static Card kingOfHearts;
    }
    */

    public class when_game_is_established_and_started : WithFakes
    {
        Establish context = () =>
        {
            game = new Game();
            IShuffler shuffler = new RandomNumberSortShuffler();
            game.SourceDecks.Add(SetupDeck(shuffler));

            game.Heros.Add(new Player() { Hand = new Deck(shuffler) });
            game.Heros.Add(new Player() { Hand = new Deck(shuffler) });

            game.DealCards(game.SourceDecks[0], new List<Deck> { game.Heros[0].Hand, game.Heros[1].Hand }, 5);
        };

        public static Deck SetupDeck(IShuffler shuffler)
        {
            Deck deck = new Deck(shuffler);

            List<string> suits = new List<string> { "Hearts", "Diamonds", "Clubs", "Spades" };
            List<string> values = new List<string> { "Ace", "King", "Queen", "Jack" };
            for (int i = 10; i > 1; i--)
            {
                values.Add(i.ToString());
            }

            foreach (var suit in suits)
            {
                foreach (var value in values)
                { 
                    Card card = new Card();
                    card["value"] = value;
                    card["suit"] = suit;
                    deck.Cards.Add(card);
                }
            }
            deck = SetupFunctionalCards(deck);

            deck.Shuffle();
            deck.MoveToTop(c => c["value"] == "2" && c["suit"] == "Clubs");
            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == "Spades");
            deck.MoveToTop(c => c["value"] == "3" && c["suit"] == "Diamonds");
            deck.MoveToTop(c => c["value"] == "Queen" && c["suit"] == "Hearts");
            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == "Hearts");

            return deck;
        }

        public static Deck SetupFunctionalCards(Deck deck) 
        {
            List<Card> cards = null;

            // setup 2s and 3s
            cards = deck.Cards.FindAll(c => c["value"] == "2" || c["value"] == "3");
            foreach (var card in cards)
            {
                int cardsToTake = int.Parse(card["value"]);
                card.Played += (o, e) =>
                {
                    if (e.TargetPlayer == null)
                        throw new System.ArgumentException("Target player cannot be null.");

                    Game.PlayerActionEventHandler action = null;
                    action += (ao, ae) =>
                    {
                        game.DealCards(game.SourceDecks[0], new List<Deck> { e.TargetPlayer.Hand }, cardsToTake);

                        game.Action -= action;
                    };

                    game.Action += action;
                };
            }

            // setup Kings
            cards = deck.Cards.FindAll(c => c["value"] == "King" && c["suit"] == "Hearts");
            foreach (var card in cards)
            {
                int cardsToTake = 5;
                card.Played += (o, e) =>
                {
                    if (e.TargetPlayer == null)
                        throw new System.ArgumentException("Target player cannot be null.");

                    Game.PlayerActionEventHandler action = null;
                    action += (ao, ae) =>
                    {
                        game.DealCards(game.SourceDecks[0], new List<Deck> { e.TargetPlayer.Hand }, cardsToTake);

                        game.Action -= action;
                    };

                    game.Action += action;
                };
            }

            return deck;
        }

        public static Game game;
    }
}
