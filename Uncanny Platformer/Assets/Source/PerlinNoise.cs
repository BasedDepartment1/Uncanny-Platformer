using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Source
{
    
    public class PerlinNoise
    {
        private readonly byte[] gradientBuffer;
        
        public PerlinNoise(int seed = 0)
        {
            var random = new Random(seed);
            gradientBuffer = new byte[1024];
            random.NextBytes(gradientBuffer);
        }

        /// <summary>
        /// Calculate Perlin noise in point with (fx, fy) coordinates
        /// </summary>
        public float Noise(float fx, float fy)
        {
            var floorX = Mathf.FloorToInt(fx);
            var floorY = Mathf.FloorToInt(fy);

            var relativeX = fx - floorX;
            var relativeY = fy - floorY;
            
            var gradientVectors = GetSquareBounds(new[] {0, 1}, floorX, floorY)
                .Select(GetGradient);
            var vertexVectors = GetSquareBounds(new[] {0, -1}, relativeX, relativeY);

            var dots = gradientVectors.Zip(vertexVectors, Vector2.Dot).ToArray();

            relativeX = ModifyParameter(relativeX);
            relativeY = ModifyParameter(relativeY);

            var interpolatedX = Mathf.Lerp(dots[0], dots[1], relativeX);
            var interpolatedY = Mathf.Lerp(dots[2], dots[3], relativeX);

            return Mathf.Lerp(interpolatedX, interpolatedY, relativeY);
        }

        /// <summary>
        /// Find corresponding pseudorandom gradient vector
        /// </summary>
        private Vector2 GetGradient(Vector2 v)
        {
            var fx = Mathf.RoundToInt(v.x);
            var fy = Mathf.RoundToInt(v.y);
            var gradientIndex = PseudoRandomNumber(fx, fy);
            var gradientNumber = gradientBuffer[gradientIndex] & 3;

            return gradientNumber switch
            {
                0 => new Vector2(1, 0),
                1 => new Vector2(-1, 0),
                2 => new Vector2(0, 1),
                _ => new Vector2(0, -1)
            };
        }

        private static int PseudoRandomNumber(int num1, int num2)
        {
            return (int)(((num1 * 1836311903) ^ (num2 * 2971215073) + 4807526976) & 1023);
        }

        /// <summary>
        /// Modifies given parameter t via quintic curve 
        /// </summary>
        private static float ModifyParameter(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static IEnumerable<Vector2> GetSquareBounds(int[] bounds, float fx, float fy)
        {
            return bounds
                .SelectMany(y => bounds
                    .Select(x => new Vector2(fx + x, fy + y)));
        }
    }
}