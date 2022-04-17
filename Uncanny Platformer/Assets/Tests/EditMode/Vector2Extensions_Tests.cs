using NUnit.Framework;
using UnityEngine;
using Assets.Physics;

namespace Assets.Tests.EditMode
{
    public class Vector2Extensions_Tests
    {
        [Test]
        public static void TestBasicOperations()
        {
            var a = new Vector2(1f, 2f);
            var b = new Vector2(2f, 3f);
            Assert.AreEqual(8f, Vector2.Dot(a, b));
            Assert.AreEqual(new Vector2(3f, 5f), a + b);
            Assert.AreEqual(new Vector2(2f, 4f), a * 2f);
        }
        
        [TestCase(new[]{0f, 0f}, Mathf.PI / 2, new[]{0f, 0f})]
        [TestCase(new[]{0f, 1f}, Mathf.PI / 2, new[]{1f, 0f})]
        [TestCase(new[]{1f, 1f}, Mathf.PI / 2, new[]{1f, -1f})]
        [TestCase(new[]{-1f, 0f}, Mathf.PI / 2, new[]{0f, 1f})]
        public static void TestRotation(float[] vCoords, float angle, float[] expectedCoords)
        {
            var v = new Vector2(vCoords[0], vCoords[1]);
            var expected = new Vector2(expectedCoords[0], expectedCoords[1]);
            var actualRotatedVector = v.Rotate(angle);
            Assert.AreEqual(expected.magnitude, actualRotatedVector.magnitude, 1e-5,
                "Vector size changed during the rotation");
            Assert.AreEqual(expected.x, actualRotatedVector.x, 1e-5,
                "X coordinates do not match");
            Assert.AreEqual(expected.y, actualRotatedVector.y, 1e-5,
                "Y coordinates do not match");
        }
    }
}