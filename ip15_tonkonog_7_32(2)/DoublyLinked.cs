using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ip15_tonkonog_7_32_2_
{
    public class DoublyNode
    {
        public DoublyNode(int data)
        {
            Data = data;
        }
        public int Data { get; set; }
        public DoublyNode Previous { get; set; }
        public DoublyNode Next { get; set; }
    }

    public class DoublyLinkedList
    {
        DoublyNode head;
        DoublyNode tail;
        int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public DoublyNode Head { get { return head; } }
        public DoublyNode Tail { get { return tail; } }

        public void Sort()
        {
            if ((head == null) || (head.Next == null))
            {
                return;
            }

            DoublyNode tmp = head;

            for (int i = 0; i < this.count - 1; i++)
            {
                DoublyNode t = tmp.Next;

                for (int j = i + 1; j < this.count; j++)
                {
                    if (t.Data < tmp.Data)
                    {
                        t.Data = tmp.Data + t.Data;
                        tmp.Data = t.Data - tmp.Data;
                        t.Data = t.Data - tmp.Data;
                    }
                    t = t.Next;

                }
                tmp = tmp.Next;

            }
            return;
        }
        public void Add(int data)
        {
            DoublyNode node = new DoublyNode(data);

            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }

        public bool Contain(int value)
        {
            var tmp = head;
            if (tmp == null) return false;
            while (tmp != tail)
            {
                if (tmp.Data == value)
                {
                    return true;
                }
                tmp = tmp.Next;
            }
            return tail.Data == value;
        }

        public void Output()
        {
            var tmp = head;
            Console.WriteLine();
            for (int i = 0; i < count; i++)
            {
                Console.Write(tmp.Data + " ");
                tmp = tmp.Next;
            }
            Console.WriteLine("\n");
        }
    }

}
