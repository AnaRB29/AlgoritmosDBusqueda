using System;
using System.Collections.Generic;
using Tuple = Eppy.Tuple;

namespace ESarkis
{
    public class PriorityQueue<T>
    {

        List<Tuple<T, double>> elements = new List<Tuple<T, double>>();
        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, double priorityValue)
        {
            elements.Add(Tuple.Create(item, priorityValue));
        }

        public T Dequeue()
        {
            int bestPriorityIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestPriorityIndex].Item2)
                {
                    bestPriorityIndex = i;
                }
            }

            T bestItem = elements[bestPriorityIndex].Item1;
            elements.RemoveAt(bestPriorityIndex);
            return bestItem;
        }

        public T Peek()
        {
            int bestPriorityIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestPriorityIndex].Item2)
                {
                    bestPriorityIndex = i;
                }
            }

            T bestItem = elements[bestPriorityIndex].Item1;
            return bestItem;
        }

        public bool Contains(T item)
        {
            foreach (var element in elements)
            {
                if (element.Item1.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            elements.Clear();
        }
    }
}