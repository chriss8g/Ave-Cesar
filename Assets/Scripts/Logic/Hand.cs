namespace BattleCards
{
    public class Hand
    {
        public Container<Card> Cards;
        public Hand(int n)
        {
            Cards = new Container<Card>(n);
        }
        /// <summary>
        /// roba cartas hasta llenar la mano
        /// </summary>
        public void Chief()
        {
            for (int i = 0; i < Cards.Length(); i++)
            {
                if(Cards[i] == null)
                {
                    Cards[i] = DataBase.GetCard();
                }
            }
        }
    }
}