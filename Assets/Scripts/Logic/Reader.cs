using System.Collections.Generic;
using System;
using System.IO;
namespace BattleCards
{
    public class Reader
    {
        /// <summary>
        /// lee los valores de la vida de los jugadores
        /// </summary>
        /// <param name="info"></param>
        /// <returns>el valor como un entero</returns>
        private static int ReadInt(string info)
        {
            string[] words = info.Split('.');
            if (Utils.AllWordAreReserved(words, Utils.Reservedint))
            {
                if (words[0] == "Adversary")
                {
                    if (words[1] == "Life")
                    {
                        return Game.players[(Game.turns + 1) % 2].myLife.life;
                    }
                    throw new SyntaxError();
                }
                if (words[0] == "Own")
                {
                    if (words[1] == "Life")
                    {
                        return Game.players[(Game.turns) % 2].myLife.life;
                    }
                }

            }
            throw new SyntaxError();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element">cadena de texto</param>
        /// <returns>valor de una cadena de texto</returns>
        public static int Value(string element)
        {
            try
            {
                return int.Parse(element);
            }
            catch (System.Exception)
            {
                return ReadInt(element);
                throw new SyntaxError();
            }
        }
        /// <summary>
        /// convierte una cadena de texto en un predicado de tipo EXISTE o PARA TODO sobre los campos
        /// </summary>
        /// <param name="info"Cadena de tecto></param>
        /// <returns></returns>
        public static bool ReadBool(string info)
        {
            string[] temp = info.Split('_');

            if (temp[0] == "Exist")
            {
                if (temp[1] == "Own")
                {
                    if (temp[2] == "HackerField")
                    {
                        return BasicMethods.ExistField<Hacker>(CreateFilter(temp[3]), Game.players[(Game.turns) % 2].myField.myHackers);
                    }
                    else if (temp[2] == "AlgorithmField")
                    {
                        return BasicMethods.ExistField<Algorithm>(CreateFilter(temp[3]), Game.players[(Game.turns) % 2].myField.myAlgorithm);
                    }
                }
                if (temp[1] == "Adversary")
                {
                    if (temp[2] == "HackerField")
                    {
                        return BasicMethods.ExistField<Hacker>(CreateFilter(temp[3]), Game.players[(Game.turns + 1) % 2].myField.myHackers);
                    }
                    else if (temp[2] == "AlgorithmField")
                    {
                        return BasicMethods.ExistField<Algorithm>(CreateFilter(temp[3]), Game.players[(Game.turns + 1) % 2].myField.myAlgorithm);
                    }
                }
            }

            if (temp[0] == "ForAll")
            {
                if (temp[1] == "Own")
                {
                    if (temp[2] == "HackerField")
                    {
                        return BasicMethods.ForAllField<Hacker>(CreateFilter(temp[3]), Game.players[(Game.turns) % 2].myField.myHackers);
                    }
                    else if (temp[2] == "AlgorithmField")
                    {
                        return BasicMethods.ForAllField<Algorithm>(CreateFilter(temp[3]), Game.players[(Game.turns) % 2].myField.myAlgorithm);
                    }
                }
                if (temp[1] == "Adversary")
                {
                    if (temp[2] == "HackerField")
                    {
                        return BasicMethods.ForAllField<Hacker>(CreateFilter(temp[3]), Game.players[(Game.turns + 1) % 2].myField.myHackers);
                    }
                    else if (temp[2] == "AlgorithmField")
                    {
                        return BasicMethods.ForAllField<Algorithm>(CreateFilter(temp[3]), Game.players[(Game.turns + 1) % 2].myField.myAlgorithm);
                    }
                }
            }
            throw new SyntaxError();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>una funcion para filtrar las cartas</returns>
        public static Filter CreateFilter(string info)
        {
            if (info.Contains("EmptySpace"))
            {
                if (info.Contains("!="))
                {
                    return (Card card) => { return card != null; };
                }
                else if (info.Contains("="))
                {
                    return (Card card) => { return card == null; };
                }
            }
            string[] elements = info.Split(' ');
            if (elements[0].Contains("Resistance"))
            {
                switch (elements[1])
                {
                    case "=":
                        {
                            return (Card card) => { return (card as Algorithm).Resistance.life == Value(elements[2]); };
                        }
                    case "<":
                        {
                            return (Card card) => { return (card as Algorithm).Resistance.life < Value(elements[2]); };
                        }
                    case ">":
                        {
                            return (Card card) => { return (card as Algorithm).Resistance.life > Value(elements[2]); };
                        }
                }
            }
            if (elements[0].Contains("Capacity"))
            {
                switch (elements[1])
                {
                    case "=":
                        {
                            return (Card card) => { return (card as Hacker).Capacity == Value(elements[2]); };
                        }
                    case "<":
                        {
                            return (Card card) => { return (card as Hacker).Capacity < Value(elements[2]); };
                        }
                    case ">":
                        {
                            return (Card card) => { return (card as Hacker).Capacity > Value(elements[2]); };
                        }

                }
            }
            throw new SyntaxError();
        }
    }
}