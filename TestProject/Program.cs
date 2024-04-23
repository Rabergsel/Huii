using Huii;
using Huii.Objects.Planes;
using Huii.Recievers.Logger;
using Raylib_CsLo;



Simulation sim = new Simulation(
    0.001f,
    10f,
    new List<Particle>()
    {
        new Particle() {Direction = new Vector3D(1, 0, 0), Position = new Vector3D(0, 0, 0), Speed = 1.5f}
    },
    new List<EmitterBase>(),
    new List<RecieverBase>()
    {
    },
    new List<ObjectBase>()
    {
        new YZPlane(-10, 10, -10, 10, 5),
        new YZPlane(-10, 10, -10, 10, -5)
    }
  );

sim.RunSimulation();
Console.ReadLine();