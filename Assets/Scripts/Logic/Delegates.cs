namespace BattleCards
{
    public delegate bool Filter (Card card);
    public delegate void Function();
    public delegate void ActionOver<T>(T Victim) where T : Card;
}