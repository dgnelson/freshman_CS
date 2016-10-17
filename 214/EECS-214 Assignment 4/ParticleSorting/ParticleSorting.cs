using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleSorting
{
    public partial class Form1 : Form
    {

        int time = Environment.TickCount;
        private static System.Diagnostics.Stopwatch watch;

        public Form1()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(DrawFrame);
            particleSystem = new ParticleSystem(4000);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);



            watch = System.Diagnostics.Stopwatch.StartNew();

            watch.Stop();
            float dt = (float)watch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            watch.Restart();

        }

        ParticleSystem particleSystem;

        void DrawFrame(object sender, PaintEventArgs args)
        {
            var newTime = Environment.TickCount;
            var interval = newTime - time;
            time = newTime;
            particleSystem.Update(interval);
            args.Graphics.Clear(Color.Gray);
            particleSystem.Render(args.Graphics);
            Sorting.DepthSort(particleSystem.particles);
            this.Text = "MinTime: " + Sorting.getMinTime() + " MaxTime: " + Sorting.getMaxTime() + " AvgTime: " + Sorting.getAvgTime();

            this.Invalidate();
        }
    }
}
