using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NameLookup.Tests
{
    public class Dictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        private LinkedListCell<KeyValuePair<TKey, TValue>> _header = new LinkedListCell<KeyValuePair<TKey, TValue>>();

        private static void CheckNull(TKey x)
        {
            if (x == null)
            {
                throw new ArgumentNullException();
            }
        }

        //Method to get the last cell. It returns the last cell, less than the x value

        private LinkedListCell<KeyValuePair<TKey, TValue>> GetLastCell(TKey x)
        {
            LinkedListCell<KeyValuePair<TKey, TValue>> temp = _header;
            while (temp.Next != null && temp.Next.Data.Key.CompareTo(x) < 0)
            {
                temp = temp.Next;
            }
            return temp;
        }

        //If the next cell is non-null, and conatins the given key, set the out parameter to the associated value, and return true.
        //Otherwise, Set the out parameter to the default value, and return false.


        public bool TryGetValue(TKey k, out TValue v)
        {
            CheckNull(k);
            LinkedListCell<KeyValuePair<TKey, TValue>> cell = GetLastCell(k);
            if (cell.Next != null && cell.Next.Data.Key.CompareTo(k) == 0)
            {
                v = cell.Next.Data.Value;
                return true;
            }
            else
            {
                v = default(TValue);
                return false;
            }
        }

        //Essentially the opposite of TRygetValue
        // If the last key is not null, and contains the given key
        //   throw an argumentExceptionargument.
        //      Otherwise, set its data to a new kayvaluepair, and insert a new cell
        public void Add(TKey k, TValue v)
        {
            CheckNull(k);
            LinkedListCell<KeyValuePair<TKey, TValue>> cell = GetLastCell(k);
            if (cell.Next != null && cell.Next.Data.Key.CompareTo(k) == 0)
            {
                throw new ArgumentException();
            }
            else
            {
                LinkedListCell<KeyValuePair<TKey, TValue>> newCell = new LinkedListCell<KeyValuePair<TKey, TValue>>();
                KeyValuePair<TKey,TValue> newPair = new KeyValuePair<TKey,TValue>(k,v);
                newCell.Data = newPair;
                newCell.Next = cell.Next;
                cell.Next = newCell;
            }
        }
    }
}
