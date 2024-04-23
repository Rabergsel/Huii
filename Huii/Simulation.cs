using Raylib_CsLo;
using System.Net.Http.Headers;

namespace Huii
{
    public class Simulation
    {

        public float Time { get; set; } = 0f;
        public long Timestep { get; set; } = 0;
        private float TimestepSize { get; set; } = 0.1f;

        private float MaxTime { get; set; } = 10f;

        public List<Particle> Particles { get; set; } = new List<Particle>();
        public List<EmitterBase> Emitters { get; set; } = new List<EmitterBase>();
        public List<RecieverBase> Reciever { get; set; } = new List<RecieverBase>();

        public List<ObjectBase> Objects { get; set; } = new List<ObjectBase>();

        public bool Visu = true;

        public Simulation(float TimestepSize, float MaxTime, List<Particle> InitialParticles, List<EmitterBase> Emitters, List<RecieverBase> Recievers, List<ObjectBase> Objects)
        {
            this.TimestepSize = TimestepSize;
            this.MaxTime = MaxTime;
            Particles = InitialParticles;
            this.Emitters = Emitters;
            Reciever = Recievers;
            this.Objects = Objects;



        }



        public unsafe void RunSimulation()
        {
            Camera3D camera = new Camera3D(
                    new System.Numerics.Vector3(15, 15, 15),
                    new System.Numerics.Vector3(0, 0, 0),
                    new System.Numerics.Vector3(1, 1, 1),
                    0f,
                    CameraProjection.CAMERA_PERSPECTIVE
                    );

            if (Visu)
            {
                Raylib.InitWindow(1280, 720, "Hello, Raylib-CsLo");
                Raylib.SetTargetFPS(60);
            }

            Time = 0;
            Timestep = 0;

            for (Time = 0; Time < MaxTime; Time += TimestepSize)
            {
                MakeStep();
                Timestep++;

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.WHITE);
                Raylib.DrawText("t = " + Time, 10, 10, 20, Raylib.RED);

                Raylib.DrawCircle(0, 0, 10, Raylib.BLUE);

                foreach (var p in Particles)
                {
                    Raylib.DrawCircle((int)p.Position.X, (int)p.Position.Y, 2, Raylib.BLACK);
                }

                Raylib.UpdateCamera(ref camera);
                Raylib.EndDrawing();

               
                
                

            }

            Raylib.CloseWindow();

        }

        public void MakeStep()
        {
            List<Particle> NewParticles = new List<Particle>();

            foreach (var Emitter in Emitters)
            {
                foreach (var p in Emitter.EmittedParticles(Time))
                {
                    Particles.Add(p);
                }
            }

            foreach (var Particle in Particles)
            {

                bool collided = false;
                //Check for collision
                foreach (var Object in Objects)
                {
                    collided = Object.CheckForCollision(Particle, TimestepSize);

                    if (collided)
                    {
                        NewParticles.Add(Object.Reflect(Particle, TimestepSize));
                        break; //Only one collision per timestep
                    }
                    else
                    {

                    }
                }

                if (!collided)
                {
                    Particle.UpdateWithDelta(TimestepSize);
                    NewParticles.Add(Particle);
                }
            }

            foreach (var R in Reciever)
            {
                R.TakeMeasurement(Time, this);
            }

            Particles = NewParticles;

        }


    }
}
