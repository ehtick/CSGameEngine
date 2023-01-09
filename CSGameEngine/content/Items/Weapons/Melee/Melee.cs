using System.Numerics;
using Raylib_cs;

class Melee : Weapon
{
    public float attackRange;
    public Raeuleaux attackRangeVisual;

    public float damage;

    public float baseRotationAngle = 45.0f;
    public float rotationPercentage = 0.0f;


    public Melee(string name, string imagePath, float damage, ItemRarity? rarity = null) : base(name, imagePath, rarity)
    {
        this.damage = damage;
    }

    public override void updateIngame(Player player)
    {
        rotationPercentage += 0.001f * Time.deltaTime;

        if (rotationPercentage >= 100)
        {
            rotationPercentage -= 100;
        }

        if (!(attackRangeVisual is Raeuleaux))
        {
            attackRangeVisual = new Raeuleaux(player.rect.Center, Configuration.config.attackRangeRadius, Configuration.config.attackRangeBaseLength, new Vector2(0, 0));
        }

        Vector2 mousepos = Raylib.GetMousePosition();

        attackRangeVisual.targetPoint = mousepos;
        // attackRangeVisual.update();

        Vector2 positionAroundCircle = ShapeMath.GetPositionAroundCircle(image.LoadedTexture.width, 400, 400, rotationPercentage);
        Vector2 imagepos = positionAroundCircle - new Vector2(image.LoadedTexture.width / 2, image.LoadedTexture.height / 2);
        Vector2 direction = positionAroundCircle - new Vector2(400, 400);
        float rotationAngle = ShapeMath.GetRotation(rotationPercentage, 400, 400, 100, (int)(400 + direction.X * 100), (int)(400 + direction.Y * 100));

        Rlgl.rlPushMatrix();
        Rlgl.rlTranslatef(imagepos.X + image.LoadedTexture.width / 2, imagepos.Y + image.LoadedTexture.height / 2, 0);
        Rlgl.rlRotatef(rotationAngle + baseRotationAngle, 0, 0, 1);
        Rlgl.rlTranslatef(-(imagepos.X + image.LoadedTexture.width / 2), -(imagepos.Y + image.LoadedTexture.height / 2), 0);

        Raylib.DrawTexture(image.LoadedTexture, (int)imagepos.X, (int)imagepos.Y, Color.WHITE);

        Rlgl.rlPopMatrix();

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