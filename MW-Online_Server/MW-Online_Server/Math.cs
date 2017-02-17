using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Maths = System.Math;

namespace MWOnlineServer.Math
{
    /// <summary>
    /// Representation of 3D vectors and points.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// Returns the x-coordinate of this <see cref="Vector3"/>.
        /// </summary>
        public float x { get; private set; }

        /// <summary>
        /// Returns the y-coordinate of this <see cref="Vector3"/>.
        /// </summary>
        public float y { get; private set; }

        /// <summary>
        /// Returns the z-coordinate of this <see cref="Vector3"/>.
        /// </summary>
        public float z { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 back
        {
            get
            {
                return new Vector3(0.0f, 0.0f, -1f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 down
        {
            get
            {
                return new Vector3(0.0f, -1f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 forward
        {
            get
            {
                return new Vector3(0.0f, 0.0f, 1f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 left
        {
            get
            {
                return new Vector3(-1f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 one
        {
            get
            {
                return new Vector3(1f, 1f, 1f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 right
        {
            get
            {
                return new Vector3(1f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 up
        {
            get
            {
                return new Vector3(0.0f, 1f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 zero
        {
            get
            {
                return new Vector3(0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector3"/>.
        /// </summary>
        public float magnitude
        {
            get
            {
                return (float)Maths.Sqrt(x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector3"/> (Read only).
        /// </summary>
        public float magnitudeSquared
        {
            get
            {
                return (x * x) + (y * y) + (z * z);
            }
        }

        /// <summary>
        /// Returns this <see cref="Vector3"/> with a magnitude of 1 (Read only).
        /// </summary>
        public Vector3 normalized
        {
            get
            {
                Vector3 norm = new Vector3(x, y, z);
                norm.Set(norm.x / magnitude, norm.y / magnitude, norm.z / magnitude);

                return norm;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class with the specified coordinates.
        /// </summary>
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class using coordinates specified by an interger value.
        /// </summary>
        public Vector3(float value)
        {
            x = value;
            y = value;
            z = value;
        }

        /// <summary>
        /// Set the XYZ values of this <see cref="Vector3"/>.
        /// </summary>
        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 diff = new Vector3(
                  a.x - b.x,
                  a.y - b.y,
                  a.z - b.z);
            return (float)Maths.Sqrt(Maths.Pow(diff.x, 2f) + Maths.Pow(diff.y, 2f) + Maths.Pow(diff.z, 2f));
        }

        /// <summary>
        /// Dot product between two vectors.
        /// </summary>
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z);
        }

        /// <summary>
        /// Returns a <see cref="Vector3"/> with the same direction as the specified <see cref="Vector3"/>, but with a length of one.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Normalize(Vector3 vector)
        {
            float l = vector.magnitude;
            return new Vector3(vector.x / l, vector.y / l, vector.z / l);
        }

        /// <summary>
        /// Cross product of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            float x = a.y * b.z - a.z * b.y;
            float y = a.z * b.x - a.x * b.z;
            float z = a.x * b.y - a.y * b.x;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 start, Vector3 end, float factor)
        {
            float x = start.x + ((end.x - start.x) * factor);
            float y = start.y + ((end.y - start.y) * factor);
            float z = start.z + ((end.z - start.z) * factor);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Reflects a vector off the plane defined by a normal.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vector3 Reflect(Vector3 dir, Vector3 normal)
        {
            float dubdot = 2.0f * ((dir.x * normal.x) + (dir.y * normal.y) + (dir.z * normal.z));
            float x = dir.x - (dubdot * normal.x);
            float y = dir.y - (dubdot * normal.y);
            float z = dir.z - (dubdot * normal.z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Projects a vector onto another vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        /// <returns></returns>
        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            return onNormal * Dot(vector, onNormal) / Dot(onNormal, onNormal);
        }

        /// <summary>
        /// Projects a vector onto a plane defined by a normal orthogonal to the plane.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            return (vector - Project(vector, planeNormal));
        }

        /// <summary>
        /// Returns the angle in degrees between from and to.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float Angle(Vector3 from, Vector3 to)
        {
            float dot = Dot(from.normalized, to.normalized);

            return (float)Maths.Acos((dot) * (180.0 / 3.141592653));
        }

        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            float x = (a.x < b.x) ? a.x : b.x;
            float y = (a.y < b.y) ? a.y : b.y;
            float z = (a.z < b.z) ? a.z : b.z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            float x = (a.x > b.x) ? a.x : b.x;
            float y = (a.y > b.y) ? a.y : b.y;
            float z = (a.z > b.z) ? a.z : b.z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Vector3"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
        }

        /// <summary>
        /// Returns a formmated <see cref="Vector3"/> string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X = {0} Y = {1} Z = {2}", x, y, z);
        }

        /// <summary>
        /// Returns a formmated <see cref="Vector3"/> string with a specified number format.
        /// </summary>
        /// <returns></returns>
        public string ToString(string numberFormat)
        {
            return string.Format("X = {0} Y = {1} Z = {2}", x.ToString(numberFormat), y.ToString(numberFormat), z.ToString(numberFormat));
        }

        /// <summary>
        /// Add a value to a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 l, Vector3 r)
        {
            return new Vector3(l.x + r.x, l.y + r.y, l.z + r.z);
        }

        /// <summary>
        /// Add a value to a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 l, float r)
        {
            return new Vector3(l.x + r, l.y + r, l.z + r);
        }

        /// <summary>
        /// Add a value to a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator +(float l, Vector3 r)
        {
            return new Vector3(r.x + l, r.y + l, r.z + l);
        }

        /// <summary>
        /// Subtract a value from a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 l, Vector3 r)
        {
            return new Vector3(l.x - r.x, l.y - r.y, l.z - r.z);
        }

        /// <summary>
        /// Subtract a value from a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 l, float r)
        {
            return new Vector3(l.x - r, l.y - r, l.z - r);
        }

        /// <summary>
        /// Subtract a value from a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator -(float l, Vector3 r)
        {
            return new Vector3(r.x - l, r.y - l, r.z - l);
        }

        /// <summary>
        /// Multiply a Vector3 with a value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 l, float r)
        {
            return new Vector3(l.x * r, l.y * r, l.z * r);
        }

        /// <summary>
        /// Multiply a Vector3 with a value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator *(float l, Vector3 r)
        {
            return new Vector3(r.x * l, r.y * l, r.z * l);
        }

        /// <summary>
        /// Multiply two Vector3 values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 l, Vector3 r)
        {
            return new Vector3(r.x * l.x, r.y * l.y, r.z * l.z);
        }

        /// <summary>
        /// Divide a Vector3 value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 l, float r)
        {
            return new Vector3(l.x / r, l.y / r, l.z / r);
        }

        /// <summary>
        /// Divide a Vector3 value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator /(float l, Vector3 r)
        {
            return new Vector3(r.x / l, r.y / l, r.z / l);
        }

        /// <summary>
        /// Divide a Vector3 value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 l, Vector3 r)
        {
            return new Vector3(r.x / l.x, r.y / l.y, r.z / l.z);
        }

        /// <summary>
        /// Returns a value determining whether the two Vector3 values are equal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3 l, Vector3 r)
        {
            return Equals(l, r);
        }

        /// <summary>
        /// Returns a value determining whether the two Vector3 values aren't equal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3 l, Vector3 r)
        {
            return !Equals(l, r);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector3"/> values are equal.
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static bool Equals(Vector3 position1, Vector3 position2)
        {
            return (position1.x == position2.x && position1.y == position2.y && position1.z == position2.z);
        }

        /// <summary>
        /// Determines whether the specified value is equal to the current <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(float value)
        {
            return (x == value && y == value && z == value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector3"/> is equal to the current <see cref="Vector3"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector3) || obj == null)
                return false;
            else return Equals(this, (Vector3)obj);
        }
    }
}