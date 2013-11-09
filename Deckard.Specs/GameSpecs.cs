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
        };

        Because of = () =>
        {
            game.DealCards(source1, new List<Deck> { dest1, dest2 }, cardsToDrawFromSource1Count);
        };

        It should_have_less_cards_in_source_decks = () =>
        {
            source1.Size.ShouldEqual(source1Size - cardsToDrawFromSource1Count * 2);
        };

        It should_have_more_cards_in_destination_decs = () =>
        {
            dest1.Size.ShouldEqual(dest1Size + cardsToDrawFromSource1Count);
            dest2.Size.ShouldEqual(dest2Size + cardsToDrawFromSource1Count);
        };
        
        It should_have_source_decks_without_dealt_cards = () =>
        {
            source1.Cards.ShouldNotContain(cardsToDest1);
            source1.Cards.ShouldNotContain(cardsToDest2);
        };

        It should_have_destination_decks_with_dealt_cards = () =>
        {
            dest1.Cards.ShouldContain(cardsToDest1);
            dest2.Cards.ShouldContain(cardsToDest2);
        };

        static Deck source1;
        static Deck dest1, dest2;
        static int dest1Size, dest2Size;
        static int source1Size;
        static int cardsToDrawFromSource1Count;
        static Game game;
        static List<Card> cardsToDest1, cardsToDest2;
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
        };

        Because of = () =>
        {
            game.DealCards(source1, new List<Deck> { dest1, dest2 }, 1);
        };

        It should_have_destination_deck_with_fewer_cards = () =>
        {
            dest2.Size.ShouldBeLessThan(dest1.Size);
        };

        static Deck source1;
        static Deck dest1, dest2;
        static Game game;
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
        };

        Because of = () =>
        {
            game.DealCards(source1, new List<Deck> { dest1, dest2 });
        };

        It should_have_no_cards_in_source_deck = () =>
        {
            source1.Size.ShouldEqual(0);
        };

        It should_have_all_cards_from_source_in_destination_decks = () =>
        {
            sourceSize.ShouldEqual(dest1.Size + dest2.Size);
        };

        static Deck source1;
        static Deck dest1, dest2;
        static Game game;
        static int sourceSize;
    }
}
