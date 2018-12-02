using System;
using System.Collections.Generic;
using System.Numerics;
using BezierCurve.Models;
using Newtonsoft.Json;
using Xunit;
using static BezierCurve.Tests.Helpers.Helpers;

namespace BezierCurveTests.Bezier
{
    public class BezierCurveTests
    {
        private readonly BezierCurve.Logic.BezierCurve _bezier;

        public BezierCurveTests()
        {
            _bezier = new BezierCurve.Logic.BezierCurve();
        }

        [Fact]
        public void GetAllPointsOnCurve_AllVectorsOnX0AndIntervals5_CurveIsAStraightLine()
        {
            _bezier.Curve.StartPoint = new Vector2(0f,0f);
            _bezier.Curve.EndPoint = new Vector2(0f,5f);
            _bezier.Curve.StartPontControl = new Vector2(0f,0f);
            _bezier.Curve.EndPointControl = new Vector2(0f,5f);
            _bezier.Curve.NumberOfIntervals = 5;
            _bezier.GetAllPointsOnCurve();
            
            var expected = new List<Vec2Time>()
            {
                new Vec2Time()
                {
                    position = new Vector2(0f,0f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0f,0.52f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0f,1.76f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0f,3.24f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0f,4.48f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0f,5f),
                    time = 0f
                }
            };
            
            ComperVector2(expected,_bezier.Curve.PointsOnCurve,4);
        }

        [Fact]
        public void GetAllPointsOnCurve_StartingPointx0y0EndingPointx1y1SameControls_LinearCurve()
        {
            _bezier.Curve.StartPoint = new Vector2(0f,0f);
            _bezier.Curve.EndPoint = new Vector2(1f,1f);
            _bezier.Curve.StartPontControl = new Vector2(0f,0f);
            _bezier.Curve.EndPointControl = new Vector2(1f,1f);
            _bezier.Curve.NumberOfIntervals = 5;
            _bezier.GetAllPointsOnCurve();
            
            var expected = new List<Vec2Time>()
            {
                new Vec2Time()
                {
                    position = new Vector2(0f,0f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0.104f,0.104f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0.352f,0.352f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0.648f,0.648f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(0.896f,0.896f),
                    time = 0f
                },
                new Vec2Time()
                {
                    position = new Vector2(1f,1f),
                    time = 0f
                }
            };
            
            ComperVector2(expected,_bezier.Curve.PointsOnCurve,4);
        }

        [Fact]
        public void GetAllPointsOnCurve_NumberOfIntervalsLessThan1_Exception()
        {
            _bezier.Curve.StartPoint = new Vector2(0f,0f);
            _bezier.Curve.EndPoint = new Vector2(1f,1f);
            _bezier.Curve.StartPontControl = new Vector2(0f,0f);
            _bezier.Curve.EndPointControl = new Vector2(1f,1f);
            _bezier.Curve.NumberOfIntervals = 0;

            Exception ex = Assert.Throws<Exception>(() => _bezier.GetAllPointsOnCurve());
            
            Assert.Equal("Number of intervals can not to be less than 1",ex.Message);
        }

    }
}