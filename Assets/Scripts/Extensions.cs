using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static void SetAll<T>(this IList<T> self, T value)
    {
        for (int i = 0; i < self.Count; ++i)
        {
            self[i] = value;
        }
    }

    public static void AddAll<T>(this List<T> self, IList<T> other)
    {
        self.Capacity = Math.Max(self.Capacity, self.Count + other.Count);
        foreach (var o in other)
        {
            self.Add(o);
        }
    }

    public static void Shuffle<T>(this IList<T> self)
    {
        T temp;
        for (int i = 0; i < self.Count; ++i)
        {
            temp = self[i];
            int idx = (int)(UnityEngine.Random.value * self.Count);
            self[i] = self[idx];
            self[idx] = temp;
        }
    }

    public static void RandomFill<T>(this IList<T> self, T[] target)
    {
        HashSet<int> taken = new HashSet<int>();
        for (int i = 0; i < target.Length; ++i)
        {
            int idx;
            do
            {
                idx = (int)(UnityEngine.Random.value * self.Count);
            } while (taken.Contains(idx) && taken.Count != self.Count);
            
            target[i] = self[idx];
        }
    }

    public static T Random<T>(this IEnumerable<T> self)
    {
        return Random(self.ToArray());
    }

    public static T Random<T>(this IList<T> self)
    {
        if (self.Count > 0)
        {
            return self[(int)(UnityEngine.Random.value * self.Count)];
        }

        return default(T);
    }

    public static Vector2 Div(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x / b.x, a.y / b.y);
    }

    public static Vector2 Mul(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public static Vector3 Mul(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector3 Inv(this Vector3 a)
    {
        return new Vector3(1 / a.x, 1 / a.y, 1 / a.z);
    }

    public static Rect Lerp(this Rect source, Rect target, float t)
    {
        var output = new Rect();
        output.position = Vector2.Lerp(source.position, target.position, t);
        output.size = Vector2.Lerp(source.size, target.size, t);
        return output;
    }

    public static void ForEach<T>(this IEnumerable<T> self, Action<T> todo)
    {
        foreach (var i in self)
        {
            todo(i);
        }
    }

    public static T Max<T>(this IEnumerable<T> self, Func<T, decimal> comparer)
    {
        T max = self.First();
        decimal val = comparer(max);
        foreach (T t in self)
        {
            decimal v = comparer(t);
            if (val < v || (val == v && UnityEngine.Random.value > .5f))
            {
                val = v;
                max = t;
            }
        }

        return max;
    }

    public static int Wrap(this int self, int max)
    {
        if (self >= 0)
        {
            return self % max;
        }

        return (self + -(self / max) * max + max) % max;
    }

    public static int IndexOf<T>(this T[] self, Predicate<T> pred)
    {
        for (int i = 0; i < self.Length; ++i)
        {
            if (pred(self[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public static Vector2 MapToRectangle(this Vector2 del, Vector2 widthAndHeight)
    {
        const float root2over2 = 0.707106781f;
        del = del.normalized;
        if (Mathf.Abs(del.x) > Mathf.Abs(del.y))
        {
            // Left-right
            return new Vector2(Mathf.Sign(del.x) * widthAndHeight.x, del.y * widthAndHeight.y / root2over2);
        }
        else
        {
            // Up-down
            return new Vector2(del.x * widthAndHeight.x / root2over2, Mathf.Sign(del.y) * widthAndHeight.y);
        }
    }

    public static float AngleSignedRad(this Vector2 vector1, Vector2 vector2)
    {
        vector1.Normalize();
        vector2.Normalize();
        return (Mathf.Atan2(vector2.y, vector2.x) - Mathf.Atan2(vector1.y, vector1.x));
    }

    public static float AngleSigned(this Vector2 vector1, Vector2 vector2)
    {
        return AngleSignedRad(vector1, vector2) * Mathf.Rad2Deg;
    }

    public static Vector2 Rotate(this Vector2 self, float degrees)
    {
        var rads = Mathf.Deg2Rad * degrees;
        return new Vector2(
            self.x * Mathf.Cos(rads) - Mathf.Sin(rads) * self.y,
            self.x * Mathf.Sin(rads) + Mathf.Cos(rads) * self.y);
    }

    public static Vector3 RotateY(this Vector3 self, float degrees)
    {
        var rads = Mathf.Deg2Rad * degrees;
        return new Vector3(
            self.x * Mathf.Cos(rads) - Mathf.Sin(rads) * self.z,
            self.y,
            self.x * Mathf.Sin(rads) + Mathf.Cos(rads) * self.z);
    }

    public static bool TryToEnum<TEnum>(this string strEnumValue, out TEnum enumValue)
    {
        enumValue = default(TEnum);

        if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
            return false;

        enumValue = (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        return true;
    }

    public static TEnum ToEnum<TEnum>(this string strEnumValue)
    {
        if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
            return default(TEnum);

        return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
    }

    public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
    {
        if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
            return defaultValue;

        return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
    }

    public static bool IsNullOrWhiteSpace(this string s)
    {
        if (s == null) return true;
        if (s.Any(c => !Char.IsWhiteSpace(c))) return false;
        return true;
    }
}