using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace RandomLib
{
    public static class RandomUtility
    {
        const string Alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        const string Alphanumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const string Symbols = @" !""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
        const string NonControlChars = @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        static readonly Random random = new Random();
        static readonly RandomNumberGenerator generator;

        static RandomUtility()
        {
            generator = RandomNumberGenerator.Create();
            AppDomain.CurrentDomain.ProcessExit += (o, e) =>
            {
                generator.Dispose();
            };
        }

        public static string GenerateAlphabets(int length)
        {
            return new string(
                Enumerable.Repeat(false, length)
                    .Select(_ => Alphabets[random.Next(Alphabets.Length)])
                    .ToArray());
        }

        public static string GenerateAlphanumerics(int length)
        {
            return new string(
                Enumerable.Repeat(false, length)
                    .Select(_ => Alphanumerics[random.Next(Alphanumerics.Length)])
                    .ToArray());
        }

        public static byte[] GenerateBytes(int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", length, "The value must be non-negative.");

            var data = new byte[length];
            generator.GetBytes(data);
            return data;
        }

        public static GuidWithDateTime GenerateOrderedGuid()
        {
            var dt = DateTime.UtcNow;
            var ticks = dt.Ticks;

            var a = (int)(ticks >> 32);
            var b = (short)(ticks >> 16);
            var c = (short)ticks;
            var d = GenerateBytes(8);

            return new GuidWithDateTime
            {
                Guid = new Guid(a, b, c, d),
                DateTime = dt,
            };
        }

        public static GuidWithDateTime GenerateOrderedGuid2()
        {
            var dt = DateTime.UtcNow;
            var ticks = dt.Ticks;

            var a = (int)(ticks >> 28);
            var b = (short)(ticks >> 12);
            var r = GenerateBytes(2);
            var c = (short)((r[0] << 8) | r[1]);
            var d = GenerateBytes(8);

            return new GuidWithDateTime
            {
                Guid = new Guid(a, b, c, d),
                DateTime = dt,
            };
        }

        public static GuidWithDateTime GenerateOrderedSqlGuid()
        {
            var dt = DateTime.UtcNow;

            var a = GenerateBytes(8);
            var b = dt.ToBytesForSqlGuid();

            return new GuidWithDateTime
            {
                Guid = new Guid(a.Concat(b).ToArray()),
                DateTime = dt,
            };
        }

        public static GuidWithDateTime GenerateOrderedSqlGuid2()
        {
            var dt = DateTime.UtcNow;

            var a = GenerateBytes(10);
            var b = dt.ToBytes2();

            return new GuidWithDateTime
            {
                Guid = new Guid(a.Concat(b).ToArray()),
                DateTime = dt,
            };
        }

        public static string ToHexString(this byte[] data, bool uppercase)
        {
            if (data == null) throw new ArgumentNullException("data");

            var format = uppercase ? "X2" : "x2";
            return data
                .Select(b => b.ToString(format))
                .ConcatStrings();
        }

        static string ConcatStrings(this IEnumerable<string> source)
        {
            return string.Concat(source);
        }

        public static string ToIso8601String(this DateTime dateTime)
        {
            return dateTime.ToString("O", CultureInfo.InvariantCulture);
        }

        public static byte[] ToBytes(this DateTime dateTime)
        {
            var ticks = dateTime.Ticks;

            var b = new byte[8];
            b[0] = (byte)(ticks >> 56);
            b[1] = (byte)(ticks >> 48);
            b[2] = (byte)(ticks >> 40);
            b[3] = (byte)(ticks >> 32);
            b[4] = (byte)(ticks >> 24);
            b[5] = (byte)(ticks >> 16);
            b[6] = (byte)(ticks >> 8);
            b[7] = (byte)ticks;
            return b;
        }

        public static byte[] ToBytesForSqlGuid(this DateTime dateTime)
        {
            var ticks = dateTime.Ticks;

            var b = new byte[8];
            b[0] = (byte)(ticks >> 8);
            b[1] = (byte)ticks;
            b[2] = (byte)(ticks >> 56);
            b[3] = (byte)(ticks >> 48);
            b[4] = (byte)(ticks >> 40);
            b[5] = (byte)(ticks >> 32);
            b[6] = (byte)(ticks >> 24);
            b[7] = (byte)(ticks >> 16);
            return b;
        }

        public static byte[] ToBytes2(this DateTime dateTime)
        {
            var ticks = dateTime.Ticks;

            var b = new byte[6];
            b[0] = (byte)(ticks >> 52);
            b[1] = (byte)(ticks >> 44);
            b[2] = (byte)(ticks >> 36);
            b[3] = (byte)(ticks >> 28);
            b[4] = (byte)(ticks >> 20);
            b[5] = (byte)(ticks >> 12);
            return b;
        }
    }

    [DebuggerDisplay("{Guid}")]
    public struct GuidWithDateTime
    {
        public Guid Guid;
        public DateTime DateTime;
    }
}
