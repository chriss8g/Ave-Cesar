namespace BattleCards
{
    public static class Game
    {
        /// <summary>
        /// Cantidad de turnos transcurridos
        /// </summary>
        public static int turns = 0;
        /// <summary>
        /// es true si el jugador actual ya ha robado, false en caso contrario
        /// </summary>
        public static bool PlayerNotHasStolen;
        /// <summary>
        /// array con los jugadores
        /// </summary>
        public static Player[] players = new Player[2] { new Player(), new Player() };

        public static void NextTurn()
        {
            // aumenta la cantidad de turnos
            turns++;
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < players[i].myField.myHackers.Length(); j++)
                {
                    if (players[i].myField.myHackers[j] != null)
                    {
                        // disminuye la duracion de los hackers
                        players[i].myField.myHackers[j].Duration--;
                        // si es 0 los retira del campo
                        if (players[i].myField.myHackers[j].Duration <= 0)
                        {
                            players[i].myField.myHackers[j] = null;
                        }
                    }
                }
            }
        }
    }


}