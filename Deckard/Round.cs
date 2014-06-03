using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Round
    {
        public RoundState State { get; set; }

        internal void Close()
        {
            State = RoundState.Closed;
        }
    }



    public enum RoundState
    {
        Opened,
        Pending,
        Closed
    }
}
