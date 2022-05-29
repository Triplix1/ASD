using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip15_tonkonog_7_32_2_
{
    internal static class InterpolationSearch
    {
        public static int SearchInArr(int[] arr, int key)
        {
            int low = 0;
            int mid = -1;
            int high = arr.Length - 1;
            while (arr[low] < key && arr[high] > key)
            {
                if (arr[high] == arr[low])
                    break;
                mid = low + ((key - arr[low]) * (high - low)) / (arr[high] - arr[low]);
                if (arr[mid] < key)
                    low = mid + 1;
                else if (arr[mid] > key)
                    high = mid - 1;
                else
                    return mid;
            }

            if (arr[low] == key)
                return low;
            if (arr[high] == key)
                return high;

            return -1;
        }

        public static int SearhInDoublyList(DoublyLinkedList list, int key)
        {
            int low = 0;
            int mid = -1;
            int high = list.Count - 1;
            DoublyNode lo = list.Head;
            DoublyNode hi = list.Tail;
            DoublyNode m;
            while (lo.Data < key && hi.Data > key)
            {
                if (hi.Data == lo.Data)
                    break;
                mid = low + ((key - lo.Data) * (high - low)) / (hi.Data - lo.Data);
                m = lo;
                for (int i = low + 1; i <= mid; i++)
                {
                    m = m.Next;
                }
                if (m.Data < key )
                {
                    for (int i = low + 1; i <= mid + 1; i++)
                    {
                        lo = lo.Next;
                    }
                    low = mid + 1;
                }
                else if (m.Data > key)
                {
                    for (int i = high - 1; i >= mid - 1; i--)
                    {
                        hi = hi.Previous;
                    }
                    high = mid - 1;
                }
                else
                    return mid;
            }

            if (lo.Data == key)
                return low;
            if (hi.Data == key)
                return high;

            return -1;
        }
    }
}
