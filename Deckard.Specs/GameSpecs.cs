﻿using Machine.Fakes;
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

            game.SourceDecks.Add(source1);

            hero1 = new Player();
            hero1.Hand = dest1;
            hero2 = new Player();
            hero2.Hand = dest2;

            game.Heros.Add(hero1);
            game.Heros.Add(hero2);
        };

        Because of = () =>
        {
            game.DealCards(game.SourceDecks[0], new List<Deck> { game.Heros[0].Hand, game.Heros[1].Hand }, cardsToDrawFromSource1Count);
        };

        It should_have_less_cards_in_source_decks = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(source1Size - cardsToDrawFromSource1Count * 2);
        };

        It should_have_more_cards_in_destination_decs = () =>
        {
            game.Heros[0].Hand.Size.ShouldEqual(dest1Size + cardsToDrawFromSource1Count);
            game.Heros[1].Hand.Size.ShouldEqual(dest2Size + cardsToDrawFromSource1Count);
        };
        
        It should_have_source_decks_without_dealt_cards = () =>
        {
            game.SourceDecks[0].Cards.ShouldNotContain(cardsToDest1);
            game.SourceDecks[0].Cards.ShouldNotContain(cardsToDest2);
        };

        It should_have_destination_decks_with_dealt_cards = () =>
        {
            game.Heros[0].Hand.Cards.ShouldContain(cardsToDest1);
            game.Heros[1].Hand.Cards.ShouldContain(cardsToDest2);
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

            game.SourceDecks.Add(source1);

            hero1 = new Player();
            hero1.Hand = dest1;
            hero2 = new Player();
            hero2.Hand = dest2;

            game.Heros.Add(hero1);
            game.Heros.Add(hero2);
        };

        Because of = () =>
        {
            game.DealCards(source1, new List<Deck> { game.Heros[0].Hand, game.Heros[1].Hand }, 1);
        };

        It should_have_destination_deck_with_fewer_cards = () =>
        {
            game.Heros[1].Hand.Size.ShouldBeLessThan(game.Heros[0].Hand.Size);
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

            game.SourceDecks.Add(source1);

            hero1 = new Player();
            hero1.Hand = dest1;
            hero2 = new Player();
            hero2.Hand = dest2;

            game.Heros.Add(hero1);
            game.Heros.Add(hero2);
        };

        Because of = () =>
        {
            game.DealCards(game.SourceDecks[0], new List<Deck> { game.Heros[0].Hand, game.Heros[1].Hand });
        };

        It should_have_no_cards_in_source_deck = () =>
        {
            game.SourceDecks[0].Size.ShouldEqual(0);
        };

        It should_have_all_cards_from_source_in_destination_decks = () =>
        {
            sourceSize.ShouldEqual(game.Heros[0].Hand.Size + game.Heros[1].Hand.Size);
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
            game.CurrentRound = 1;
        };

        Because of = () => 
        {
            game.EndRound();            
        };

        It should_have_second_round_as_actual = () => 
        {
            game.CurrentRound.ShouldEqual(2);
        };
        static Game game;
    }
}
