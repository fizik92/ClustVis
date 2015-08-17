using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolVis
{
    class Quaternion
    {
        float x, y, z;
        float w;

        public Quaternion()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 1;
        }

        public Quaternion(float a, float b, float c, float d)
        {
            x = a;
            y = b;
            z = c;
            w = d;
        }

        public Quaternion(Vector v, float angle)
        {
            x = (float)(v.X * Math.Sin(angle / 2));
            y = (float)(v.Y * Math.Sin(angle / 2));
            z = (float)(v.Z * Math.Sin(angle / 2));
            w = (float)(Math.Cos(angle / 2));
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public float W
        {
            get { return w; }
            set { w = value; }
        }

        public void setQuaternion(float a, float b, float c, float d)
        {
            x = a;
            y = b;
            z = c;
            w = d;
        }

        // Инверсный кватернион
        public static Quaternion operator !(Quaternion q)
        {
            Quaternion c = new Quaternion();
            float norma = q.X * q.X + q.Y * q.Y + q.Z * q.Z + q.W * q.W;
            c.X = -q.X / norma;
            c.Y = -q.Y / norma;
            c.Z = -q.Z / norma;
            c.W = q.W / norma;
            return c;
        }

        // Умножение кватерниона на кватернион
        public static Quaternion operator *(Quaternion q, Quaternion p)
        {
            Quaternion c = new Quaternion();
            c.X = q.Y * p.Z - q.Z * p.Y + q.W * p.X + p.W * q.X;
            c.Y = q.Z * p.X - q.X * p.Z + q.W * p.Y + p.W * q.Y;
            c.Z = q.X * p.Y - q.Y * p.X + q.W * p.Z + p.W * q.Z;
            c.W = q.W * p.W - q.X * p.X - q.Y * p.Y - q.Z * p.Z;
            return c;
        }

        // Умножение вектора на кватернион
        public static Vector operator *(Vector v, Quaternion q)
        {
            Quaternion c = new Quaternion();
            Quaternion p = new Quaternion(v.X, v.Y, v.Z, 0);
            c = q * p;
            p = c * !q;
            return new Vector(p.X, p.Y, p.Z);
        }
    }
}
