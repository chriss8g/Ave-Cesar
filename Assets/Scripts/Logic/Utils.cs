using System;
using System.Collections.Generic;
namespace BattleCards
{
    public static class Utils
    {
        // listas de palabras reservadas del lenguaje
        public static Dictionary<string, bool> Literal = new Dictionary<string, bool>
        {
            {"True",true}, 
            {"False", false},
            {"true",true}, 
            {"false", false}
        };
        public static List<string> Comparators = new List<string>() {"<",">","="};
        public static List<string> Reservedint = new List<string>() {"Own", "Adversary", "Life"};
        public static List<string> Reservedbool = new List<string>() {"Exist", "ForAll"};
        public static List<string> BooleanOperators = new List<string>() {"and" , "or"};
        public static List<string> ActionsOverField = new List<string> (){"LowerLife", "UploadLife", "Change", "Capacity", "Resistance", "Sweep"};
        public static List<string> ActionsOverCard = new List<string> (){"ChangeField", "Kill", "LowerCoefficient", "UploadCoefficient"};
        /// <summary>
        /// funcion vacia
        /// </summary>
        public static void Empty()
        {
            
        }
        /// <summary>
        /// funcion vacia
        /// </summary>
        /// <param name="Victim"></param>
        /// <typeparam name="T"></typeparam>
        public static void Empty2<T>(T Victim) where T : Card
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="lista"></param>
        /// <returns>true si algun elemento del array esta contenido en la lista
        /// false en caso contrario</returns>
        public static bool ContainsAny (string [] s, List<string> lista)
        {
            foreach (string item in s)
            {
                if (lista.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="lista"></param>
        /// <returns>true si algun elemento del string esta contenido en la lista
        /// false en caso contrario</returns>
        public static bool ContainsAny (string s, List<string> lista)
        {
            foreach (string item in lista)
            {
                if (s.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="index"></param>
        /// <returns>una subcadena de la cadena original, desde el inicio hasta el index</returns>
        public static string ToStringLeft(string[] parts, int index)
        {
            string a = "";
            for (int i = 0; i < index; i++)
            {
                a += parts[i] + " ";
            }
            return a;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="index"></param>
        /// <returns>una subcadena de la cadena original, desde el index hasta el final</returns>
        public static string ToStringRigth(string[] parts, int index)
        {
            string a = "";
            for (int i = index + 1; i < parts.Length; i++)
            {
                a += parts[i] + " ";
            }
            return a;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>la posicion del operador booleano mas a la derecha 
        /// tal que la cantidad de parentesis despues de el es balanceada</returns>
        public static int SelectPosition (string [] s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (BooleanOperators.Contains(s[i]) && IsBalanced(s,i))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="a"></param>
        /// <returns>true si la cantidad de parentesis despues de la posicion a es balanceada
        /// false en caso contrario</returns>
        private static bool IsBalanced (string [] s, int a)
        {
            int cantop = 0;
            int cantclos = 0;
            for (int i = a + 1; i < s.Length; i++)
            {
                if (s[i] == "(")
                {
                    //cuenta la cantidad de parentesis abiertos
                    cantop++;
                }
                if (s[i] == ")")
                {
                    //cuenta la cantidad de parentesis cerrado
                    cantclos++;
                }
            }
            return cantop == cantclos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true si la expresion esta entre parentesis
        /// false en caso contrario</returns>
        public static bool HaveParentesis(string s)
        {
            if (s.Contains("(") || s.Contains(")"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="words"></param>
        /// <param name="lista"></param>
        /// <returns>true si todas las palabras estan contenidas en la lista
        /// false en caso contrario</returns>
        public static bool AllWordAreReserved(string [] words, List<string> lista)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (!lista.Contains(words[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}