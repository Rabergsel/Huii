using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Huii.Objects.Planes
{
    public class XZPlane : ObjectBase
    {

        float MinX { get; set; } = 0f;
        float MaxX { get; set; } = 0f;
        float MinZ { get; set; } = 0f;
        float MaxZ { get; set; } = 0f;
        float Y { get; set; } = 0f;

        public XZPlane(float minX, float maxX, float minZ, float maxZ, float y)
        {
            MinX = minX;
            MaxX = maxX;
            MinZ = minZ;
            MaxZ = maxZ;
            Y = y;
        }

        internal Vector3D GetIntersectionPoint(Particle p, out float TimeToIntersection)
        {
            var annealingSpeed = p.Direction.Y * p.Speed;
            var timeToIntersect = (Y - p.Position.Y) / annealingSpeed;

            TimeToIntersection = timeToIntersect;

            if (timeToIntersect < 0) return null;

            var IntersectX = p.Position.X + (p.Speed * p.Direction.X * timeToIntersect);
            var IntersectZ = p.Position.Z + (p.Speed * p.Direction.Z * timeToIntersect);

            return new Vector3D(IntersectX, Y, IntersectZ);
        }

        public override bool CheckForCollision(Particle p, float DeltaTime)
        {
            //Does Y cross plane?
            var Intersection = GetIntersectionPoint(p, out float TimeToIntersection);

            if(TimeToIntersection > DeltaTime) return false;
            if (Intersection == null) return false;

            if (Intersection.X > MaxX) return false;
            if (Intersection.Z > MaxZ) return false;

            if (Intersection.X < MinX) return false;
            if (Intersection.Z < MinZ) return false;

            return true;

        }

        public override Particle Reflect(Particle p, float DeltaTime)
        {

            var intersection = GetIntersectionPoint(p, out float TimeToIntersection);

            //Set Position
            p.Position = intersection;

            //Manipulate Direction
            p.Direction = new Vector3D(p.Direction.X, -p.Direction.Y, p.Direction.Z);

            //Drive Rest of Time
            p.UpdateWithDelta(DeltaTime - TimeToIntersection);

            return p;
        }

    }
}
