namespace BattleCards
{
    interface IAssessable
    {
        string s {get; set;}
        /// <summary>
        /// se evalua la expresion
        /// </summary>
        /// <returns></returns>
        bool Evaluate();
    }
}