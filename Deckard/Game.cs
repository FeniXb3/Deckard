using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Game
    {
        public Deck SourceDeck;
        public ObservableCollection<Player> Players { get; set; }
        public int CurrentPlayerNumber { get; set; }
        public bool IsCustomActionSet { get; set; }
        protected Player ForcedNextPlayer { get; set; }
        public Deck DestinationDeck { get; set; }
        public Predicate<Card> NextCardCriteria { get; set; }
        public Predicate<Card> CustomNextCardCriteria { get; set; }
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

        public delegate void PlayerActionEventHandler(object oSender, PlayerActionEventArgs oEventArgs);
        public event PlayerActionEventHandler CustomAction;
        public event PlayerActionEventHandler DefaultAction;
        public event PlayerActionEventHandler TurnEndAction;
        public event EventHandler Starting;

        public Game()
        {
            Players = new ObservableCollection<Player>();
        }

        public void DealCards(Deck source, List<Deck> destinationDecks, int? cardsCount = null)
        {
            if (!cardsCount.HasValue)
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

        public void EndTurn()
        {
            OnTurnEnded(this, new PlayerActionEventArgs(CurrentPlayer));
            

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
        }

        public void Start()
        {
            OnStarting(this, EventArgs.Empty);
        }

        public void OnCustomActionTaken(object sender, EventArgs eventArgs)
        {
            CustomAction?.Invoke(sender, eventArgs as PlayerActionEventArgs);
            IsCustomActionSet = false;
        }

        public void OnDefaultActionTaken(object sender, EventArgs eventArgs)
        {
            DefaultAction?.Invoke(sender, eventArgs as PlayerActionEventArgs);
        }

        public void OnTurnEnded(object sender, EventArgs eventArgs)
        {
            TurnEndAction?.Invoke(sender, eventArgs as PlayerActionEventArgs);
        }

        public void DealFirstCards(int cardsCount)
        {
            List<Deck> allHands = new List<Deck>();

            foreach (var player in Players)
                allHands.Add(player.Hand);

            DealCards(SourceDeck, allHands, cardsCount);
        
        }

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

        public void ForceNextPlayerTo(Player player)
        {
            ForcedNextPlayer = player;
        }

        public void OnStarting(object sender, EventArgs eventArgs)
        {
            Starting?.Invoke(sender, eventArgs);
        }

    }
}
