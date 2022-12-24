using System.IO;
using System;

namespace BattleCards
{
    public class DataBase
    {
        private static string[] Files = Directory.GetFiles("./Assets/Cards");
        /// <summary>
        /// carga una carta aleatoria de la carpeta Cards
        /// </summary>
        /// <returns></returns>
        public static Card GetCard()
        {
            System.Random rnd = new System.Random();
            
            string info;
            do{
                info = File.ReadAllText(Files[rnd.Next(Files.Length)]);
            }while(info[0] != '@');
            
            if (info.StartsWith("@Hacker"))
            {
                return new Hacker(info);
            }
            else if(info.StartsWith("@Algorithm"))
            {
                return new Algorithm (info);
            }
            else if(info.StartsWith("@EffectOverField"))
            {
                return new EffectOverField (info);
            }
            else if(info.StartsWith("@EffectOverCard"))
            {
                return new EffectOverCard<Card> (info);
            }
            else
            {
                return null;
            }
        }
    }
}