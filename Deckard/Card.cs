using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Card
    {
        public Dictionary<string, string> Attributes;

        public Card()
        {
            Attributes = new Dictionary<string, string>();
        }
    }
}
