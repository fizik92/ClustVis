using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolVis
{
    class Vector
    {
        float x, y, z;

        public Vector()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector(float a, float b, float c)
        {
            x = a;
            y = b;
            z = c;
        }

        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
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

        public void setVector(float a, float b, float c)
        {
            x = a;
            y = b;
            z = c;
        }

        public void copy(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        // Длина вектора
        public float Lenght
        {
            get { return (float)Math.Sqrt(x * x + y * y + z * z); }
        }

        // Нормализация
        public void normolize()
        {
            float a = (float)(Math.Sqrt(x * x + y * y + z * z));
            x = x / a;
            y = y / a;
            z = z / a;
        }

        // Сумма двух векторов
        public static Vector operator -(Vector v, Vector w)
        {
            return new Vector(v.x - w.x, v.y - w.y, v.z - w.z);
        }

        // Произведение двух векторов
        public static Vector operator +(Vector v, Vector w)
        {
            return new Vector(v.x + w.x, v.y + w.y, v.z + w.z);
        }

        // Расстояние между двумя векторами
        public static float operator &(Vector v, Vector w)
        {
            return (float)(Math.Sqrt((v.X - w.X) * (v.X - w.X)
            + (v.Y - w.Y) * (v.Y - w.Y)
            + (v.Z - w.Z) * (v.Z - w.Z)));
        }

        // Векторное произведение
        public static Vector operator *(Vector v, Vector w)
        {
            return new Vector(v.Y * w.Z - v.Z * w.Y,
                v.Z * w.X - v.X * w.Z, v.X * w.Y - v.Y * w.X);
        }

        // Скалярное произведение
        public static float operator %(Vector v, Vector w)
        {
            return (v.X * w.X + v.Y * w.Y + v.Z * w.Z);
        }

        // Угол
        public static float operator ^(Vector v, Vector w)
        {
            float cosa = (v % w) / (v.Lenght * w.Lenght);
            float a = (float)(Math.Acos(cosa)); // в радианах
            return a * 180f / 3.14f;
        }

        // Эта функция возвращает кватернион, на который надо умножить первый вектор,
        // чтобы получить второй
        // Shortest arc
        public static Quaternion operator |(Vector v, Vector w)
        {
            float cosa = (v % w) /
                (((new Vector(0, 0, 0)) & v) * ((new Vector(0, 0, 0)) & w));
            Quaternion q = new Quaternion(v * w, (float)(Math.Acos(cosa)));
            //То, что ниже, не работает. Найдено в интернете.
            //float k = (float)(Math.Sqrt(lenghtVector(v1) * lenghtVector(v2)));
            //Quaternion q = new Quaternion(cp.X, cp.Y, cp.Z, sp + k);
            // еще рассмотреть частный случай
            return q;
        }
    }
}
