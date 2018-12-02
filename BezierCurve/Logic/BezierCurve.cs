using System;
using System.Numerics;
using BezierCurve.Models;

namespace BezierCurve.Logic
{
    public class BezierCurve
    {
        public BezierCurveModel Curve { get; set; } = new BezierCurveModel();
        
        public void GetAllPointsOnCurve()
        {
            if (Curve.NumberOfIntervals < 1)
            {
                throw new Exception("Number of intervals can not to be less than 1");
            }
            Curve.PointsOnCurve.Clear();
            for (int i =0; i <= Curve.NumberOfIntervals; i++)
            {
                Vector2 a = Vector2.Lerp(Curve.StartPoint, Curve.StartPontControl, i/(float)Curve.NumberOfIntervals);
                Vector2 b = Vector2.Lerp(Curve.StartPontControl, Curve.EndPointControl, i/(float)Curve.NumberOfIntervals);
                Vector2 c = Vector2.Lerp(Curve.EndPointControl, Curve.EndPoint, i/(float)Curve.NumberOfIntervals);
                Vector2 d = Vector2.Lerp(a, b, i/(float)Curve.NumberOfIntervals);
                Vector2 e = Vector2.Lerp(b, c, i/(float)Curve.NumberOfIntervals);
                Vector2 pointOnCurve = Vector2.Lerp(d, e, i/(float)Curve.NumberOfIntervals);
                Curve.PointsOnCurve.Add(new Vec2Time()
                {
                    position = pointOnCurve,
                    time = i/(float)Curve.NumberOfIntervals
                });
            }
        }
        
        
    }
}