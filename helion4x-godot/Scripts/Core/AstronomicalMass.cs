namespace Helion4x.Scripts
{
    public static class AstronomicalMass
    {
        private const float Sun = 1.989e30f;
        private const float Earth = 5.972e24f;
        private const float Moon = 7.342e22f;

        public static float ForMassType(MassType massType)
        {
            switch (massType)
            {
                case MassType.Sun:
                    return Sun;
                case MassType.Earth:
                    return Earth;
                case MassType.Moon:
                    return Moon;
                default:
                    return 0f;
            }
        }
    }
}