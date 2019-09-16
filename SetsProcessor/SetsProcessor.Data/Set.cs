using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SetsProcessor.Domain
{
    public sealed class Set : IEquatable<Set>, IEnumerable<int>
    {

        private readonly int[] data;
        public int this[int index]
        {
            get { return data[index]; }
        }

        public Set(params int[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            this.data = (int[])data.Clone();
        }

        private int? hash;

        public override int GetHashCode()
        {
            if (hash == null)
            {
                int result = 13;
                for (int i = 0; i < data.Length; i++)
                {
                    result = (result * 7) + data[i];
                }
                hash = result;
            }
            return hash.GetValueOrDefault();
        }

        public int Length { get { return data.Length; } }


        public bool Equals(Set obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            return this == (obj as Set);
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < data.Length; i++)
                yield return data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Devuelve el Set formateado con coma
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (data != null && data.Length > 0)
            {
                int length = data.Length - 1;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < length; i++)
                    sb.Append(data[i] + ",");
                sb.Append(data[length]);
                return sb.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Comparador de Sets. Se emplea Parallel ya que, en grandes cantidades de datos, ofrece mejor rendimiento
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(Set x, Set y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            if (x.hash.HasValue && y.hash.HasValue && // exploit known different hash
                x.hash.GetValueOrDefault() != y.hash.GetValueOrDefault()) return false;
            int[] xdata = x.data, ydata = y.data;

            var areEqual = true;

            if (xdata.Length > 0 && xdata.Length == ydata.Length)
            {
                Parallel.ForEach(xdata,
                    (i, s, z) =>
                    {
                        if (xdata[z] != ydata[z])
                        {
                            areEqual = false;
                            s.Stop();
                        }
                    });
            }
            else
                areEqual = false;

            return areEqual;
        }

        /// <summary>
        /// Devuelve si dos sets son diferentes
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(Set x, Set y)
        {
            return !(x == y);
        }
    }
}
