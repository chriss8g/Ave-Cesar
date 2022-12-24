using System.Collections.Generic;
using System.IO;
using System;
namespace BattleCards
{
    public class Comparation : IAssessable
    {
        public string s {get;set;}
        private string Operator;
        private int LeftOperand;
        private int RigthOperand;
        public Comparation (string s)
        {
            this.s = s;
            string [] elements = s.Split(' ');
            this.Operator = elements[1];
            this.LeftOperand = Reader.Value(elements[0]);
            this.RigthOperand = Reader.Value(elements[2]);
        }

        public bool Evaluate()
        {
            if (this.Operator == "<")
            {
                return LeftOperand < RigthOperand;
            }

            if (this.Operator == ">")
            {
                return LeftOperand > RigthOperand;
            }

            if (this.Operator == "=")
            {
                return LeftOperand == RigthOperand;
            }
            
            // si el operador no es ninguno de los antes expuestos lanza una excepcion
            throw new SyntaxError();
        }

    }
}