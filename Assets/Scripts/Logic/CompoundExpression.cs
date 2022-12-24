using System.Collections.Generic;
namespace BattleCards
{
    public class CompoundExpression :IAssessable
    {
        public string s {get;set;}
        private string Operator;
        private string LeftOperand;
        private string RigthOperand;

        public CompoundExpression (string s)
        {
            this.s = s;
            string [] temp = s.Split(' ');
            //si contiene algun operador booleano se llenan las propiedades, sino esto no tiene sentido
            if (Utils.ContainsAny(temp, Utils.BooleanOperators))
            {
                int position = Utils.SelectPosition(temp);
                if (position == 0)
                {
                    if (Utils.HaveParentesis(this.s))
                    {
                        string _new = s.Substring(2, s.Length - 5);
                        temp = _new.Split(' ');
                        position = Utils.SelectPosition(temp);
                    }
                    
                }
                Operator = temp[position];
                RigthOperand = Utils.ToStringRigth(temp,position);
                LeftOperand = Utils.ToStringLeft(temp,position);
            }
            else 
            {
                Operator = " ";
                RigthOperand = " ";
                LeftOperand = " ";
            }

        }
        public bool Evaluate()
        {
            // si no se encontro ningun operador entonces es una expresion simple
            if (Operator == " ")
            {
                SimpleExpression simple = new SimpleExpression(this.s);
                return simple.Evaluate();
            }
            
            // si contiene un operador tanto el operando derecho como el izquierdo con expresiones
            CompoundExpression opp1 = new CompoundExpression(LeftOperand);
            CompoundExpression opp2 = new CompoundExpression(RigthOperand);
            if (Operator == "and")
            {
                return opp1.Evaluate() && opp2.Evaluate();
            }

            if (Operator == "or")
            {
                return opp1.Evaluate() || opp2.Evaluate();
            }

            // si no es un operador lanza una excepcion
            throw new SyntaxError();
        }
    }
}