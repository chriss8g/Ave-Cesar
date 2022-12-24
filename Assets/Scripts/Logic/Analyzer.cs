using System.Collections;
using System.Collections.Generic;
namespace BattleCards
{
    public static class Analyzer
    {
        /// <summary>
        /// procesa una cadena de texto para convertirlo en un Function
        /// </summary>
        /// <param name="Action">accion como string para interpretar</param>
        /// <returns>un Function para asignarlo a una carta EffectOverField</returns>
        public static Function Create(string Action)
        {
            // inicia una funcion vacia
            Function Function = Utils.Empty;

            // separa por ; para almacenar las acciones atomicas
            string[] elements = Action.Split(';');
            foreach (string item in elements)
            {
                // separa por _ para obtener los componentes de la funcion
                string[] components = item.Split('_');
                if (!(Utils.ContainsAny(components,Utils.ActionsOverField)))
                {
                    throw new SyntaxError();
                }
                if (components[0] == "LowerLife")
                // identifica la funcion LowerLife y busca sus parametros
                {
                    if (!((components[1] == "Own") || (components[1] == "Adversary")))
                    {
                        throw new SyntaxError();
                    }
                    int n = Reader.Value(components[2]);
                    void function()
                    {
                        BasicMethods.LowerLife(n , components[1] == "Own" ? 0 : 1);
                    }
                    
                    Function += function;
                    //adiciona la funcion al delegado
                    continue;
                }
                if (components[0] == "UploadLife")
                {
                    if (!((components[1] == "Own") || (components[1] == "Adversary")))
                    {
                        throw new SyntaxError();
                    }
                    int n = Reader.Value(components[2]);
                    void function()
                    {
                        BasicMethods.UploadLife( n , components[1] == "Own" ? 0 : 1);
                    }
                    Function += function;
                    continue;
                }
                if (components[0] == "Change")
                {
                    if (components[1] == "HackerField")
                    {
                        Function += BasicMethods.Change<Hacker>;
                    }
                    else if (components[1] == "AlgorithmField")
                    {
                        Function += BasicMethods.Change<Algorithm>;
                    }
                    else if (components[1] == "AllFileds")
                    {
                        Function += BasicMethods.Change<Card>;
                    }
                    else 
                    {
                        throw new SyntaxError();
                    }
                    continue;
                }
                if (components[0] == "Capacity")
                {
                    if (!((components[1] == "Own") || (components[1] == "Adversary")))
                    {
                        throw new SyntaxError();
                    }
                    int n = Reader.Value(components[2]);
                    void function()
                    {
                        BasicMethods.ModifyCapacity( n , components[1] == "Own" ? 0 : 1);
                    }
                    Function += function;
                    continue;
                }
                if (components[0] == "Resistance")
                {
                    if (!((components[1] == "Own") || (components[1] == "Adversary")))
                    {
                        throw new SyntaxError();
                    }
                    int n = Reader.Value(components[2]);
                    void function()
                    {
                        BasicMethods.ModifyResistance( n , components[1] == "Own" ? 0 : 1);
                    }
                    Function += function;
                    continue;
                }
                if (components[0] == "Sweep")
                {
                    if (!((components[1] == "Hackers") || (components[1] == "Algorithms") || (components[1] == "All")))
                    {
                        throw new SyntaxError();
                    }
                    if (!((components[2] == "Own") || (components[2] == "Adversary")))
                    {
                        throw new SyntaxError();
                    }
                    if (components[1] == "Hackers")
                    {
                        void function()
                        {
                            BasicMethods.Kill<Hacker>(components[2] == "Own" ? 0 : 1);
                        }
                        Function += function;
                    }
                    if (components[1] == "Algorithms")
                    {
                        void function()
                        {
                            BasicMethods.Kill<Algorithm>(components[2] == "Own" ? 0 : 1);
                        }
                        Function += function;
                    }
                    if (components[1] == "All")
                    {
                        void function()
                        {
                            BasicMethods.Kill<Card>(components[2] == "Own" ? 0 : 1);
                        }
                        Function += function;
                    }
                    continue;
                }

            }
            // devuelve la funcion con todas las acciones adicionadas
            return Function;
        }
        /// <summary>
        /// procesa una cadena de texto para convertirlo en un ActionOver
        /// </summary>
        /// <param name="Action">string a procesar</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>un ActionOver para asignarlo a una carta EffectOverCard</returns>
        public static ActionOver<T> Create<T>(string Action) where T : Card
        //similar al metodo anterior
        {
            ActionOver<T> Function = Utils.Empty2;
            string[] elements = Action.Split(';');
            foreach (string item in elements)
            {
                string[] components = item.Split('_');
                if (components[0] != "Victim")
                {
                    throw new SyntaxError();
                }
                if (!(Utils.ContainsAny(components,Utils.ActionsOverCard)))
                {
                    throw new SyntaxError();
                }
                if (components[1] == "ChangeField")
                {
                    void function<T>(T Victim) where T : Card
                    {
                        BasicMethods.ChangeField<T>(Victim);
                    }
                    Function += function;
                    continue;
                }
                if (components[1] == "Kill")
                {
                    void function<T>(T Victim) where T : Card
                    {
                        BasicMethods.Kill<T>(Victim);
                    }
                    Function += function;
                    continue;
                }
                if (components[1] == "LowerCoefficient")
                {
                    int n = Reader.Value(components[2]);
                    void function<T>(T Victim) where T : Card
                    {
                        BasicMethods.ModifyCoefficient<T>(Victim, - n);
                    }
                    Function += function;
                    continue;
                }
                if (components[1] == "UploadCoefficient")
                {
                    int n = Reader.Value(components[2]);
                    void function<T>(T Victim) where T : Card
                    {
                        BasicMethods.ModifyCoefficient<T>(Victim, n );
                    }
                    Function += function;
                    continue;
                }
            }
            return Function;
        }
        
    }
}