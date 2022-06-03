using static CombatSystem.SelectorMode;

namespace CombatSystem
{
    public enum SelectorMode
    {
        Self,
        Adversary,
        Team,
        All
    }

    public static class SelectorModeExtensions
    {
        public static string GetTeamName(this SelectorMode self)
        {
            switch (self)
            {
                case Team:
                    return "Player";
                case Adversary:
                    return "Enemy";
                default:
                    return "Uspecified";
            }
        }
    }
}
