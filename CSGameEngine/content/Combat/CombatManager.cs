class CombatManager
{
    public static void DamagePlayer(PlayerObject mp, float amount)
    {
        if (((MultiplayerServer)LevelManager.ActiveLevel).player.statManager.Health > 0)
        {
            mp.health = Math.Max(mp.health - amount, 0);
        }
    }
}