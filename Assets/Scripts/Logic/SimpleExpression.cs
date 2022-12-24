namespace BattleCards
{
    public class SimpleExpression : IAssessable
    {
        public string s{get;set;}
        public SimpleExpression(string s)
        {
            this.s = s;
        }

        /// <summary>
        /// una expresion simple puede ser un literal, una comparacion o un predicado 
        /// </summary>
        /// <returns></returns>
        public bool Evaluate ()
        {
            //literal
            foreach (string item in Utils.Literal.Keys)
            {
                if (s.StartsWith(item))
                {
                    return Utils.Literal[item];
                }
            }
            //predicado
            foreach (string word in Utils.Reservedbool)
            {
                if (s.StartsWith(word))
                {
                    return Reader.ReadBool(this.s);
                }
            }
            //comparacion
            foreach (string item in Utils.Comparators)
            {
                if (Utils.ContainsAny(this.s, Utils.Comparators))
                {
                    Comparation comp = new Comparation(this.s);
                    return comp.Evaluate();
                }
            }
            //lanza una excepcion
            throw new SyntaxError();
        } 
    }
}