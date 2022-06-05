using System;
using System.Collections.Generic;
using NUnit.Framework;
using Source;
using System.Reflection;
using UnityEngine;
using System.Linq;
using System.Threading;
using Random = System.Random;


namespace Tests.EditMode
{
    [TestFixture]
    public class PerlinNoiseTests
    {
        [TestCase(1, 2, 3, 4)]
        [TestCase(666, 666, -98, -45)]
        [TestCase(342424, 454545, -2232f, -100000f)]
        public void Should_GetBoundsWithTwoBounds(int bound1, int bound2, float fx, float fy)
        {
            var perlinNoiseGenerator = new PerlinNoise();
            var method = typeof(PerlinNoise).GetMethod("GetSquareBounds", BindingFlags.NonPublic | BindingFlags.Static );
            var actual = (IEnumerable<Vector2>)method.Invoke(perlinNoiseGenerator,
                new object[] {new [] {bound1, bound2}, fx, fy});
            Assert.AreEqual(new []
            {
                new Vector2(bound1 + fx, bound1 + fy),
                new Vector2(bound2 + fx, bound1 + fy),
                new Vector2(bound1 + fx, bound2 + fy),
                new Vector2(bound2 + fx, bound2 + fy)
            }, actual.ToArray());
        }

        [TestCase(1, 2, 3, 4, 5)]
        [TestCase(666, 666, 666, -98, -45)]
        [TestCase(342424, 454545, 4434222, -2232f, -100000f)]
        public void Should_GetBoundsWithThreeBounds(int bound1, int bound2, int bound3, float fx, float fy)
        {
            var perlinNoiseGenerator = new PerlinNoise();
            var method = typeof(PerlinNoise).GetMethod("GetSquareBounds", BindingFlags.NonPublic | BindingFlags.Static);
            var actual = (IEnumerable<Vector2>) method.Invoke(perlinNoiseGenerator,
                new object[] {new[] {bound1, bound2, bound3}, fx, fy});
            Assert.AreEqual(new[]
            {
                new Vector2(bound1 + fx, bound1 + fy),
                new Vector2(bound2 + fx, bound1 + fy),
                new Vector2(bound3 + fx, bound1 + fy),
                new Vector2(bound1 + fx, bound2 + fy),
                new Vector2(bound2 + fx, bound2 + fy),
                new Vector2(bound3 + fx, bound2 + fy),
                new Vector2(bound1 + fx, bound3 + fy),
                new Vector2(bound2 + fx, bound3 + fy),
                new Vector2(bound3 + fx, bound3 + fy),
            }, actual.ToArray());
        }
        [Test]
        public void Should_PseudoRandomNumberGetOnValueWhenTimePasses()
        {
            var rn = new Random();
            var n1 = rn.Next(int.MaxValue);
            var n2 = rn.Next(int.MaxValue);
            var perlinNoiseGenerator = new PerlinNoise();
            var method = typeof(PerlinNoise).GetMethod("PseudoRandomNumber", BindingFlags.NonPublic | BindingFlags.Static);
            var v1 = method.Invoke(perlinNoiseGenerator,new object[] {n1, n2});
            Thread.Sleep(rn.Next(500));
            var v2 = method.Invoke(perlinNoiseGenerator,new object[] {n1, n2});
            Assert.AreEqual(v1, v2);
        }

        [TestCase(1f, 1f)]
        [TestCase(2.5f, -1f)]
        [TestCase(0.0001f, 0.00001f)]
        [TestCase(4.56575734f, 454.4543f)]
        public void Should_NoiseIsPeriodical(float x, float y)
        {
            var pn = new PerlinNoise();
            var expected = pn.Noise(x, y);
            var actual1 = pn.Noise(x + 1, y);
            var actual2 = pn.Noise(x, y + 1);
            var actual3 = pn.Noise(x + 1, y + 1);
            Assert.AreEqual(expected, actual1, 1e-4);
            Assert.AreEqual(expected, actual2, 1e-4);
            Assert.AreEqual(expected, actual3, 1e-4);
        }
    }
}