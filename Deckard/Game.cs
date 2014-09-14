using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Game
    {
        public Deck SourceDeck;
        public List<Player> Players { get; set; }

        public Game()
        {
            Players = new List<Player>();
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
            OnRoundEnded(this, new PlayerActionEventArgs(CurrentPlayer));
            
            CurrentRound.Close();

            if (ForcedNextPlayer == null)
            {
                CurrentPlayerNumber = (CurrentPlayerNumber + 1) % Players.Count;
            }
            else
            {
                CurrentPlayerNumber = Players.IndexOf(ForcedNextPlayer);
                ForcedNextPlayer = null;
            }
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
                return Players[CurrentPlayerNumber];
            }
        }

        public Player NextPlayer
        {
            get
            {
                return Players[(CurrentPlayerNumber + 1) % Players.Count];
            }
        }

        public Player PreviousPlayer
        {
            get
            {
                int prevNumber = CurrentPlayerNumber == 0 ? Players.Count - 1 : CurrentPlayerNumber - 1;
                return Players[prevNumber];
            }
        }

        public int CurrentPlayerNumber { get; set; }

        public delegate void PlayerActionEventHandler(object oSender, PlayerActionEventArgs oEventArgs);
        public event PlayerActionEventHandler CustomAction;
        public event PlayerActionEventHandler DefaultAction;
        public event PlayerActionEventHandler RoundEndAction;
        
        public void OnCustomActionTaken(object sender, EventArgs eventArgs)
        {
            if (CustomAction != null)
            {
                CustomAction(sender, eventArgs as PlayerActionEventArgs);
            }
            IsCustomActionSet = false;
        }


        public void OnDefaultActionTaken(object sender, EventArgs eventArgs)
        {
            if (DefaultAction != null)
            {
                DefaultAction(sender, eventArgs as PlayerActionEventArgs);
            }
        }

        public void OnRoundEnded(object sender, EventArgs eventArgs)
        {
            if (RoundEndAction != null)
            {
                RoundEndAction(sender, eventArgs as PlayerActionEventArgs);
            }
        }

        public void DealFirstCards(int cardsCount)
        {
            List<Deck> allHands = new List<Deck>();

            foreach (var player in Players)
                allHands.Add(player.Hand);

            DealCards(SourceDeck, allHands, cardsCount);
        
        }

        public Deck DestinationDeck { get; set; }
        public Predicate<Card> NextCardCriteria;
        public Predicate<Card> CustomNextCardCriteria;

        public bool CheckCard(Card card)
        {
            bool isValid = false;

            if (CustomNextCardCriteria != null)
            {
                isValid = CustomNextCardCriteria(card);
            }
            else
            {
                isValid = NextCardCriteria(card);
            }

            return isValid;
        }

        public bool IsCustomActionSet;

        public void ForceNextPlayerTo(Player player)
        {
            ForcedNextPlayer = player;
        }

        public Player ForcedNextPlayer { get; set; }
    }
}
