namespace Huii
{
    public class Vector3D
    {
        public Vector3D() { }
        public Vector3D(float x, float y, float z) { X = x; Y = y; Z = z; }

        public float X { get; set; } = 0f;
        public float Y { get; set; } = 0f;
        public float Z { get; set; } = 0f;

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            }

        }

        public Vector3D GetNormalized()
        {
            return this / Length;
        }


        public static Vector3D operator +(Vector3D a, Vector3D b) { return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Vector3D operator +(Vector3D a, float b) { return new Vector3D(a.X + b, a.Y + b, a.Z + b); }

        public static Vector3D operator -(Vector3D a, Vector3D b) { return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static Vector3D operator -(Vector3D a, float b) { return new Vector3D(a.X - b, a.Y - b, a.Z - b); }

        public static Vector3D operator *(Vector3D a, Vector3D b) { return new Vector3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z); }
        public static Vector3D operator *(Vector3D a, float b) { return new Vector3D(a.X * b, a.Y * b, a.Z * b); }

        public static Vector3D operator /(Vector3D a, Vector3D b) { return new Vector3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }
        public static Vector3D operator /(Vector3D a, float b) { return new Vector3D(a.X / b, a.Y / b, a.Z / b); }




    }
}
