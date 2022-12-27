class ParticleFactory
{
    public static void UpdateParticles(List<Particle> Particles, int shiftx = 0, int shifty = 0)
    {
        foreach (Particle particle in Particles)
        {
            particle.update(shiftx, shifty);
        }
    }

    public static List<Particle> CreateParticles<T>(int amount, bool affectedByCamera) where T : Particle, new()
    {
        List<Particle> particles = new List<Particle>();

        for (int i = 0; i < amount; i++)
        {
            T instance = new T();
            instance.affectedByCamera = affectedByCamera;
            particles.Add(instance);
        }

        return particles;
    }
}