using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huii.Recievers.Logger
{
    public class ConsoleLogger : RecieverBase
    {
        public override float GetMeasurement(float Time, Simulation sim)
        {
            Console.WriteLine("Time = " + Time + ", #Particles: " + sim.Particles.Count);

            foreach(var p in sim.Particles)
            {

                Console.WriteLine($"Pos = ({p.Position.X}|{p.Position.Y}|{p.Position.Z}), Dir = ({p.Direction.X}|{p.Direction.Y}|{p.Direction.Z}), Speed = {p.Speed}, Amp = {p.Amplitude}");

            }
            Console.WriteLine("");
            return sim.Particles.Count;
        }

    }
}
