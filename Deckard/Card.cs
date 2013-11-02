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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("  Attributes count: {0}\n", Attributes.Count);

            foreach (var attr in Attributes)
            {

                sb.AppendFormat("  + {0} = {1}\n", attr.Key, attr.Value);
            }

            return sb.ToString();
        }
    }
}
