﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PRNG
    {
        private System.Random random;

        private static PRNG instance;

        public PRNG()
        {
        }

        public static bool IsInitialized
        {
            get { return GetInstance().random != null; }
        }

        private static PRNG GetInstance()
        {
            if (instance == null)
                instance = new PRNG();

            return instance;
        }

        public static int GetNumber(int min, int max)
        {
            if (GetInstance().random == null)
                return 0;
            return GetInstance().random.Next(min, max + 1);
        }

        public static float GetFloatNumber(float min, float max, int precision = 2)
        {
            float p = 1f;
            for (int i = 0; i < precision; i++)
            {
                p *= 10f;
            }
            return GetFloatNumber(min, max, p);
        }

        private static float GetFloatNumber(float min, float max, float precision)
        {
            if (GetInstance().random == null)
                return 0f;

            int MIN = (int)(min * precision);
            int MAX = (int)(max * precision + 1);
            return GetInstance().random.Next(MIN, MAX) / precision;
        }

        public static void ChangeSeed(int seed)
        {
            GetInstance().random = new System.Random(seed);
        }

        public static void ChangeSeed(string seed)
        {
            // Stringify the text to numbers
            string numbers = "";
            while (numbers.Length < 8)
            {
                foreach (char c in seed)
                {
                    numbers += ((int)c).ToString();
                }
            }

            // Grab the first 8 numbers and convert them to an actual number
            int iSeed = Convert.ToInt32(numbers.Substring(0, 8));

            // Do some magic with the seed number
            numbers = numbers.Remove(0, 8);
            ChangeSeed(0);
            foreach (char c in numbers)
            {
                int option = GetNumber(0, 3);
                if (option == 0) iSeed += c;
                else if (option == 1) iSeed -= c;
                else if (option == 2) iSeed *= c;
                else if (option == 3) iSeed /= c;
            }

            GetInstance().random = new System.Random(iSeed);
        }
    }
}
