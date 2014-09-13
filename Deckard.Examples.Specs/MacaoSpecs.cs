using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Collections.Generic;

namespace Deckard.Examples.Specs
{
    [Subject("Next player")]
    class when_current_player_plays_2_of_any_suit_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 2;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "2" && c["suit"] == Clubs);
            EndTurnWithoutPlayingCard();
        };

        It should_have_taken_2_card_from_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
        };
        It should_have_2_more_card_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    [Subject("Next player")]
    class when_current_player_plays_3_of_any_suit_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 3;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "3" && c["suit"] == Clubs);
            EndTurnWithoutPlayingCard();
        };

        It should_have_taken_3_card_from_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
        };
        It should_have_3_more_card_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }
    
    [Subject("Next player")]
    class when_current_player_plays_King_of_Hearts_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 5;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "King" && c["suit"] == Hearts);
            EndTurnWithoutPlayingCard();
        };

        It should_have_taken_5_card_from_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
        };
        It should_have_5_more_card_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    [Subject("Third player")]
    class when_current_player_plays_3_of_any_suit_and_second_player_plays_2_of_any_suit_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 5;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "3" && c["suit"] == Clubs);  // 1
            ChoosePlayAndEnd(c => c["value"] == "2" && c["suit"] == Spades);    // 2
            EndTurnWithoutPlayingCard();                                        // 3
        };

        It should_have_taken_5_card_from_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
        };
        It should_have_5_more_card_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    [Subject("Next player")]
    class when_nonfunction_card_is_played_and_wants_to_play_a_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 1;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "8" && c["suit"] == Clubs);
        };

        It should_be_able_to_play_card_of_the_same_suit = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["suit"] == Hearts));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_card_of_the_same_value = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == "8"));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_Queen = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == "Queen"));
            couldBePlayed.ShouldEqual(true);
        };

        It should_not_be_able_to_play_any_other_card = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] != "Queen" && c["value"] != "8" && c["suit"] != Hearts));
            couldBePlayed.ShouldEqual(false);
        };
    }

    [Subject("Next player")]
    class when_nonfunction_card_is_played_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 1;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "8" && c["suit"] == Clubs);
            EndTurnWithoutPlayingCard();
        };

        It should_have_taken_1_card_from_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
        };
        It should_have_1_more_card_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    [Subject("Third player")]
    class when_first_player_plays_3_of_any_suit_and_second_player_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 3;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "3" && c["suit"] == Clubs);
            EndTurnWithoutPlayingCard();
        };

        It should_be_able_to_play_card_of_the_same_suit = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["suit"] == Clubs));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_card_of_the_same_value = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == "3"));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_Queen = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == "Queen"));
            couldBePlayed.ShouldEqual(true);
        };

        It should_not_be_able_to_play_any_other_card = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] != "Queen" && c["value"] != "3" && c["suit"] != Clubs));
            couldBePlayed.ShouldEqual(false);
        };
    }

    [Subject("Next player")]
    class when_current_player_plays_Queen_of_any_suit : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 3;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "Queen" && c["suit"] == Hearts);
        };

        It should_be_able_to_play_card_of_the_same_suit = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["suit"] == Hearts));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_card_of_the_same_value = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == "Queen"));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_card_of_another_suit = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["suit"] != Hearts));
            couldBePlayed.ShouldEqual(true);
        };

        It should_be_able_to_play_card_of_another_value = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] != "Queen"));
            couldBePlayed.ShouldEqual(true);
        };
    }

    [Subject("Next player")]
    class when_current_player_plays_4_of_any_suit_and_wants_to_play_a_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardValue = "4";
            cardSuit = Clubs;

            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == cardValue && c["suit"] == cardSuit);
        };

        It should_be_able_to_play_card_of_the_same_value = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == cardValue));
            couldBePlayed.ShouldEqual(true);
        };

        It should_not_be_able_to_play_card_of_the_same_suit_other_than_4 = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["suit"] == cardSuit));
            couldBePlayed.ShouldEqual(false);
        };

        It should_not_be_able_to_play_Queen = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["value"] == "Queen"));
            couldBePlayed.ShouldEqual(false);
        };

        static string cardValue;
        static string cardSuit;
    }


    [Subject("Next player")]
    class when_current_player_plays_4_of_any_suit_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "4" && c["suit"] == Clubs);
            EndTurnWithoutPlayingCard();
        };

        It should_be_forced_to_wait_1_turn = () =>
        {
            game.PreviousPlayer[turnsToWait].ShouldEqual(1);
        };

        It should_have_the_same_amount_of_cards_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction);
        };
    }


    [Subject("Third player")]
    class when_first_player_plays_4_and_second_player_plays_4_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "4");
            ChoosePlayAndEnd(c => c["value"] == "4");
            EndTurnWithoutPlayingCard();
        };

        It should_be_forced_to_wait_2_turns = () =>
        {
            game.PreviousPlayer[turnsToWait].ShouldEqual(2);
        };
    }



    [Subject("Next player")]
    class when_current_player_plays_Ace_of_any_suit_and_he_wants_to_play_a_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            newSuit = "Spades";

            AceCardActionEventArgs actionEventArgs = new AceCardActionEventArgs(newSuit);

            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.NextPlayer.Hand.Size;

            ChoosePlayAndEnd(c => c["value"] == "Ace" && c["suit"] == Clubs, actionEventArgs);
        };

        It should_force_the_current_suit_to_chosen_one = () =>
        {
            bool couldBePlayed = game.CheckCard(game.CurrentPlayer.Hand.Cards.Find(c => c["suit"] == newSuit));
            couldBePlayed.ShouldEqual(true);
        };

        static string newSuit;
    }


    [Subject("Previous player")]
    class when_current_player_plays_King_of_Spaes_and_he_did_not_play_any_card : when_game_is_established_and_started
    {
        Because of = () =>
        {
            cardsToTake = 5;
            game.Start();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;

            EndTurnWithoutPlayingCard();
            sourceSizeBeforeAction = game.SourceDecks[0].Size;
            handSizeBeforeAction = game.PreviousPlayer.Hand.Size;
            ChoosePlayAndEnd(c => c["value"] == "King" && c["suit"] == Spades);
            EndTurnWithoutPlayingCard();
        };

        It should_have_taken_5_card_from_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(sourceSizeBeforeAction - cardsToTake);
        };
        It should_have_5_more_card_in_hand = () =>
        {
            game.PreviousPlayer.Hand.Size.ShouldEqual(handSizeBeforeAction + cardsToTake);
        };
    }

    public class when_game_is_established_and_started : WithFakes
    {
        Establish context = () =>
        {
            #if DEBUG
                System.Threading.Thread.Sleep(5000);
            #endif
            
            game = new Game();
            game.RoundEndAction += (o, e) => 
            {
                if (game.CurrentPlayer.CardsPlayed == 0)
                {
                    if (!game.IsCustomActionSet)
                    {
                        game.OnDefaultActionTaken(game, new PlayerActionEventArgs(game.CurrentPlayer));
                    }
                    else
                    {
                        game.OnCustomActionTaken(game, new PlayerActionEventArgs(game.CurrentPlayer));
                    }
                }
            };
            game.DefaultAction += (o, e) =>
            {
                if (e.TargetPlayer[turnsToWait] == 0)
                    e.TargetPlayer.Draw(game.SourceDecks[0]);
                else
                    e.TargetPlayer[turnsToWait]--;
            };
            game.NextCardCriteria = (c => c["value"] == game.DestinationDeck.Top["value"] 
                || c["suit"] == game.DestinationDeck.Top["suit"]
                || c["value"] == "Queen");


            IShuffler shuffler = new RandomNumberSortShuffler();
            game.SourceDecks.Add(SetupDeck(shuffler));
            game.DestinationDeck = new Deck(shuffler);

            game.Heros.Add(new Player() 
            {
                Attributes = new Dictionary<string, int>()
                {
                    { "number", 1 } ,
                    { turnsToWait, 0 } 
                },
                Hand = new Deck(shuffler) 
            });
            game.Heros.Add(new Player() 
            {
                Attributes = new Dictionary<string, int>()
                {
                    { "number", 2 } ,
                    { turnsToWait, 0 } 
                },
                Hand = new Deck(shuffler) 
            });
            game.Heros.Add(new Player() 
            {
                Attributes = new Dictionary<string, int> ()
                {
                    { "number", 3 } ,
                    { turnsToWait, 0 } 
                },
                Hand = new Deck(shuffler) 
            });

            game.DealFirstCards(7);
            game.DealCards(game.SourceDecks[0], new List<Deck> { game.DestinationDeck }, 1);
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

            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == Clubs); // first card


            deck.MoveToTop(c => c["value"] == "6" && c["suit"] == Hearts);       // 3
            deck.MoveToTop(c => c["value"] == "7" && c["suit"] == Spades);   // 2
            deck.MoveToTop(c => c["value"] == "Ace" && c["suit"] == Clubs);       // 1

            deck.MoveToTop(c => c["value"] == "5" && c["suit"] == Hearts);       // 3
            deck.MoveToTop(c => c["value"] == "4" && c["suit"] == Spades);   // 2
            deck.MoveToTop(c => c["value"] == "4" && c["suit"] == Clubs);       // 1

            deck.MoveToTop(c => c["value"] == "5" && c["suit"] == Clubs);       // 3
            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == Spades);   // 2
            deck.MoveToTop(c => c["value"] == "2" && c["suit"] == Clubs);       // 1

            deck.MoveToTop(c => c["value"] == "10" && c["suit"] == Diamonds);   // 3
            deck.MoveToTop(c => c["value"] == "Queen" && c["suit"] == Clubs);   // 2
            deck.MoveToTop(c => c["value"] == "3" && c["suit"] == Clubs);    // 1
            
            deck.MoveToTop(c => c["value"] == "3" && c["suit"] == Spades);      // 3
            deck.MoveToTop(c => c["value"] == "2" && c["suit"] == Spades);      // 2
            deck.MoveToTop(c => c["value"] == "King" && c["suit"] == Hearts);   // 1

            deck.MoveToTop(c => c["value"] == "8" && c["suit"] == Diamonds);    // 3
            deck.MoveToTop(c => c["value"] == "10" && c["suit"] == Hearts);     // 2
            deck.MoveToTop(c => c["value"] == "8" && c["suit"] == Clubs);      // 1

            deck.MoveToTop(c => c["value"] == "Queen" && c["suit"] == Diamonds);    // 3
            deck.MoveToTop(c => c["value"] == "8" && c["suit"] == Hearts);       // 2
            deck.MoveToTop(c => c["value"] == "Queen" && c["suit"] == Hearts);      // 1

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

                        game.CustomNextCardCriteria = null;
                        game.CustomAction -= action;
                    };
                    game.CustomAction += action;
                    game.IsCustomActionSet = true;

                    game.CustomNextCardCriteria = (c => c["value"] == "2" || c["value"] == "3"
                        || (c["value"] == "King" && (c["suit"] == Hearts || c["suit"] == Spades)));
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

                        game.CustomNextCardCriteria = null;
                        game.CustomAction -= action;
                    };
                    game.CustomAction += action;
                    game.IsCustomActionSet = true;

                    game.CustomNextCardCriteria = (c => c["value"] == "2" || c["value"] == "3"
                        || (c["value"] == "King" && (c["suit"] == Hearts || c["suit"] == Spades)));
                };
            }
            cards = deck.Cards.FindAll(c => c["value"] == "King" && c["suit"] == Spades);
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

                        game.CustomNextCardCriteria = null;
                        game.CustomAction -= action;
                    };
                    game.CustomAction += action;
                    game.IsCustomActionSet = true;
                    game.ForceNextPlayerTo(game.PreviousPlayer);
                    
                    game.CustomNextCardCriteria = (c => c["value"] == "2" || c["value"] == "3"
                        || (c["value"] == "King" && (c["suit"] == Hearts || c["suit"] == Spades)));
                };
            }

            // setup Queens
            cards = deck.Cards.FindAll(c => c["value"] == "Queen");
            foreach (var card in cards)
            {
                card.Played += (o, e) =>
                {
                    Game.PlayerActionEventHandler action = null;
                    action += (ao, ae) =>
                    {
                        game.CustomNextCardCriteria = null;
                        game.CustomAction -= action;
                    };
                    game.CustomAction += action;
                    game.IsCustomActionSet = true;

                    game.CustomNextCardCriteria = (c => c["value"] == game.DestinationDeck.Top["value"]
                                                    || c["suit"] == game.DestinationDeck.Top["suit"]
                                                    || c["value"] != game.DestinationDeck.Top["value"]
                                                    || c["suit"] != game.DestinationDeck.Top["suit"]);
                };
            }

            // setup 4s
            cards = deck.Cards.FindAll(c => c["value"] == "4");
            foreach (var card in cards)
            {
                card.Played += (o, e) =>
                {
                    Game.PlayerActionEventHandler action = null;
                    action += (ao, ae) =>
                    {
                        ae.TargetPlayer[turnsToWait]++;

                        game.CustomNextCardCriteria = null;
                        game.CustomAction -= action;
                    };
                    game.CustomAction += action;
                    game.IsCustomActionSet = true;

                    game.CustomNextCardCriteria = (c => c["value"] == "4");
                };
            }


            // setup Aces
            cards = deck.Cards.FindAll(c => c["value"] == "Ace");
            foreach (var card in cards)
            {
                card.Played += (o, e) =>
                {
                    Game.PlayerActionEventHandler action = null;
                    action += (ao, ae) =>
                    {
                        game.CustomNextCardCriteria = null;
                        game.CustomAction -= action;
                    };
                    game.CustomAction += action;
                    game.IsCustomActionSet = true;

                    game.CustomNextCardCriteria = (c => c["value"] == game.DestinationDeck.Top["value"]
                || c["suit"] == ((AceCardActionEventArgs)e).ChosenSuit);
                };
            }

            return deck;
        }

        public static void ChoosePlayAndEnd(Predicate<Card> cardPredicate, CardActionEventArgs actionEventArgs = null)
        {
            Card chosenCard = game.CurrentPlayer.ChooseCardToPlay(cardPredicate);

            if (chosenCard == null)
                throw new ArgumentException("The player does not such card and cannot play it.");

            if (!game.CheckCard(chosenCard))
                throw new ArgumentException("The card cannot be played - it does not meet the requirements.");

            game.CurrentPlayer.PlayCard(game.NextPlayer, game.DestinationDeck, actionEventArgs);
            game.EndRound();
        }

        public static void EndTurnWithoutPlayingCard()
        {
            game.EndRound();
        }

        public static Game game;
        public static int sourceSizeBeforeAction;
        public static int handSizeBeforeAction;
        public static int cardsToTake;

        protected const string Hearts = "Hearts";
        protected const string Diamonds = "Diamonds";
        protected const string Clubs = "Clubs";
        protected const string Spades = "Spades";

        protected const string turnsToWait = "turnsToWait";
    }

    public class AceCardActionEventArgs : CardActionEventArgs
    {
        public string ChosenSuit;

        public AceCardActionEventArgs(string chosenSuit) : base(null)
        {
            ChosenSuit = chosenSuit;
        }
    }
}
