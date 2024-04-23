using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Huii.Objects.Planes
{
    public class XYPlane : ObjectBase
    {

        float MinX { get; set; } = 0f;
        float MaxX { get; set; } = 0f;
        float MinY { get; set; } = 0f;
        float MaxY { get; set; } = 0f;
        float Z { get; set; } = 0f;

        public XYPlane(float minX, float maxX, float minY, float maxY, float z)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            Z = z;
        }

        internal Vector3D GetIntersectionPoint(Particle p, out float TimeToIntersection)
        {
            var annealingSpeed = p.Direction.Z * p.Speed;
            var timeToIntersect = (Z - p.Position.Z) / annealingSpeed;

            TimeToIntersection = timeToIntersect;

            if (timeToIntersect < 0) return null;

            var IntersectX = p.Position.X + (p.Speed * p.Direction.X * timeToIntersect);
            var IntersectY = p.Position.Y + (p.Speed * p.Direction.Y * timeToIntersect);

            return new Vector3D(IntersectX, IntersectY, Z);
        }

        public override bool CheckForCollision(Particle p, float DeltaTime)
        {
            //Does Y cross plane?
            var Intersection = GetIntersectionPoint(p, out float TimeToIntersection);

            if(TimeToIntersection > DeltaTime) return false;
            if (Intersection == null) return false;

            if (Intersection.X > MaxX) return false;
            if (Intersection.Y > MaxY) return false;

            if (Intersection.X < MinX) return false;
            if (Intersection.Y < MinY) return false;

            return true;

        }

        public override Particle Reflect(Particle p, float DeltaTime)
        {

            var intersection = GetIntersectionPoint(p, out float TimeToIntersection);

            //Set Position
            p.Position = intersection;

            //Manipulate Direction
            p.Direction = new Vector3D(p.Direction.X, p.Direction.Y, -p.Direction.Z);

            //Drive Rest of Time
            p.UpdateWithDelta(DeltaTime - TimeToIntersection);

            return p;
        }

    }
}
