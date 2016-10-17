using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSorting
{
    public class Sorting
    {
        private static System.Diagnostics.Stopwatch sortingWatch = new System.Diagnostics.Stopwatch();
        private static long count = 0;
        public static float currentTime, minTime = 9999, maxTime, avgTime;

        public static string getCurrentTime()
        {
            return Sorting.currentTime.ToString("n6") + "ms";
        }

        public static string getMinTime()
        {
            return Sorting.minTime.ToString("n6") + "ms";
        }

        public static string getMaxTime()
        {
            return Sorting.maxTime.ToString("n6") + "ms";
        }

        public static string getAvgTime()
        {
            return Sorting.avgTime.ToString("n6") + "ms";
        }

        public static void DepthSort(Particle[] particles)
        {
            sortingWatch.Restart();

            // You can select which sorting algorithm you'll be using by uncommenting one of the two function calls below
            // You can visually test both of your algorithms this way

            //QuicksortDepthSort(particles);
            InsertionDepthSort(particles);

            sortingWatch.Stop();
            UpdateTimes((float)sortingWatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency);
        }

        public static void InsertionDepthSort(Particle[] particles)
        {
            for (int i = 1; i < particles.Length; i++) {
                Particle p = particles[i];
                float val = p.depth;
                int j = 0;
                for (j = i - 1; j >= 0 && particles[j].depth > p.depth; j--) {
                    particles[j + 1] = particles[j];
                }
                particles[j + 1] = p;
            }
        }

        public static void QuicksortDepthSort(Particle[] particles)
        {
           QuicksortHelper(0, particles.Length-1, particles);
        }

        private static void QuicksortHelper(int b, int e, Particle[] p) {
            if (b == e)
                return;
            Particle pivot = p[(b + e) / 2];
            p[(b + e) / 2] = p[e];
            p[e] = pivot;
            int nextLeft = b;
            for (int i = b; i < e; i++) {
                if (p[i].depth < pivot.depth) {
                    Particle temp = p[i];
                    p[i] = p[nextLeft];
                    p[nextLeft] = temp;
                    nextLeft++;
                }
            }
            p[e] = p[nextLeft];
            p[nextLeft] = pivot;
            QuicksortHelper(b, (b + e) / 2, p);
            QuicksortHelper(((b + e) / 2) + 1, e, p);
        }

        public static void UpdateTimes(float time)
        {
            time *= 1000;
            count++;
            currentTime = time;
            minTime = minTime < time ? minTime : time;
            maxTime = maxTime > time ? maxTime : time;
            avgTime = avgTime == 0 ? time : ((avgTime * (count - 1)) + time) / count;
        }
    }
}
