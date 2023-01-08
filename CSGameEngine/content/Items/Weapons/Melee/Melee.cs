using System.Numerics;
using Raylib_cs;

class Melee : Weapon
{
    public float attackRange;
    public Raeuleaux attackRangeVisual;

    public float damage;

    public Melee(string name, string imagePath, float damage, ItemRarity? rarity = null) : base(name, imagePath, rarity)
    {
        this.damage = damage;
    }

    public override void updateIngame(Player player)
    {
        if (!(attackRangeVisual is Raeuleaux))
        {
            attackRangeVisual = new Raeuleaux(player.rect.Center, Configuration.config.attackRangeRadius, Configuration.config.attackRangeBaseLength, new Vector2(0, 0));
        }

        Vector2 mousepos = Raylib.GetMousePosition();

        attackRangeVisual.targetPoint = mousepos;
        attackRangeVisual.update();

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            if (LevelManager.ActiveLevel is MultiplayerServer)
            {
                Random random = new Random();

                foreach (KeyValuePair<string, PlayerObject> po in ((MultiplayerServer)LevelManager.ActiveLevel).Players)
                {
                    if (po.Key != ((MultiplayerServer)LevelManager.ActiveLevel).playerUsername && po.Value.health > 0)
                    {
                        PlayerObject mp = po.Value;
                        if (CollisionMath.CircleIntersectsTriangle(new Vector2(mp.x, mp.y) + new Vector2(25, 25) - ((MultiplayerServer)LevelManager.ActiveLevel).camera.position, 25, attackRangeVisual.tri.p1, attackRangeVisual.tri.p2, attackRangeVisual.tri.p3))
                        {
                            int chance = random.Next(100);

                            CombatManager.DamagePlayer(mp, (damage + player.statManager.PhysicalAttackDamage) * (chance <= player.statManager.CriticalChance ? player.statManager.CriticalMultiplier : 1));

                            Console.WriteLine("HEALTH: " + mp.health);
                        }

                        ((MultiplayerServer)LevelManager.ActiveLevel).Players[po.Key] = mp;
                    }
                }
            }
        }
    }
}