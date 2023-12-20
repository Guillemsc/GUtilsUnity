using Random = UnityEngine.Random;
using GUtils.Randomization.Generators;

namespace GUtilsUnity.Randomization.Generators
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IRandomGenerator"/> based on the Unity's C# <see cref="Random"/>.
    /// </summary>
    public sealed class UnityRandomGenerator : IRandomGenerator
    {
        public static readonly UnityRandomGenerator Instance = new();

        UnityRandomGenerator() { }

        public int NewInt()
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }

        public int NewInt(int minInclusive, int maxExclusive)
        {
            return Random.Range(minInclusive, maxExclusive);
        }

        public float NewFloat()
        {
            return Random.Range(float.MinValue, float.MaxValue);
        }

        public float NewFloat(float minInclusive, float maxInclusive)
        {
            return Random.Range(minInclusive, maxInclusive);
        }
    }
}
