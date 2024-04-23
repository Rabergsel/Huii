namespace Huii
{
    public class Particle
    {
        public float Amplitude { get; set; } = 0f;
        public float Speed { get; set; } = 1f;
        public Vector3D Position { get; set; } = new Vector3D();
        public Vector3D Direction { get; set; } = new Vector3D();

        public Vector3D GetDelta(float DeltaTime)
        {
            return Direction * DeltaTime * Speed;
        }

        public void UpdateWithDelta(float DeltaTime)
        {
            Position = Position + GetDelta(DeltaTime);
        }

    }
}
