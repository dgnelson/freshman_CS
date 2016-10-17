using System;
using System.Drawing;

namespace ParticleSorting
{
    public class ParticleSystem
    {
        public static Random random;
        public Particle[] particles;
        private int size;

        public ParticleSystem() : this(100) { } // Default constructor creates 100 particles

        public ParticleSystem(int n)
        {
            random = new Random();
            size = n;
            particles = new Particle[n];
            for (int i = 0; i < n; i++)
                particles[i] = new Particle();
        }

        public Particle getParticle(int i)
        {
            return particles[i];
        }

        public void setParticle(int i, Particle p)
        {
            particles[i] = p;
        }

        public int Length()
        {
            return size;
        }

        public bool IsDepthSorted()
        {
            float previous = float.MinValue;
            int i = 0;
            foreach (Particle particle in particles)
            {
                if (previous > particle.depth)
                    return false;
                previous = particle.depth;
                i++;
            }
            return true;
        }

        const float focalLength = 100f;
        public void Render(Graphics graphics)
        {
            foreach (var p in particles)
            {
                var position3d = p.getPos();
                position3d.z = 1;
                var location = new PointF(415+focalLength*position3d.x/position3d.z, 300+focalLength*position3d.y/position3d.z) ;      
                graphics.FillEllipse(brush, new RectangleF(location, new SizeF(10/position3d.z, 10/position3d.z)));
            }
        }

        Brush brush = new SolidBrush(Color.FromArgb(128, 210, 168, 255));
        const float scaling = 0.0001f;
        public void Update(int ms)
        {
            foreach (var p in particles) {
                var position = p.getPos();
                var speed = p.getSpeed();
                position.x += speed.x * ms * scaling;
                position.y += speed.y * ms * scaling;
                position.z += speed.z * ms * scaling;
                p.setPos(position);
            }
        }
    }
}
