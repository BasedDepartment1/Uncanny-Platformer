using UnityEngine;

namespace Assets.Physics
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Rotates 2D vector by given angle clockwise
        /// </summary>
        /// <param name="v">Vector to rotate</param>
        /// <param name="angle">Angle by which vector will be rotated</param>
        /// <returns>Rotated vector</returns>
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            return new Vector2(cos * v.x + sin * v.y, 
                -sin * v.x + cos * v.y);
        }
    }
}