using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Monads
{
    public static class GenericExtensions
    {
        public static Nullable<T> CreateNullable<T>(this T value) where T:struct
        {
            return new Nullable<T>(value);
        }
        public static IEnumerable<T> CreateEnumerable<T>(this T value)
        {
            throw new NotImplementedException("implement me");
        }

        public static Func<T> CreateOnDemand<T>(this T value)
        {
            throw new NotImplementedException("implement me");
        }

        public static Lazy<T> CreateLazy<T>(this T value)
        {
            throw new NotImplementedException("implement me");
        }

        public static Task<T> CreateTask<T>(this T value)
        {
            throw new NotImplementedException("implement me");
        }
    }

    [TestFixture]
    public class WrappingValues
    {
        [Test]
        public void CreatingMonadLikeValues() 
        {
            var nullable = 1.CreateNullable();
            var enumerable = 2.CreateEnumerable();
            var onDemand = 3.CreateOnDemand();
            var lazy = 4.CreateLazy();
            var task = 5.CreateTask();

            Assert.Multiple(()=>
            {
                Assert.True(nullable.HasValue, "Nullable has value");
                CollectionAssert.IsNotEmpty(enumerable, "Enumerable has values");
                Assert.DoesNotThrow(() => onDemand(), "Ondemand does not error");
                Assert.AreEqual(4, lazy.Value, "Lazy returns value");
                Assert.IsFalse(task.IsFaulted, "Task didn't fail");
            });
        }
    }
}
