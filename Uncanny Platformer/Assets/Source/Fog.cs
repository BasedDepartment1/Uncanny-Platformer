using System;
using UnityEngine;

namespace Source
{

    public class Fog : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float scale;
        [SerializeField] private float colorOffset;
        private PerlinNoise perlinNoiseGenerator;
        private float offsetX = 0;
        private float offsetY = 0;


        private void Start()
        {
            perlinNoiseGenerator = new PerlinNoise();
        }

        private void Update()
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = GenerateTexture();
            offsetX += 0.001f;
            offsetY += 0.001f;
        }

        private Texture2D GenerateTexture()
        {
            var texture = new Texture2D(width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = CalculateColor(x, y);
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();
            return texture;
        }

        private Color CalculateColor(int x, int y)
        {
            var xCoord = (float) x / width * scale + offsetX;
            var yCoord = (float) y / height * scale + offsetY;

            var colorCode = perlinNoiseGenerator.Noise(xCoord, yCoord);
            return new Color(colorCode + colorOffset, colorCode + colorOffset, colorCode + colorOffset, colorCode);
        }
    }
}