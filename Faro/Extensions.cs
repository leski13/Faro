using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Faro
{
    public static class Extensions
    {
        public static IEnumerable<T> InterLeaveSequenceWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while(firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
            }
        }
        public static bool SequenceEquals<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstItem = first.GetEnumerator();
            var secondItem = second.GetEnumerator();

            while(firstItem.MoveNext() && secondItem.MoveNext())
            {
                if (!firstItem.Current.Equals(secondItem.Current))
                {
                    return false;
                }

            }
            return true;
        }
        public static IEnumerable<T>LogQuery<T>(this IEnumerable<T> sequence, string tag)
        {
            using (var writer = File.AppendText("debug.log"))
            {
                writer.WriteLine($"Wxecuting Query {tag}");
            }
            return sequence;
        }
    }
}
