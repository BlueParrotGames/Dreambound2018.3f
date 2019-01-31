using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentGeneration
{
    public static class HeightMapGenerator
    {

        public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
        {
            float[,] values = Noise.GenerateNoiseMap(width, height, settings.noiseSettings, sampleCentre);

            AnimationCurve heightCurve_threadsafe = new AnimationCurve(settings.heightCurve.keys);

            float minValue = float.MaxValue;
            float maxValue = float.MinValue;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    values[i, j] *= heightCurve_threadsafe.Evaluate(values[i, j]) * settings.heightMultiplier;

                    if (values[i, j] > maxValue)
                    {
                        maxValue = values[i, j];
                    }
                    if (values[i, j] < minValue)
                    {
                        minValue = values[i, j];
                    }
                }
            }

            return new HeightMap(values, minValue, maxValue);
        }

        public static float GetHeighestPoint(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
        {
            return GenerateHeightMap(width, height, settings, sampleCentre).maxValue;
        }
        public static float GetHeighestPoint(HeightMap heightMap)
        {
            return heightMap.maxValue;
        }

        public static float GetLowestPoint(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
        {
            return GenerateHeightMap(width, height, settings, sampleCentre).minValue;
        }
        public static float GetLowestPoint(HeightMap heightMap)
        {
            return heightMap.minValue;
        }

        public static float GetTotalTerrainHeight(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
        {
            HeightMap heightMap = GenerateHeightMap(width, height, settings, sampleCentre);
            return (heightMap.maxValue + Mathf.Abs(heightMap.minValue));
        }
        public static float GetTotalTerrainHeight(HeightMap heightMap)
        {
            return (heightMap.maxValue + Mathf.Abs(heightMap.minValue));
        }

    }

    public struct HeightMap
    {
        public readonly float[,] values;
        public readonly float minValue;
        public readonly float maxValue;

        public HeightMap(float[,] values, float minValue, float maxValue)
        {
            this.values = values;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }


}