namespace Huii
{
    public abstract class ObjectBase
    {
        public abstract bool CheckForCollision(Particle p, float DeltaTime);

        public abstract Particle Reflect(Particle p, float DeltaTime);

    }
}
