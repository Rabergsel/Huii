namespace Huii
{
    public abstract class RecieverBase
    {

        public List<Tuple<float, float>> Measurements = new List<Tuple<float, float>>();

        public abstract float GetMeasurement(float Time, Simulation sim);

        public void TakeMeasurement(float Time, Simulation sim)
        {
            Measurements.Add(new Tuple<float, float>(Time, GetMeasurement(Time, sim)));
        }

    }
}
