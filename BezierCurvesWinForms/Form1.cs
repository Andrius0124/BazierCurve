using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BZC = BezierCurve.Logic.BezierCurve;

namespace BezierCurves
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 100;
            
            Point p1 = new Point(25,25);
            Point p2 = new Point(300,25);
            Point p3 = new Point(25,300);
            Point p4 = new Point(300, 300);
            List<Point> p = new List<Point>(){p1,p2,p3,p4};
            InitBezierCurve(Curve, p);


            textBox1.Text = "1";

        }

        private void InitCustomBezierCurve(PictureBox pic)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            BZC customCurve = new BZC();
            customCurve.Curve.StartPoint = new Vector2(Convert.ToSingle(textBox2.Text),Convert.ToSingle(textBox3.Text));
            customCurve.Curve.StartPontControl = new Vector2(Convert.ToSingle(textBox8.Text),Convert.ToSingle(textBox9.Text));
            customCurve.Curve.EndPoint = new Vector2(Convert.ToSingle(textBox6.Text),Convert.ToSingle(textBox7.Text));
            customCurve.Curve.EndPointControl = new Vector2(Convert.ToSingle(textBox4.Text),Convert.ToSingle(textBox5.Text));
            customCurve.Curve.NumberOfIntervals = (uint)trackBar1.Value;
            customCurve.GetAllPointsOnCurve();
            

            Bitmap DrawArea = new Bitmap(pic.Size.Width, pic.Size.Height);
            Graphics g = Graphics.FromImage(DrawArea);
            pic.BorderStyle = BorderStyle.FixedSingle;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            Pen bl = new System.Drawing.Pen(System.Drawing.Color.Black);
            Pen red = new System.Drawing.Pen(System.Drawing.Color.Red);
            
            Point p1 = new Point((int)customCurve.Curve.StartPoint.X,(int)customCurve.Curve.StartPoint.Y);
            Point p2 = new Point((int)customCurve.Curve.StartPontControl.X,(int)customCurve.Curve.StartPontControl.Y);
            Point p3 = new Point((int)customCurve.Curve.EndPoint.X,(int)customCurve.Curve.EndPoint.Y);
            Point p4 = new Point((int)customCurve.Curve.EndPointControl.X,(int)customCurve.Curve.EndPointControl.Y);
            
            List<Point> originalPoints = new List<Point>(){p1,p2,p3,p4};
            List<Point> points = new List<Point>(){p1,p2,p3,p4};
            
            float scaleX = (pic.Size.Width/2-10)/(float)GreatestX(points);
            float scaleY = (pic.Size.Height/2-10)/(float)GreatestY(points);
            
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new Point(pic.Size.Width / 2 + (int)(points[i].X * scaleX),(pic.Size.Height / 2 - (int)(points[i].Y * scaleY)));
            }
            Point xStart = new Point(0,pic.Size.Height/2);
            Point xEnd = new Point(pic.Size.Width,pic.Size.Height/2);
            Point yStart = new Point(pic.Size.Width/2,0); 
            Point yEnd = new Point(pic.Size.Width/2,pic.Size.Height); 
            
            g.DrawLine(Pens.Black,xStart,xEnd);
            g.DrawLine(Pens.Black,yStart,yEnd);
            

            for (int i = 0; i < customCurve.Curve.PointsOnCurve.Count; i++)
            {
                int x = pic.Size.Width / 2 + (int) (customCurve.Curve.PointsOnCurve[i].position.X * scaleX);
                int y = pic.Size.Height / 2 - (int) (customCurve.Curve.PointsOnCurve[i].position.Y * scaleY);
                
                g.DrawRectangle(red,x-1,y-1,2,2);
                if (i != customCurve.Curve.PointsOnCurve.Count - 1)
                {
                    int x1 = pic.Size.Width / 2 + (int) (customCurve.Curve.PointsOnCurve[i+1].position.X * scaleX);
                    int y1 = pic.Size.Height / 2 - (int) (customCurve.Curve.PointsOnCurve[i+1].position.Y * scaleY);
                    g.DrawLine(red,x,y,x1,y1);
                }
            }

            StringFormat stringFormat = new StringFormat {Alignment = StringAlignment.Far};
            for (int i = 0; i < points.Count; i++)
            {
                g.DrawRectangle(bl,points[i].X-1,points[i].Y-1,2,2);
                if (points[i].X > pic.Size.Width - 20)
                {
                    string coordinates = string.Format("{0}:{1}", originalPoints[i].X, originalPoints[i].Y);
                    g.DrawString(coordinates,new Font("Arial", 9),  SystemBrushes.WindowText, new Point(points[i].X,points[i].Y),stringFormat );
                }
                else
                {
                    g.DrawString(string.Format("{0}:{1}",originalPoints[i].X,originalPoints[i].Y),new Font("Arial", 9),  SystemBrushes.WindowText, points[i]);
                }
            }

            pic.Image = DrawArea;
        }

        private void InitBezierCurve(PictureBox pic, List<Point> points)
        {
            List<Point> originalPoints = new List<Point>(points);
           
            float scaleX = (pic.Size.Width/2-10)/(float)GreatestX(points);
            float scaleY = (pic.Size.Height/2-10)/(float)GreatestY(points);
            
            Bitmap DrawArea = new Bitmap(pic.Size.Width, pic.Size.Height);
            Graphics g = Graphics.FromImage(DrawArea);
            pic.BorderStyle = BorderStyle.FixedSingle;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            Pen bl = new System.Drawing.Pen(System.Drawing.Color.Black);
            Pen red = new System.Drawing.Pen(System.Drawing.Color.Red);
            
            Point xStart = new Point(0,pic.Size.Height/2);
            Point xEnd = new Point(pic.Size.Width,pic.Size.Height/2);
            Point yStart = new Point(pic.Size.Width/2,0); 
            Point yEnd = new Point(pic.Size.Width/2,pic.Size.Height); 

            g.DrawLine(Pens.Black,xStart,xEnd);
            g.DrawLine(Pens.Black,yStart,yEnd);

            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new Point(pic.Size.Width / 2 + (int)(points[i].X * scaleX),(pic.Size.Height / 2 - (int)(points[i].Y * scaleY)));
            }

            g.DrawBezier(red,points[0],points[1],points[2],points[3]);

            StringFormat stringFormat = new StringFormat {Alignment = StringAlignment.Far};
            for (int i = 0; i < points.Count; i++)
            {
                g.DrawRectangle(bl,points[i].X-1,points[i].Y-1,2,2);
                if (points[i].X > pic.Size.Width - 20)
                {
                    string coordinates = string.Format("{0}:{1}", originalPoints[i].X, originalPoints[i].Y);
                    g.DrawString(coordinates,new Font("Arial", 9),  SystemBrushes.WindowText, new Point(points[i].X,points[i].Y),stringFormat );
                }
                else
                {
                    g.DrawString(string.Format("{0}:{1}",originalPoints[i].X,originalPoints[i].Y),new Font("Arial", 9),  SystemBrushes.WindowText, points[i]);
                }
            }

            
            pic.Image = DrawArea;
        }

        private int GreatestX(List<Point> points)
        {
            var greatestX = 0;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X  > greatestX)
                {
                    greatestX = points[i].X;
                }
            }

            return greatestX;
        }
        private int GreatestY(List<Point> points)
        {
            var greatestY = 0;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y  > greatestY)
                {
                    greatestY = points[i].Y;
                }
            }

            return greatestY;
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_LocationChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitCustomBezierCurve(MyBezierCurve);
        }
    }
}
