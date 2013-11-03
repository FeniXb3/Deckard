using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Card : IEquatable<Card>
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

        public bool Equals(Card other)
        {
            if (other == null)
                return false;

            if (this.Attributes.Count != other.Attributes.Count)
                return false;

            foreach (var attr in Attributes)
            {
                if (!other.Attributes.ContainsKey(attr.Key))
                    return false;

                if (!Attributes[attr.Key].Equals(other.Attributes[attr.Key]))
                    return false;
            }

            return true;
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            Card c = other as Card;


            if (this.Attributes.Count != c.Attributes.Count)
                return false;

            foreach (var attr in Attributes)
            {
                if (!c.Attributes.ContainsKey(attr.Key))
                    return false;

                if (!Attributes[attr.Key].Equals(c.Attributes[attr.Key]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 37;

            foreach (var attr in Attributes)
            {
                hashCode += 3 * attr.Key.GetHashCode() + 7 * attr.Value.GetHashCode();
            }

            return hashCode;
        }
    }
}
