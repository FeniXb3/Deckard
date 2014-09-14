using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;

namespace Deckard.Specs
{
    [Subject("Game")]
    class when_dealing_cards : WithFakes
    {
        Establish context = () =>
        {
            game = new Game();
            IShuffler shuffler = An<IShuffler>();
            source1 = new Deck(shuffler);

            dest1 = new Deck(shuffler);
            dest2 = new Deck(shuffler);

            cardsToDest1 = new List<Card>();
            cardsToDest2 = new List<Card>();

            cardsToDest1.Add(new Card());
            cardsToDest1[0]["suit"] = "Dimonds";
            cardsToDest1[0]["name"] = "10";

            cardsToDest1.Add(new Card());
            cardsToDest1[1]["suit"] = "Hearts";
            cardsToDest1[1]["name"] = "Jack";
            
            cardsToDest2.Add(new Card());
            cardsToDest2[0]["suit"] = "Spades";
            cardsToDest2[0]["name"] = "Ace";

            cardsToDest2.Add(new Card());
            cardsToDest2[1]["suit"] = "Hearts";
            cardsToDest2[1]["name"] = "Queen";

            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Spades";
            source1.Top["name"] = "King";

            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Spades";
            source1.Top["name"] = "7";

            source1.Cards.Add(cardsToDest2[0].DeepCopy());
            source1.Cards.Add(cardsToDest1[0].DeepCopy());
            source1.Cards.Add(cardsToDest2[1].DeepCopy());
            source1.Cards.Add(cardsToDest1[1].DeepCopy());

            source1Size = source1.Size;
            dest1Size = dest1.Size;
            dest2Size = dest2.Size;
            cardsToDrawFromSource1Count = 2;

            game.SourceDeck = source1;

            hero1 = new Player();
            hero1.Hand = dest1;
            hero2 = new Player();
            hero2.Hand = dest2;

            game.Players.Add(hero1);
            game.Players.Add(hero2);
        };

        Because of = () =>
        {
            game.DealCards(game.SourceDeck, new List<Deck> { game.Players[0].Hand, game.Players[1].Hand }, cardsToDrawFromSource1Count);
        };

        It should_have_less_cards_in_source_decks = () =>
        {
            game.SourceDeck.Size.ShouldEqual(source1Size - cardsToDrawFromSource1Count * 2);
        };

        It should_have_more_cards_in_destination_decs = () =>
        {
            game.Players[0].Hand.Size.ShouldEqual(dest1Size + cardsToDrawFromSource1Count);
            game.Players[1].Hand.Size.ShouldEqual(dest2Size + cardsToDrawFromSource1Count);
        };
        
        It should_have_source_decks_without_dealt_cards = () =>
        {
            game.SourceDeck.Cards.ShouldNotContain(cardsToDest1);
            game.SourceDeck.Cards.ShouldNotContain(cardsToDest2);
        };

        It should_have_destination_decks_with_dealt_cards = () =>
        {
            game.Players[0].Hand.Cards.ShouldContain(cardsToDest1);
            game.Players[1].Hand.Cards.ShouldContain(cardsToDest2);
        };

        static Deck source1;
        static Deck dest1, dest2;
        static int dest1Size, dest2Size;
        static int source1Size;
        static int cardsToDrawFromSource1Count;
        static Game game;
        static List<Card> cardsToDest1, cardsToDest2;
        static Player hero1, hero2;
    }


    [Subject("Game")]
    class when_dealing_cards_from_to_small_deck : WithFakes
    {
        Establish context = () =>
        {
            game = new Game();
            IShuffler shuffler = An<IShuffler>();
            source1 = new Deck(shuffler);

            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Spades";
            source1.Top["name"] = "7";

            dest1 = new Deck(shuffler);
            dest2 = new Deck(shuffler);

            game.SourceDeck = source1;

            hero1 = new Player();
            hero1.Hand = dest1;
            hero2 = new Player();
            hero2.Hand = dest2;

            game.Players.Add(hero1);
            game.Players.Add(hero2);
        };

        Because of = () =>
        {
            game.DealCards(source1, new List<Deck> { game.Players[0].Hand, game.Players[1].Hand }, 1);
        };

        It should_have_destination_deck_with_fewer_cards = () =>
        {
            game.Players[1].Hand.Size.ShouldBeLessThan(game.Players[0].Hand.Size);
        };

        static Deck source1;
        static Deck dest1, dest2;
        static Game game;
        static Player hero1, hero2;
    }

    [Subject("Game")]
    class when_dealing_all_cards_from_a_deck : WithFakes
    {
        Establish context = () =>
        {
            game = new Game();
            IShuffler shuffler = An<IShuffler>();
            source1 = new Deck(shuffler);

            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Spades";
            source1.Top["name"] = "7";
            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Dimonds";
            source1.Top["name"] = "Queen";
            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Spades";
            source1.Top["name"] = "1";
            source1.Cards.Add(new Card());
            source1.Top["suit"] = "Spades";
            source1.Top["name"] = "Ace";

            dest1 = new Deck(shuffler);
            dest2 = new Deck(shuffler);

            sourceSize = source1.Size;

            game.SourceDeck = source1;

            hero1 = new Player();
            hero1.Hand = dest1;
            hero2 = new Player();
            hero2.Hand = dest2;

            game.Players.Add(hero1);
            game.Players.Add(hero2);
        };

        Because of = () =>
        {
            game.DealCards(game.SourceDeck, new List<Deck> { game.Players[0].Hand, game.Players[1].Hand });
        };

        It should_have_no_cards_in_source_deck = () =>
        {
            game.SourceDeck.Size.ShouldEqual(0);
        };

        It should_have_all_cards_from_source_in_destination_decks = () =>
        {
            sourceSize.ShouldEqual(game.Players[0].Hand.Size + game.Players[1].Hand.Size);
        };

        static Deck source1;
        static Deck dest1, dest2;
        static Game game;
        static int sourceSize;
        static Player hero1, hero2;
    }

    [Subject(typeof(Game))]
    class when_has_first_round_finished
    {
        Establish context = () => 
        {
            game = new Game();
            game.Players.Add(new Player());
            game.Start();
        };

        Because of = () => 
        {
            game.EndRound();            
        };

        It should_have_second_round_as_actual = () => 
        {
            game.CurrentRoundNumber.ShouldEqual(1);
        };

        It should_have_first_round_set_as_closed = () =>
        {
            game.Rounds[0].State.ShouldEqual(RoundState.Closed);
        };

        It should_have_current_round_set_as_opened = () =>
        {
            game.CurrentRound.State.ShouldEqual(RoundState.Opened);
        };

        static Game game;
    }
}
