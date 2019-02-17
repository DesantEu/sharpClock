using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace sharp_clock
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Random rnd = new Random(DateTime.Now.Millisecond);
        int count = 0;
        Point center;
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Text = (count++).ToString();
            Graphics g = e.Graphics;
            int sec = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int hour = 0 ;
            if (DateTime.Now.Hour > 12) hour = DateTime.Now.Hour - 12;
            else hour = DateTime.Now.Hour;
            label2.Text = hour.ToString();

            Pen pen = new Pen(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)), 3);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //sec
            g.DrawLine(pen, center, GetNextSecPoint(center, 85, sec));
            //lines
            for (int i = 0; i < 60; i++)
                g.DrawLine(Pens.Black, GetNextSecPoint(center, 82, i), GetNextSecPoint(center, 90, i));
            for (int i = 0; i < 12; i++)
                g.DrawLine(Pens.Red, GetNextSecPoint(center, 80, i * 5), GetNextSecPoint(center, 95, i * 5));
            //minute
            g.DrawLine(pen, center, GetNextMinPoint(center, 70, min, sec, 12));
            g.DrawLine(pen, center, GetNextHourPoint(center, 55, hour, min, 12));
        }

        protected Point GetNextSecPoint(Point center, int R, int sec)
        {
            int x = center.X + (int)(R * Math.Cos(Math.PI * sec / 30 - Math.PI / 2));
            int y = center.Y + (int)(R * Math.Sin(Math.PI * sec / 30 - Math.PI / 2));
            return new Point(x, y);
        }
        //min
        protected Point GetNextMinPoint(Point center, int R, int min, int sec, int accuracy)
        {
            int x = center.X + (int)(R * Math.Cos(Math.PI * (min * accuracy + sec / (60/accuracy)) / (30*accuracy) - Math.PI / 2));
            int y = center.Y + (int)(R * Math.Sin(Math.PI * (min * accuracy + sec / (60 / accuracy)) / (30 * accuracy) - Math.PI / 2));
            return new Point(x, y);
        }
        //hour
        protected Point GetNextHourPoint(Point center, int R, int hrs, int min, int accuracy)
        {
            int x = center.X + (int)(R * Math.Cos(Math.PI * (hrs * accuracy + min / (60 / accuracy)) / (6 * accuracy)-Math.PI/2));
            int y = center.Y + (int)(R * Math.Sin(Math.PI * (hrs * accuracy + min / (60 / accuracy)) / (6 * accuracy) - Math.PI / 2));
            return new Point(x, y);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
