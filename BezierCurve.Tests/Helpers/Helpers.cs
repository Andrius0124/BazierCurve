using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace BezierCurve.Tests.Helpers
{
    public static class Helpers
    {
        public static void ComperVector2(List<Vector2> expected, List<Vector2> result, int precision)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].X,result[i].X,precision);
                Assert.Equal(expected[i].Y,result[i].Y,precision);
            }
        }
    }
}