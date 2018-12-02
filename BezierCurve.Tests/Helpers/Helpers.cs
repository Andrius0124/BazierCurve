using System.Collections.Generic;
using System.Numerics;
using BezierCurve.Models;
using Xunit;

namespace BezierCurve.Tests.Helpers
{
    public static class Helpers
    {
        public static void ComperVector2(List<Vec2Time> expected, List<Vec2Time> result, int precision)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].position.X,result[i].position.X,precision);
                Assert.Equal(expected[i].position.Y,result[i].position.Y,precision);
                Assert.Equal(expected[i].time,result[i].time,precision);                
            }
        }
    }
}