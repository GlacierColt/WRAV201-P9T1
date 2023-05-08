using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9T1_Y2S1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {

        }
    }

    class WRAV201List
    {
        public static int NumberOfInstances = 0;    //This counter will store the number of times this class (or its descendants) were instantiated
        protected int Size = 0;     //The number of items currently in the list

        public WRAV201List()
        {
            NumberOfInstances++;    //A new instance was created
        }
        public virtual Object GetFirst()
        //Post Condition:  The first element in the list is returned. If the list is empty, null is returned
        {
            return null;
        }

        public virtual Object GetLast()
        //Post Condition:  The last element in the list is returned. If the list is empty, null is returned
        {
            return null;
        }

        public virtual Object GetAt(int pos)
        //Post Condition:  The element at position pos in the list is returned. If there are fewer than pos+1 elements in the list then null is returned
        {
            return null;
        }

        public virtual void InsertAtFirst(Object cargo)
        //Post Condition: The object cargo is inserted in the front of the list (all other elements are shifted one position down the list)
        {
        }

        public virtual void InsertAtLast(Object cargo)
        //Post Condition: The object cargo is inserted at the end of the list
        {
        }

        public virtual bool InsertAt(int pos, Object cargo)
        //Post Condition: The object cargo is inserted at position pos in the list. All existing elements from pos are shifted one position down the list).  True is returned if successful.
        //There must be at least pos elements in the list otherwise false is returned. 
        {
            return false;
        }

        public virtual Object Search(Object find)
        //Post Condition: The list is traversed and the first object for which .Equals(find) is true is returned. If no such object is found then null is returned.
        {
            return null;
        }

        public virtual Object DeleteFirst()
        //Post Condition: The first object in the list is removed and returned. If the list is empty then null is returned.
        {
            return null;
        }

        public virtual Object DeleteLast()
        //Post Condition: The last object in the list is removed and returned. If the list is empty then null is returned.
        {
            return null;
        }

        public virtual Object DeleteAt(int pos)
        //Post Condition: The object at position pos in the list is removed and returned. If there is no object at pos then null is returned.
        {
            return null;
        }

        public int Count()
        //Post Condition: The number of elments in the list is returned.    
        {
            return Size;
        }

        public virtual WRAV201Iterator GetIterator()
        //Post Condition: Returns a WRAV201Iterator that can be used to traverse the list
        {
            return null;
        }
    }

    class WRAV201LinkedList : WRAV201List
    {
        private Node head; //Points to first element in the LL if list is not empty
        private Node tail; //Points to last element in the LL if list is not empty
        public WRAV201LinkedList() : base()
        {
        }

        

        public override Object GetFirst()
        {
            if (head != null) return head.Cargo;
            else return null;
        }

        public override Object GetLast()
        {
            if (tail != null) return tail.Cargo;
            else return null;
        }

        public override Object GetAt(int pos)
        {
            if (head == null) return null;  //The list is empty
            else
            {
                Node temp = head;
                while (pos > 0)     // Decrement pos and more to next node until pos == 0
                {
                    pos--;
                    temp = temp.next;
                    if (temp == null) return null;  //There is no element at pos
                }

                return temp.Cargo;
            }
        }

        public override void InsertAtFirst(Object cargo)
        {
            Node newnode = new Node();
            newnode.Cargo = cargo;
            newnode.next = head;
            head = newnode;

            if (newnode.next == null) tail = newnode;       //The list only contains one element so tail must point to same node as head
            Size++;
        }

        public override void InsertAtLast(Object cargo)
        {
            Node newnode = new Node();
            newnode.Cargo = cargo;

            if (head == null)           //The list is empty
            {
                head = newnode;
                tail = newnode;
            }
            else
            {
                tail.next = newnode;
                tail = newnode;
            }
            Size++;
        }

        public override bool InsertAt(int pos, Object cargo)
        {
            Node newnode = new Node();
            newnode.Cargo = cargo;

            if (pos == 0)
            {
                newnode.next = head;
                head = newnode;

                if (tail == null) tail = newnode;       //Check if this is the only element in the list

                Size++;
                return true;
            }
            else
            {
                if (head == null) return false;         //The list is empty and pos is not 0
                pos--;                                  // We want to find the node before the insert spot
                Node temp = head;
                while (pos > 0)
                {
                    pos--;
                    temp = temp.next;
                    if (temp == null) return false;     //The is no node before the insert spot so insertion fails
                }

                if (temp == tail) tail = newnode;       //The node is to be added to the end of the list

                newnode.next = temp.next;
                temp.next = newnode;
                Size++;
                return true;
            }
        }

        public override Object Search(Object find)
        {
            Node temp = head;
            while ((temp != null) && (!temp.Cargo.Equals(find)))    //Use Cargo's Equals() method to check for equality
            {
                temp = temp.next;
            }

            if (temp != null) return temp.Cargo;
            else return null;                                       //The element was not found
        }

        public override Object DeleteFirst()
        {
            if (head == null) return null;              //The list is empty. Delete fails
            else
            {
                Object tempCargo = head.Cargo;
                head = head.next;
                if (head == null) tail = null;          //There was only one element in the list. Now it is empty
                Size--;
                return tempCargo;
            }
        }

        public override Object DeleteLast()
        {
            if (head == null) return null;              //The list is empty. Delete fails
            else if (head.next == null)                 //The list contains only one element
            {
                Object tempCargo = head.Cargo;
                head = null;
                tail = null;
                Size--;
                return tempCargo;
            }
            else
            {
                Node temp = head;
                while (temp.next.next != null)          //A reference to the last element in the list must be found
                {
                    temp = temp.next;
                }
                Object tempCargo = temp.next.Cargo;
                temp.next = null;                       //temp is now the new last element in the list
                tail = temp;
                Size--;
                return tempCargo;
            }
        }

        public override Object DeleteAt(int pos)
        {
            if (head == null) return null;          //The list is empty. Delete fails

            if (pos == 0)                           //Deleting the first element in the lsit
            {
                Object tempCargo = head.Cargo;
                head = head.next;
                if (head == null) tail = null;      //The only element in the list was deleted
                Size--;
                return tempCargo;
            }
            else
            {
                pos--;                              // We want to find the node before the remove spot so that we can have a reference to the node to be deleted
                Node temp = head;
                while (pos > 0)
                {
                    pos--;
                    temp = temp.next;
                    if (temp == null) return null;  //There is no element at pos. Delete fails
                }

                if (temp.next == null) return null; //There is no element at pos. Delete fails
                else
                {
                    Object tempCargo = temp.next.Cargo;
                    temp.next = temp.next.next;
                    if (temp.next == null) tail = temp; //The deleted node was the last element in the list
                    Size--;
                    return tempCargo;
                }
            }
        }

        public WRAV201ArrayList GetArrayListClone()
        // Post Condition: returns an WRAV201ArrayList object that contains the same elements as the current linked list
        {
            WRAV201ArrayList NewArray = new WRAV201ArrayList();
            Node temp = head;
            while (temp.next != null)
            {
                NewArray.InsertAtLast(temp.next.Cargo);
                temp = temp.next;
            }
            return NewArray;
        }

        public override WRAV201Iterator GetIterator()
        {
            return null; //Place code for Task 3 here
        }

    }

    class Node
    {
        public Object Cargo;  //This linked list stores Objects
        public Node next;  //Reference to next list element
    }

    class WRAV201ArrayList : WRAV201List
    {
        private int MaxSize = 10;       //Initially the array will have space for 10 elements.
        private Object[] array;
        public WRAV201ArrayList() : base()
        {
            array = new Object[MaxSize];
        }

        public override Object GetFirst()
        {
            if (Size == 0) return null;
            else return array[0];
        }

        public override Object GetLast()
        {
            if (Size == 0) return null;
            else return array[Size - 1];
        }

        public override Object GetAt(int pos)
        {
            if (pos >= Size) return null;
            else return array[pos];
        }

        public override void InsertAtFirst(Object cargo)
        {
            if (Size == MaxSize) DoubleMaxSize();       //Make sure there is space for the new element

            for (int i = Size; i > 0; i--)          //Shift all elements one space down
            {
                array[i] = array[i - 1];
            }
            array[0] = cargo;
            Size++;
        }

        public override void InsertAtLast(Object cargo)
        {
            if (Size == MaxSize) DoubleMaxSize();       //Make sure there is space for the new element

            array[Size] = cargo;
            Size++;
        }

        public override bool InsertAt(int pos, Object cargo)
        {
            if (Size == MaxSize) DoubleMaxSize();       //Make sure there is space for the new element

            if (pos > Size) return false;               //There is not at least pos elements in the list
            else
            {
                for (int i = Size; i > pos; i--)        //Shift the appropriate number of elements to make space at pos
                {
                    array[i] = array[i - 1];
                }
                array[pos] = cargo;
                Size++;
                return true;
            }
        }

        public override Object Search(Object find)
        {
            for (int i = 0; i < Size; i++)
            {
                if (array[i].Equals(find)) return array[i];         //Use Cargo's Equals() method to check for equality
            }
            return null;
        }

        public override Object DeleteFirst()
        {
            if (Size == 0) return null;         //List is empty. Delete fails
            else
            {
                Object temp = array[0];
                for (int i = 0; i < Size - 1; i++)                 //Shift all the elements up one space
                {
                    array[i] = array[i + 1];
                }
                Size--;
                return temp;
            }
        }

        public override Object DeleteLast()
        {
            if (Size == 0) return null;         //List is empty. Delete fails
            {
                Size--;
                return array[Size];
            }
        }

        public override Object DeleteAt(int pos)
        {
            if (pos >= Size) return null;           //No element at pos. Delete fails
            else
            {
                Object temp = array[0];
                for (int i = pos; i < Size - 1; i++)            //Shift the appropriate number of elements up one space to fill the gap left at pos
                {
                    array[i] = array[i + 1];
                }
                Size--;
                return temp;
            }
        }

        private void DoubleMaxSize()
        // Post Condition: a new array that is double the size of the old array was created and the elements from the old array was copied across. 
        {
            Object[] oldarray = array;
            MaxSize = MaxSize * 2;              //Double the size of the array       
            array = new Object[MaxSize];

            for (int i = 0; i < Size; i++)       //Copy elements across. 
            {
                array[i] = oldarray[i];
            }
        }

        public WRAV201LinkedList GetLinkedListClone()
        // Post Condition: returns an WRAV201LinkedList object that contains the same elements as the current array
        {
            WRAV201LinkedList newLinkedList = new WRAV201LinkedList();
             //Place code for Task 2 here
             for (int i = 0; i < Count(); i++)
             {
                newLinkedList.InsertAtLast(array[i]);
             }
             return newLinkedList;  
        }

        public override WRAV201Iterator GetIterator()
        {
            return null; //Place code for Task 3 here 
        }
    }

    class WRAV201Iterator
    {
        public virtual bool HasMoreElements()
        //Post Condition: returns true if there are more elments in the list to visit. This method should be O(1)
        {
            return false;
        }

        public virtual Object GetNextElement()
        //Post Condition: returns the next element in the list. If the list contains no more elements then return null. This method should be O(1)
        {
            return null;
        }
    }
}