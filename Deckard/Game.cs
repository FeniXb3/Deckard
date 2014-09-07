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
            if (CurrentPlayer.CardsPlayed == 0)
            {
                OnActionTaken(this, new PlayerActionEventArgs(CurrentPlayer));
            }

            CurrentRound.Close();
            CurrentPlayerNumber = (CurrentPlayerNumber + 1) % Heros.Count;
            CurrentPlayer.CardsPlayed = 0;
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

        public Player CurrentPlayer
        {
            get
            {
                return Heros[CurrentPlayerNumber];
            }
        }

        public Player NextPlayer
        {
            get
            {
                return Heros[(CurrentPlayerNumber + 1) % Heros.Count];
            }
        }

        public Player PreviousPlayer
        {
            get
            {
                int prevNumber = CurrentPlayerNumber == 0 ? Heros.Count - 1 : CurrentPlayerNumber - 1;
                return Heros[prevNumber];
            }
        }


        public int CurrentPlayerNumber { get; set; }


        public delegate void PlayerActionEventHandler(object oSender, PlayerActionEventArgs oEventArgs);
        public event PlayerActionEventHandler Action;


        public void OnActionTaken(object sender, EventArgs eventArgs)
        {
            if (Action != null)
            {
                Action(sender, eventArgs as PlayerActionEventArgs);
            }
        }

        public void DealFirstCards(int cardsCount)
        {
            List<Deck> allHands = new List<Deck>();

            foreach (var player in Heros)
                allHands.Add(player.Hand);

            DealCards(SourceDecks[0], allHands, cardsCount);
        
        }
    }
}
