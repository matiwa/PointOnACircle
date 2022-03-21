using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PointOnACircle
{
    public partial class Form1 : Form
    {
        Thread th;
        Graphics g;
        Graphics fG;
        Bitmap btm;

        bool drawing = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btm = new Bitmap(580, 580);
            g = Graphics.FromImage(btm);
            fG = CreateGraphics();
            th = new Thread(Draw);
            th.IsBackground = true;
            th.Start();
        }

        public void Draw()
        {
            float angle = 0.0f;
            PointF org = new PointF(250, 250);
            float rad = 250;
            Pen pen = new Pen(Brushes.Azure, 3.0f);
            RectangleF area = new RectangleF(30, 30, 500, 500);
            RectangleF circle = new RectangleF(0, 0, 50, 50);

            PointF loc = Point.Empty;
            PointF img = new PointF(20, 20);

            fG.Clear(Color.Black);

            while (drawing)
            {
                g.Clear(Color.Black);

                g.DrawEllipse(pen, area);
                loc = CirclePoint(rad, angle, org);

                circle.X = loc.X - (circle.Width / 2) + area.X;
                circle.Y = loc.Y - (circle.Height / 2) + area.Y;


                g.DrawEllipse(pen, circle);

                fG.DrawImage(btm, img);

                if (angle < 360) angle += 0.5f;
                else angle = 0;
            }
        }

        public PointF CirclePoint(float radius, float angleInDegrees, PointF origin)
        {
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F) + origin.X);
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F) + origin.Y);

            return new PointF(x, y);
        }
    }
}
