using System.Collections.Generic;
using System.Numerics;

namespace BezierCurve.Models
{
    public class BezierCurveModel
    {
        public Vector2 StartPoint { get; set; } = Vector2.Zero;
        public Vector2 EndPoint { get; set; } = Vector2.One;
        public Vector2 StartPontControl { get; set; } = Vector2.Zero;
        public Vector2 EndPointControl { get; set; } = Vector2.One;
        public uint NumberOfIntervals { get; set; }= 0;
        public List<Vector2> PointsOnCurve { get; set; } = new List<Vector2>();
    }
}