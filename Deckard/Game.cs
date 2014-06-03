using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Game
    {
        public List<Deck> SourceDecks;
        public List<Player> Heros { get; set; }

        public Game()
        {
            SourceDecks = new List<Deck>();
            Heros = new List<Player>();
            Rounds = new List<Round>();
            CurrentRoundNumber = -1;
        }

        public void DealCards(Deck source, List<Deck> destinationDecks, int cardsCount = -1)
        {
            if (cardsCount == -1)
                cardsCount = source.Size;

            while (cardsCount-- > 0)
            {
                foreach (var deck in destinationDecks)
                {
                    if (source.Size == 0)
                        return;

                    deck.Cards.Add(source.TakeAndRemoveCard());
                }
            }
        }


        public int CurrentRoundNumber { get; set; }

        public void EndRound()
        {
            CurrentRound.Close();
            NextRound();
        }

        private void NextRound()
        {
            AddRound(RoundState.Opened);
        }

        private void AddRound(RoundState roundState)
        {
            Rounds.Add(new Round() { State = roundState });
            if (roundState == RoundState.Opened)
                CurrentRoundNumber = Rounds.Count - 1;
        }

        public List<Round> Rounds { get; set; }

        public Round CurrentRound 
        {
            get 
            {
                return Rounds.FirstOrDefault(r => r.State == RoundState.Opened);
            }
        }

        public void Start()
        {
            AddRound(RoundState.Opened);
        }

    }
}
