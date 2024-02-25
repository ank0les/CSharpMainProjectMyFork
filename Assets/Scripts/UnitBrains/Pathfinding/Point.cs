using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UnitBrains.Pathfinding
{
    public class Point
    {
        public int X; 
        public int Y;
        public int Cost = 10;
        public int Estimate;
        public int Value;

        public Point(int x, int y) 
        { 
            X = x;
            Y = y;
        }

        public void CalcEstimate(int targetX, int targetY)
        {
            Estimate = Math.Abs(X - targetX) + Math.Abs(Y - targetY);
        }

        public void CalcValue()
        {
            Value = Cost + Estimate;
        }
    }
}
