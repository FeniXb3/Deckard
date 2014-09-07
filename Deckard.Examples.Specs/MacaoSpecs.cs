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

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "2" && c["suit"] == Clubs);
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };
        It should_have_drawn_2_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
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

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "3" && c["suit"] == Diamonds);
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };
        It should_have_drawn_3_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
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

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "King" && c["suit"] == Hearts);
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };
        It should_have_drawn_5_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    [Subject("Third player")]
    class when_current_player_plays_3_of_any_suit_and_second_player_plays_2_of_any_suit : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 5;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "3" && c["suit"] == Diamonds);
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "2" && c["suit"] == Spades);
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();
            
            game.EndRound();
        };
        It should_have_drawn_5_cards_at_the_end_of_round = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    [Subject("Next player")]
    class when_non_function_card_is_played : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 1;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            game.CurrentPlayer.ChooseCardToPlay(c => c["value"] == "8" && c["suit"] == Hearts);
            game.CurrentPlayer.PlayCard(game.NextPlayer);
            game.EndRound();

            game.EndRound();
        };

        It should_have_taken_1_card = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }


    public class when_game_is_established_and_started : WithFakes
    {
        Establish context = () =>
        {
            #if DEBUG
                System.Threading.Thread.Sleep(10000);
            #endif

            game = new Game();
            game.DefaultAction += (o, e) =>
            {
                e.TargetPlayer.Draw(game.SourceDecks[0]);
            };


            IShuffler shuffler = new RandomNumberSortShuffler();
            game.SourceDecks.Add(SetupDeck(shuffler));

            game.Heros.Add(new Player() { Attributes = new Dictionary<string, int> { { "number", 1 } }, Hand = new Deck(shuffler) });
            game.Heros.Add(new Player() { Attributes = new Dictionary<string, int> { { "number", 2 } }, Hand = new Deck(shuffler) });
            game.Heros.Add(new Player() { Attributes = new Dictionary<string, int> { { "number", 3 } }, Hand = new Deck(shuffler) });

            game.DealFirstCards(5);
        };

        public static Deck SetupDeck(IShuffler shuffler)
        {
            Deck deck = new Deck(shuffler);

            List<string> suits = new List<string> { Hearts, Diamonds, Clubs, Spades };
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
            deck.MoveToTop(c => c["value"] == "5" && c["suit"] == Clubs);       // 3
            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == Spades);   // 2
            deck.MoveToTop(c => c["value"] == "2" && c["suit"] == Clubs);       // 1
            deck.MoveToTop(c => c["value"] == "10" && c["suit"] == Diamonds);   // 3
            deck.MoveToTop(c => c["value"] == "Queen" && c["suit"] == Hearts);  // 2
            deck.MoveToTop(c => c["value"] == "3" && c["suit"] == Diamonds);    // 1
            deck.MoveToTop(c => c["value"] == "7" && c["suit"] == Spades);      // 3
            deck.MoveToTop(c => c["value"] == "2" && c["suit"] == Spades);      // 2
            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == Hearts);   // 1
            deck.MoveToTop(c => c["value"] == "8" && c["suit"] == Diamonds);    // 3
            deck.MoveToTop(c => c["value"] == "10" && c["suit"] == Hearts);     // 2
            deck.MoveToTop(c => c["value"] == "8" && c["suit"] == Hearts);      // 1

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
                        while (cardsToTake-- > 0)
                            ae.TargetPlayer.Draw(game.SourceDecks[0]);

                        game.CustomAction -= action;
                    };

                    game.CustomAction += action;
                };
            }

            // setup Kings
            cards = deck.Cards.FindAll(c => c["value"] == "King" && c["suit"] == Hearts);
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
                        while (cardsToTake-- > 0)
                            ae.TargetPlayer.Draw(game.SourceDecks[0]);

                        game.CustomAction -= action;
                    };

                    game.CustomAction += action;
                };
            }

            return deck;
        }

        public static Game game;
        public static int sourceSizeBeforeAction;
        public static int handSizeBeforeAction;
        public static int cardsToTake;

        protected const string Hearts = "Hearts";
        protected const string Diamonds = "Diamonds";
        protected const string Clubs = "Clubs";
        protected const string Spades = "Spades";
    }
}
