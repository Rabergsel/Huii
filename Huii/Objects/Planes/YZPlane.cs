using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Huii.Objects.Planes
{
    public class YZPlane : ObjectBase
    {

        float MinY { get; set; } = 0f;
        float MaxY { get; set; } = 0f;
        float MinZ { get; set; } = 0f;
        float MaxZ { get; set; } = 0f;
        float X { get; set; } = 0f;

        public YZPlane(float minY, float maxY, float minZ, float maxZ, float x)
        {
            MinY = minY;
            MaxY = maxY;
            MinZ = minZ;
            MaxZ = maxZ;
            X = x;
        }

        internal Vector3D GetIntersectionPoint(Particle p, out float TimeToIntersection)
        {
            var annealingSpeed = p.Direction.X * p.Speed;
            Console.WriteLine("Annealing Speed: " + annealingSpeed);
            var timeToIntersect = (X - p.Position.X) / annealingSpeed;
            Console.WriteLine("Delta: " + (X - p.Position.X));
            Console.WriteLine("TtI: " + timeToIntersect);

            TimeToIntersection = timeToIntersect;

            if (timeToIntersect < 0) return null;

            var IntersectY = p.Position.Y + (p.Speed * p.Direction.Y * timeToIntersect);
            var IntersectZ = p.Position.Z + (p.Speed * p.Direction.Z * timeToIntersect);

            return new Vector3D(X, IntersectY, IntersectZ);
        }

        public override bool CheckForCollision(Particle p, float DeltaTime)
        {
            //Does X cross plane?
            var Intersection = GetIntersectionPoint(p, out float TimeToIntersection);

            
            
            if (Intersection == null) return false;
            Console.WriteLine($"Int = ({Intersection.X}|{Intersection.Y}|{Intersection.Z}), TtI = {TimeToIntersection}");
            if (TimeToIntersection > DeltaTime) return false;
            

            if (Intersection.Y > MaxY) return false;
            if (Intersection.Z > MaxZ) return false;

            if (Intersection.Y < MinY) return false;
            if (Intersection.Z < MinZ) return false;

            return true;

        }

        public override Particle Reflect(Particle p, float DeltaTime)
        {

            var intersection = GetIntersectionPoint(p, out float TimeToIntersection);

            //Set Position
            p.Position = intersection;

            //Manipulate Direction
            p.Direction = new Vector3D(-p.Direction.X, p.Direction.Y, p.Direction.Z);

            //Drive Rest of Time
            p.UpdateWithDelta(DeltaTime - TimeToIntersection);

            return p;
        }

    }
}
