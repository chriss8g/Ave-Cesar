namespace BattleCards
{
    public class Life
    {
        /// <summary>
        /// Vida. su valor inicial es 50 puntos
        /// </summary>
        /// <value></value>
        public int life { private set; get; }
        /// <summary>
        /// Disminuye la vida en un valor dado
        /// </summary>
        /// <param name="damage">Dagno que se infligira</param>
        public void Damage(int damage)
        {
            life -= damage;
        }
        /// <summary>
        /// Aumenta la vida en un valor dado
        /// </summary>
        /// <param name="medicine">Puntos de vida que aumentara</param>
        public void Medicine(int medicine)
        {
            life += medicine;
        }

        public Life(int i)
        {
            life = i;
        }
        public Life()
        {
            life = 0;
        }

    }

}