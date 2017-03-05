using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Monads.Answers
{
    public static class GenericExtensions
    {
        public static Nullable<T> CreateNullable<T>(this T value) where T:struct
        {
            return new Nullable<T>(value);
        }
        public static IEnumerable<T> CreateEnumerable<T>(this T value)
        {
            yield return value;
        }

        public static Func<T> CreateOnDemand<T>(this T value)
        {
            return () => {return value;};
        }

        public static Lazy<T> CreateLazy<T>(this T value)
        {
            return new Lazy<T>(() => value);
        }

        public static Task<T> CreateTask<T>(this T value)
        {
            return new Task<T>(() => value);
        }
    }
}
