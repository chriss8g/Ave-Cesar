namespace BattleCards
{
    [System.Serializable]
    public class SyntaxError : System.Exception
    {
        public SyntaxError() { }
        public SyntaxError(string message) : base(message) { }
        public SyntaxError(string message, System.Exception inner) : base(message, inner) { }
        protected SyntaxError(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}