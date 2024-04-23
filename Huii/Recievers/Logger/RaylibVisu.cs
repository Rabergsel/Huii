using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;
using System.Numerics;
using System.Diagnostics;

namespace Huii.Recievers.Logger
{
    public class RaylibVisu : RecieverBase
    {
        public RaylibVisu() 
        {
            Raylib.InitWindow(800, 480, "Simulation");
            Raylib.SetTargetFPS(60);
            Thread.Sleep(500);
        }

        public override float GetMeasurement(float Time, Simulation sim)
        {
            var sw = Stopwatch.StartNew();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);
            
            foreach(var P in sim.Particles)
            {
                Raylib.DrawPoint3D(new Vector3(P.Position.X, P.Position.Y, P.Position.Z), Raylib.RED);
            }


            Raylib.EndDrawing();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
