namespace BattleCards
{
    public class Player
    {
        public Life myLife;
        public Hand myHand;
        public Field myField;
        public static int Played = 0;
        
        public Player()
        {
            myLife = new Life(100);
            myHand = new Hand(5);
            myField = new Field(4);
        }

    }

}