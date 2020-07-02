using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Position
    {
        private double x;
        private double y;
        private double z;
        public double X {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public Position(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public double d(Position point1, Position point2) // Khoang cach tu doi tuong goi ham den _point.(trong khong gian 3D)
        {
            double kq;
            kq = Math.Sqrt(Math.Pow(point1.X - point2.X, 2.0) + Math.Pow(point1.Y - point2.Y, 2.0));
            return kq;
        }

    }
}
