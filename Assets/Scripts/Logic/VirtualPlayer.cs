using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
namespace BattleCards
{
    public class VirtualPlayer
    {   
        /// <summary>
        /// Selecciona el hacker con mayor capacidad de un container dado
        /// </summary>
        /// <param name="container">Potencialmente la mano</param>
        /// <returns>el indice del hacker buscado en el container</returns>
        private int SearchHacker(Container<Card> container)
        {
            Dictionary<Hacker, int> InField = new Dictionary<Hacker, int>();
            Hacker Temp = new Hacker("@Hacker@Temp@-1"); ;
            int t = -1;

            //Busca todas las posiciones q tengan cartas
            for (int i = 0; i < container.Length(); i++)
            {
                if (container[i] != null && container[i] is Hacker)
                {
                    InField[(Hacker)container[i]] = i;
                    Temp = (Hacker)container[i];
                    t = i;
                }
            }

            //ordena las cartas encontradas
            foreach (var item in InField.Keys)
            {
                if (item.Capacity >= Temp.Capacity)
                {
                    Temp = item;
                    t = InField[item];
                }
            }
            return t;
        }
        /// <summary>
        /// Busca la posicion mas atacada que no contiene algoritmos protegiendola
        /// </summary>
        /// <param name="container">Potencialmente HackerField del enemigo</param>
        /// <param name="busy">Potencialmente AlgorithmField propio</param>
        /// <returns>indice del lugar a defender</returns>
        private int SearchHacker(Container<Hacker> container, Container<Algorithm> busy)
        {
            Dictionary<Hacker, int> InField = new Dictionary<Hacker, int>();
            Hacker Temp = new Hacker("@Hacker@Temp@-1"); ;
            int t = -1;

            //Busca todas las posiciones q tengan cartas
            for (int i = 0; i < container.Length(); i++)
            {
                if (container[i] != null)
                {
                    InField[container[i]] = i;
                    Temp = container[i];
                    t = i;
                }
            }

            //ordena las cartas encontradas
            foreach (var item in InField.Keys)
            {
                if (item.Capacity >= Temp.Capacity && busy[InField[item]] == null)
                {
                    Temp = item;
                    t = InField[item];
                }
            }
            return t;
        }

        /// <summary>
        /// Busca una carta Effect en un container
        /// </summary>
        /// <param name="container">Potencialmente la mano</param>
        /// <returns>el indice de la carta encontrada</returns>
        private int SearchEffect(Container<Card> container)
        {
            for (int i = 0; i < container.Length(); i++)
            {
                if (container[i] is Effect)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Busca el mejor algoritmo en un container
        /// </summary>
        /// <param name="container">Potencialmente la mano</param>
        /// <returns>indice del algortimo encontrado en el container</returns>
        private int SearchAlgorithm(Container<Card> container)
        {
            Dictionary<Algorithm, int> InField = new Dictionary<Algorithm, int>();
            Algorithm Temp = new Algorithm("@Algorithm@Temp@-1");
            int t = -1;

            //Busca todas las posiciones q tengan cartas
            for (int i = 0; i < container.Length(); i++)
            {
                if (container[i] != null && container[i] is Algorithm)
                {
                    InField[container[i] as Algorithm] = i;
                    Temp = container[i] as Algorithm;
                    t = i;
                }
            }

            //ordena las cartas encontradas
            foreach (var item in InField.Keys)
            {
                if (item.Resistance.life >= Temp.Resistance.life)
                {
                    Temp = item;
                    t = InField[item];
                }
            }
            return t;
        }


        /// <summary>
        /// Seleccionar la carta mas adecuada para invocar
        /// </summary>
        /// <param name="review">Revisa cual es la posicion del campo q debe ser defendida o atacada</param>
        /// <param name="hand">Mano del jugador</param>
        /// <param name="work">Lugar donde se colocara la carta</param>
        /// <typeparam name="T">Tipo contrario al que se desea invocar</typeparam>
        /// <typeparam name="S">tipo que se quiere invocar</typeparam>
        


        public void InvokeAlgorithm(Container<Hacker> review, Container<Card> hand, Container<Algorithm> work)
        {

            if (SearchHacker(review, work) != -1)
            {
                for (int i = 0; i < work.Length() && Player.Played > 0; i++)
                {
                    if (SearchAlgorithm(hand) != -1 && work[SearchHacker(review, work)] == null)
                    {
                        work[SearchHacker(review, work)] = (Algorithm)hand[SearchAlgorithm(hand)]; ;
                        hand[SearchAlgorithm(hand)] = null;
                        Player.Played--;
                    }
                }
            }
            for (int i = 0; i < work.Length() && Player.Played > 0; i++)
            {
                if (SearchAlgorithm(hand) != -1 && work[i] == null)
                {
                    work[i] = (Algorithm)hand[SearchAlgorithm(hand)];
                    hand[SearchAlgorithm(hand)] = null;
                    Player.Played--;
                }
            }
        }
        public void InvokeEffect(Container<Card> hand, Container<Effect> work)
        {
            Random rnd = new Random();

            for (int i = 0; i < work.Length() && Player.Played > 0; i++)
            {
                if (rnd.Next(2) == 1)
                {
                    if (SearchEffect(hand) != -1 && work[i] == null)
                    {
                        work[i] = (Effect)hand[SearchEffect(hand)]; ;
                        hand[SearchEffect(hand)] = null;
                        Player.Played--;
                        break;
                    }
                }
            }
        }
        public void InvokeHacker(Container<Algorithm> review, Container<Card> hand, Container<Hacker> work)
        {
            for (int i = 0; i < work.Length() && Player.Played > 0; i++)
            {

                if (review[i] == null)
                {
                    if (SearchHacker(hand) != -1 && work[i] == null)
                    {
                        work[i] = (Hacker)hand[SearchHacker(hand)];
                        hand[SearchHacker(hand)] = null;
                        Player.Played--;
                    }
                }
            }
            for (int i = 0; i < work.Length() && Player.Played > 0; i++)
            {
                if (SearchHacker(hand) != -1 && work[i] == null)
                {
                    work[i] = (Hacker)hand[SearchHacker(hand)];
                    hand[SearchHacker(hand)] = null;
                    Player.Played--;
                }
            }
        }
    }
}