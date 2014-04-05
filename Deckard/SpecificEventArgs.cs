using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deckard
{
    public class CardActionEventArgs : EventArgs
    {
        //public Card TargetCard;
        public Player TargetPlayer;

        //public CardActionEventArgs(Card targetCard)
        //{
        //    TargetCard = targetCard;
        //}

        public CardActionEventArgs(Player targetPlayer)
        {
            TargetPlayer = targetPlayer;
        }

        //public CardActionEventArgs(Card targetCard, Player targetPlayer)
        //{
        //    TargetCard = targetCard;
        //    TargetPlayer = targetPlayer;
        //}
    }
}
