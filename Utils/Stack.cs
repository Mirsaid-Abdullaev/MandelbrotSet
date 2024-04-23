﻿namespace MandelbrotSet.Utils
{
    internal class Stack<T>
    {
        private int Size;
        public int CurrentSize
        {
            get
            {
                return Size;
            }
        }

        private readonly int MaxSize;
        public int GetMaxSize
        {
            get
            {
                return MaxSize;
            }
        }

        private T[] Values;

        public Stack(bool FixedSize = false, int MaxSize = 250)
        {
            if (FixedSize)
            {
                this.MaxSize = MaxSize;
            }
            else
            {
                this.MaxSize = -1;
            }

            Values = Array.Empty<T>();
            Size = 0;
        }

        public T Pop()
        {
            if (Size > 0)
            {
                T Result = Values.Last();
                T[] Temp = new T[Values.Length];
                Array.Copy(Values, Temp, Values.Length);
                Values = new T[Temp.Length - 1];
                Array.Copy(Temp, 0, Values, 0, Values.Length);
                Size--;
                return Result;
            }
            else //empty stack
            {
                throw new Exception("Error: stack is empty, nothing to pop.");
            }
        }

        public void Push(T Item)
        {
            if (Size == MaxSize)
            {
                throw new Exception("Error: stack full.");
            }
            else
            {
                T[] Temp = new T[Values.Length];
                Array.Copy(Values, Temp, Values.Length);
                Values = new T[Values.Length + 1];
                Array.Copy(Temp, Values, Temp.Length);
                Values[^1] = Item;
                Size++;
            }
        }

        public T Peek()
        {
            if (Size > 0)
            {
                T Result = Values.Last();
                return Result;
            }
            else //empty stack
            {
                throw new Exception("Error: empty stack, nothing to peek)");
            }
        }

        public List<T> StackToList()
        {
            return Values.ToList();
        }

        public void ForgetFirstNItems(int FirstNItems)
        {
            if (Size >= FirstNItems)
            {
                T[] Temp = new T[Values.Length - FirstNItems];
                Array.Copy(Values, FirstNItems, Temp, 0, Temp.Length);
                Values = new T[Temp.Length];
                Array.Copy(Temp, Values, Temp.Length);
            }
            else
            {
                throw new ArgumentException("Error: stack does not contain that many items.");
            }
        }

        public override string ToString()
        {
            return "{" + string.Join(',', Values) + "}";
        }
    }
}