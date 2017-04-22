using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public static class MathExtensions
    {
        public static Vector3 GetAveragePosition<T>(this List<T> t) where T : MonoBehaviour
        {
            int sum = 0;
            List<Vector3> positions = new List<Vector3>();
            foreach (T i in t)
            {
                positions.Add(i.transform.position);
            }
            sum = positions.Count;
            if (sum== 0)
            {
                sum = 1;
            }
            Vector3 averagePosition = new Vector3();
            foreach (Vector3 v in positions)
            {
                averagePosition += v;
            }
            averagePosition = averagePosition / sum;
            return averagePosition;
        }
        public static float RotPerMinuteToRotPerFrame(float rotation, float deltaTime)
        {
            return FromPerMinuteToPerFrame(rotation, deltaTime) * 360;
            //return (rotation * 360.0f * Time.deltaTime) / 60.0f;
        }
        public static float FromPerSecToPerFrame(float perSec,float deltaTime)
        {
            return perSec * deltaTime;
        }
        public static float FromPerMinuteToPerFrame(float perMinute, float deltaTime)
        {
            return FromPerMinuteToPerSecond(perMinute) * deltaTime;
        }
        public static float FromPerMinuteToPerSecond(float perMinute)
        {
            return perMinute * (1.0f/60.0f);
        }
        public static int RoundOnEndToInt(float f)
        {
            int d = Mathf.CeilToInt(f);
            float difference = Mathf.Abs(d - f);
            if (difference <= 0.1f)
            {
                return d;
            }
            return Mathf.FloorToInt(f);
        }
        public static Vector3 ShortenVector(this Vector3 v, float by)
        {
            Vector3 shortenedVector = new Vector3();

            shortenedVector = v - v.normalized * by;

            return shortenedVector;
        }
    }