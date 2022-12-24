namespace BattleCards
{
    public class Field
    {
        public Container<Hacker> myHackers;
        public Container<Algorithm> myAlgorithm;
        public Container<Effect> myEffects;
        public Field(int Length)
        {
            myAlgorithm = new Container<Algorithm>(Length);
            myHackers = new Container<Hacker>(Length);
            myEffects = new Container<Effect>(Length/2);
        }
    }

}