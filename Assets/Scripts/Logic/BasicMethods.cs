namespace BattleCards
{
    public static class BasicMethods
    {
        /// <summary>
        /// valida si existe un espacio en el campo que cumpla una condicion determinada
        /// </summary>
        /// <param name="f"></param>
        /// <param name="field"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ExistField<T>(Filter f, Container<T> field) where T : Card
        {
            for (int i = 0; i < field.Length(); i++)
            {
                if (f(field[i]))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// valida si todos los elementos del campo cumplen una condicion determinada
        /// </summary>
        /// <param name="f"></param>
        /// <param name="field"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ForAllField<T>(Filter f, Container<T> field) where T : Card
        {
            for (int i = 0; i < field.Length(); i++)
            {
                if (!f(field[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// baja la vida de un jugador
        /// </summary>
        /// <param name="n">cantidad en la cual se va a bajar la vida</param>
        /// <param name="who">indice del jugador cuya vida se vera afectada</param>
        public static void LowerLife(int n, int who)
        {
            Game.players[(Game.turns + who) % 2].myLife.Damage(n);
        }
        /// <summary>
        /// Aumenta la vida de un jugador
        /// </summary>
        /// <param name="n">cantidad en la cual se va a aumentar la vida</param>
        /// <param name="who">indice del jugador cuya vida se vera afectada</param>
        public static void UploadLife(int n, int who)
        {
            Game.players[(Game.turns + who) % 2].myLife.Medicine(n);
        }
        /// <summary>
        /// modifica la capacidad de todos los hackers del campo de un jugador
        /// </summary>
        /// <param name="n">cantidad en la que se modificara</param>
        /// <param name="who">indice del jugador</param>
        public static void ModifyCapacity(int n, int who)
        {
            for (int i = 0; i < Game.players[(Game.turns + who) % 2].myField.myHackers.Length(); i++)
            {
                if (Game.players[(Game.turns + who) % 2].myField.myHackers[i] != null)
                {
                    Game.players[(Game.turns + who) % 2].myField.myHackers[i].Capacity += n;
                }
            }
        }
        /// <summary>
        /// modifica la resistencia de todos los Algoritmos del campo de un jugador
        /// </summary>
        /// <param name="n">cantidad en la que se modificara</param>
        /// <param name="who">indice del jugador</param>
        public static void ModifyResistance(int n, int who)
        {
            for (int i = 0; i < Game.players[(Game.turns + who) % 2].myField.myAlgorithm.Length(); i++)
            {
                if (Game.players[(Game.turns + who) % 2].myField.myAlgorithm[i] != null)
                {
                    Game.players[(Game.turns + who) % 2].myField.myAlgorithm[i].Resistance.Medicine(n);
                }
            }
        }
        /// <summary>
        /// intercambia los campos de tipo T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Change<T>() where T : Card
        {
            if (typeof(T) == typeof(Hacker))
            {
                for (int i = 0; i < 4; i++)
                {
                    Hacker temp = new Hacker("@Hacker@Temp@-1");
                    temp = Game.players[0].myField.myHackers[i];
                    Game.players[0].myField.myHackers[i] = Game.players[1].myField.myHackers[i];
                    Game.players[1].myField.myHackers[i] = temp;
                }
                return;
            }

            if (typeof(T) == typeof(Algorithm))
            {
                for (int i = 0; i < 4; i++)
                {
                    Algorithm temp2 = new Algorithm("@Algorithm@Temp@-1");
                    temp2 = Game.players[0].myField.myAlgorithm[i];
                    Game.players[0].myField.myAlgorithm[i] = Game.players[1].myField.myAlgorithm[i];
                    Game.players[1].myField.myAlgorithm[i] = temp2;
                }

                return;
            }
        }
        /// <summary>
        /// mata todas las cartas de tipo T de un jugador
        /// </summary>
        /// <param name="who">indice del jugador</param>
        /// <typeparam name="T"></typeparam>
        public static void Kill<T>(int who) where T : Card
        {
            if (typeof(T) == typeof(Hacker))
            {
                for (int i = 0; i < Game.players[(Game.turns + who) % 2].myField.myHackers.Length(); i++)
                {
                    Game.players[(Game.turns + who) % 2].myField.myHackers[i] = null;
                }
                return;
            }
            if (typeof(T) == typeof(Algorithm))
            {
                for (int i = 0; i < Game.players[(Game.turns + who) % 2].myField.myAlgorithm.Length(); i++)
                {
                    Game.players[(Game.turns + who) % 2].myField.myAlgorithm[i] = null;
                }
                return;
            }
        }
        /// <summary>
        /// cambia una carta de campo
        /// </summary>
        /// <param name="item">carta a cambiar</param>
        /// <typeparam name="T"></typeparam>
        public static void ChangeField<T>(T item) where T : Card
        {
            if (item is Hacker)
            {
                for (int i = 0; i < Game.players[0].myField.myHackers.Length(); i++)
                {
                    if (Game.players[0].myField.myHackers[i] == item)
                    {
                        Game.players[0].myField.myHackers[i] = null;
                        for (int j = 0; j < Game.players[1].myField.myHackers.Length(); j++)
                        {
                            if (Game.players[1].myField.myHackers[j] == null)
                            {
                                Game.players[1].myField.myHackers[j] = (item as Hacker);
                                return;
                            }
                        }
                    }
                }
                for (int i = 0; i < Game.players[1].myField.myHackers.Length(); i++)
                {
                    if (Game.players[1].myField.myHackers[i] == item)
                    {
                        Game.players[1].myField.myHackers[i] = null;
                        for (int j = 0; j < Game.players[0].myField.myHackers.Length(); j++)
                        {
                            if (Game.players[0].myField.myHackers[j] == null)
                            {
                                Game.players[0].myField.myHackers[j] = (item as Hacker);
                                return;
                            }
                        }
                    }
                }
            }
            if (item is Algorithm)
            {
                for (int i = 0; i < Game.players[0].myField.myAlgorithm.Length(); i++)
                {
                    if (Game.players[0].myField.myAlgorithm[i] == item)
                    {
                        Game.players[0].myField.myAlgorithm[i] = null;
                        for (int j = 0; j < Game.players[1].myField.myAlgorithm.Length(); j++)
                        {
                            if (Game.players[1].myField.myAlgorithm[j] == null)
                            {
                                Game.players[1].myField.myAlgorithm[j] = (item as Algorithm);
                                return;
                            }
                        }
                    }
                }
                for (int i = 0; i < Game.players[1].myField.myAlgorithm.Length(); i++)
                {
                    if (Game.players[1].myField.myAlgorithm[i] == item)
                    {
                        Game.players[1].myField.myAlgorithm[i] = null;
                        for (int j = 0; j < Game.players[0].myField.myAlgorithm.Length(); j++)
                        {
                            if (Game.players[0].myField.myAlgorithm[j] == null)
                            {
                                Game.players[0].myField.myAlgorithm[j] = (item as Algorithm);
                                return;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// elimina una carta del campo
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void Kill<T>(T item) where T : Card
        {
            if (item is Hacker)
            {
                for (int i = 0; i < Game.players[0].myField.myHackers.Length(); i++)
                {
                    if (Game.players[0].myField.myHackers[i] == item)
                    {
                        Game.players[0].myField.myHackers[i] = null;
                        return;
                    }
                }
                for (int i = 0; i < Game.players[1].myField.myHackers.Length(); i++)
                {
                    if (Game.players[1].myField.myHackers[i] == item)
                    {
                        Game.players[1].myField.myHackers[i] = null;
                        return;
                    }
                }
            }
            if (item is Algorithm)
            {
                for (int i = 0; i < Game.players[0].myField.myAlgorithm.Length(); i++)
                {
                    if (Game.players[0].myField.myAlgorithm[i] == item)
                    {
                        Game.players[0].myField.myAlgorithm[i] = null;
                        return;
                    }
                }
                for (int i = 0; i < Game.players[1].myField.myAlgorithm.Length(); i++)
                {
                    if (Game.players[1].myField.myAlgorithm[i] == item)
                    {
                        Game.players[1].myField.myAlgorithm[i] = null;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// modifica el coeficiente de una carta
        /// </summary>
        /// <param name="item">cantidad en la que se modificara</param>
        /// <param name="n"></param>
        /// <typeparam name="T"></typeparam>
        public static void ModifyCoefficient<T>(T item, int n) where T : Card
        {
            if (item is Hacker)
            {
                for (int i = 0; i < Game.players[0].myField.myHackers.Length(); i++)
                {
                    if (Game.players[0].myField.myHackers[i] == item)
                    {
                        Game.players[0].myField.myHackers[i].Capacity += n;
                        return;
                    }
                }
                for (int i = 0; i < Game.players[1].myField.myHackers.Length(); i++)
                {
                    if (Game.players[1].myField.myHackers[i] == item)
                    {
                        Game.players[1].myField.myHackers[i].Capacity += n;
                        return;
                    }
                }
            }
            if (item is Algorithm)
            {
                for (int i = 0; i < Game.players[0].myField.myAlgorithm.Length(); i++)
                {
                    if (Game.players[0].myField.myAlgorithm[i] == item)
                    {
                        Game.players[0].myField.myAlgorithm[i].Resistance.Medicine(n);
                    }
                }
                for (int i = 0; i < Game.players[1].myField.myAlgorithm.Length(); i++)
                {
                    if (Game.players[1].myField.myAlgorithm[i] == item)
                    {
                        Game.players[1].myField.myAlgorithm[i].Resistance.Medicine(n);
                    }
                }
            }
        }
    }
}