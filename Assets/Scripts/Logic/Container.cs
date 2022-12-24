using System.Collections;
using System;
namespace BattleCards
{
    public class Container<T> : IEnumerable where T : Card
    {
        /// <summary>
        /// elementos de una coleccion. Su proposito inicial es guardar cartas, hackers o algoritmos
        /// </summary>
        private T[] Elements;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Recibe en su constructor la cantidad de elementos</param>
        public Container(int n)
        {
            Elements = new T[n];

        }
        /// <summary>
        /// implementa IEnumerable para poder hacerle foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        /// <summary>
        /// indexer para trabajar mas facil, tal si fuera un arreglo
        /// </summary>
        /// <value></value>
        public T this[int index]
        {
            get
            {
                return Elements[index];
            }
            set
            {
                Elements[index] = value;
            }
        }

        public void MoveCard<P>(int i, Container<P> meta, int j) where P : T
        {
            meta[j] = (P)Elements[i];
            Elements[i] = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>cantidad de elementos no nulos</returns>
        public int Count()
        {
            int t = 0;
            for (int i = 0; i < Elements.Length; i++)
            {
                if(Elements[i] != null)
                {
                    t++;
                }    
            }
            return t;
        }
        
        public int Length()
        {
            return Elements.Length;
        }

    }

}