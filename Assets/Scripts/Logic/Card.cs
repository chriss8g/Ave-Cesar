namespace BattleCards
{
    public abstract class Card
    {
        public string name { private set; get; }
        /// <summary>
        /// array con los elementos que componen la carta
        /// </summary>
        /// <value></value>
        protected string [] elements{set; get; }
        public string Description{private set; get;}
        public Card(string info)
        {
            this.elements = info.Split('@');
            this.name = elements[2];
            this.Description = elements[elements.Length -1];
        }
    }
    public class Hacker : Card
    {
        public int Capacity{set; get;}
        /// <summary>
        /// tiempo que estaran los Hackers en el campo
        /// </summary>
        public int Duration = 9;

        public Hacker(string info) : base(info)
        {
            this.Capacity = int.Parse(elements[3]);
        }

        /// <summary>
        /// forma en que atacan los hackers
        /// </summary>
        /// <param name="player">juagador al que pertenece el campo</param>
        /// <param name="i">indice de la carta</param>
        public void Attack(Player player, int i)
        {
            // si tiene un algoritmo al que atacar le resta resistencia y la diferencia se la quita a la vida
            if (player.myField.myAlgorithm[i] != null)
            {
                player.myField.myAlgorithm[i].Resistance.Damage(Capacity);
                
                if(player.myField.myAlgorithm[i].Resistance.life <= 0)
                {
                    player.myLife.Damage(-1*player.myField.myAlgorithm[i].Resistance.life);
                    player.myField.myAlgorithm[i] = null;
                }
            }
            // si no tiene algoritmo al que atacar le resta directamente a la vida
            else
            {
                player.myLife.Damage(Capacity);
            }
        }
    }
    public class Algorithm : Card
    {
        public Life Resistance = new Life();
        public Algorithm(string info) : base(info)
        {
            this.Resistance.Medicine(int.Parse(elements[3]));
        }
        
    }
}