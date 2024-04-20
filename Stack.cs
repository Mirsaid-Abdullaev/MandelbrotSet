using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
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

        private List<T> Values;

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

            Values = new List<T>();
            Size = 0;
        }

        public T? Pop()
        {
            if (Size > 0)
            {
                T Result = Values.Last();
                Values.RemoveAt(Values.Count - 1);
                Size--;
                return Result;
            }
            else //empty stack
            {
                return default;
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
                Values.Add(Item);
                Size++;
            }
        }

        public T? Peek()
        {
            if (Size > 0)
            {
                T Result = Values.Last();
                return Result;
            }
            else //empty stack
            {
                return default;
            }
        }

        public List<T> StackToList()
        {
            return Values.ToList();
        }

    }
}
