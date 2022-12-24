namespace BattleCards
{
    public abstract class Effect : Card
    {
        public CompoundExpression Condition{get; private set;}
        protected string Action;

        public Effect (string info) : base(info)
        {
            this.Condition = new CompoundExpression(elements[3]);
            this.Action = elements[4];
        }
    }
    public class EffectOverCard <T>: Effect where T : Card
    {
        public T Victim;
        public ActionOver<T> Function;
        public EffectOverCard(string info) : base(info)
        {
            Function += Analyzer.Create <T>(Action);
        }
    }
    public class EffectOverField : Effect
    {
        public Function Function {get; set;} 
        public EffectOverField(string info) : base(info)
        {
            Function += Analyzer.Create(Action);
        }
        
    }
}
