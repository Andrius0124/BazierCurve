using System;
using System.Numerics;

namespace BezierCurve
{
    static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-----------------Bezier Curve ---------------");
            Console.WriteLine("");
            var bezierCurve = new Logic.BezierCurve();
            Console.WriteLine("Enter start point:");
            bezierCurve.Curve.StartPoint = new Vector2(Convert.ToSingle(Console.ReadLine()),Convert.ToSingle(Console.ReadLine()));
            
            Console.WriteLine("Enter end point:");
            bezierCurve.Curve.EndPoint = new Vector2(Convert.ToSingle(Console.ReadLine()),Convert.ToSingle(Console.ReadLine()));
           
            
            Console.WriteLine("Enter start point control:");
            bezierCurve.Curve.StartPontControl = new Vector2(Convert.ToSingle(Console.ReadLine()),Convert.ToSingle(Console.ReadLine()));

            Console.WriteLine("Enter start point control:");
            bezierCurve.Curve.EndPointControl = new Vector2(Convert.ToSingle(Console.ReadLine()),Convert.ToSingle(Console.ReadLine()));

            
            Console.WriteLine("Enter number  of intervals:");
            while (bezierCurve.Curve.NumberOfIntervals < 1)
            {
                string input = Console.ReadLine();
                uint value;
                if (UInt32.TryParse(input, out value))
                {
                    if (value > 0)
                    {
                        bezierCurve.Curve.NumberOfIntervals = value;
                    }
                    else
                    {
                        Console.WriteLine("Number of intervals can not be less than 1");
                    }
                }
                else
                {
                    Console.WriteLine("Number of intervals must be an integer value and can not be less than 1 ");
                }

            }
            bezierCurve.GetAllPointsOnCurve();   
            foreach (var point in bezierCurve.Curve.PointsOnCurve)
            {
                Console.WriteLine(string.Format("Time: {0}  Position:{1}", point.time, point.position) );
            }

            Console.ReadKey();
        }


       
    }
}
