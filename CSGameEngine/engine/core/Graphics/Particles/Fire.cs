using Raylib_cs;
using System.Numerics;

class Fire : IUpdateable
{
    public Vector2 Position;

    public int width, height;

    public bool affectedByCamera;

    public List<FireParticle> FireParticles = new List<FireParticle>();

    public static int density = 1;

    public Fire(Vector2 position, int width, int height, bool affectedByCamera)
    {
        this.Position = position;

        this.width = width;
        this.height = height;

        this.affectedByCamera = affectedByCamera;
    }

    public void LoadParticles()
    {
        Random rand = new Random();
        for (int i = 0; i < width; i++)
        {
            for (int x = 0; x < density; x += rand.Next(1, 3))
            {
                CreateParticle(i);
            }
        }
    }

    public void CreateParticle(float x)
    {
        FireParticle particle = new FireParticle();
        particle.Position = new Vector2(x, new Random().Next((int)(Position.Y + height / 2), (int)Position.Y + height));
        particle.affectedByCamera = affectedByCamera;
        particle.EndHeight = new Random().Next((int)Position.Y, (int)(Position.Y + height * 0.8));
        particle.Size = (int)(width * 0.0425);
        FireParticles.Add(particle);
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        foreach (FireParticle particle in FireParticles.ToList())
        {
            particle.Position -= new Vector2(0, (float)particle.Speed * Time.deltaTime);
            particle.update(shiftx, shifty);

            if (particle.Position.Y <= particle.EndHeight)
            {
                particle.isDead = true;
                CreateParticle(particle.Position.X);
            }

            if (particle.isDead)
            {
                FireParticles.Remove(particle);
            }
        }
    }
}