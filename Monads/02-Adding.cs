using NUnit.Framework;
using System;

namespace Monads
{
    public static class ApplyExtensions
    {
        public static Nullable<int> AddOne(this Nullable<int> value)
        {
            if (value.HasValue)
            {
                var unwrapped = value.Value;
                var result = unwrapped + 1;
                return new Nullable<int>(result);
            }

            return new Nullable<int>();
        }

        public static Func<int> AddOne(this Func<int> value)
        {
            throw new NotImplementedException("implement me");
        }
    }

    public static class ApplyFunctionExtension
    {
        public static Nullable<int> ApplyFunction(this Nullable<int> value, Func<int, Nullable<int>> function)
        {
            throw new NotImplementedException("implement me");
        }

        public static Func<int> ApplyFunction(this Func<int> value, Func<int, int> function)
        {
            throw new NotImplementedException("implement me");
        }

        public static Func<TOut> ApplyFunction<TIn, TOut>(this Func<TIn> value, Func<TIn, TOut> function)
        {
            throw new NotImplementedException("implement me");
        }
    }

    [TestFixture]
    public class AddingOne
    {

        [Test]
        public void AddingToNullableWithValue_CreatesNullableWithValue()
        {
            var expected = 3;
            var actual = 2.CreateNullable().AddOne();
            Assert.Multiple(() =>
            {
                Assert.True(actual.HasValue);
                Assert.AreEqual(expected, actual.Value);
            });
        }

        [Test]
        public void AddingToEmptyNullable_CreatesEmptyNullable()
        {
            var actual = new Nullable<int>().AddOne();
            Assert.False(actual.HasValue);
        }

        [Test]
        public void AddingToOnDemand_CreatesNewOnDemand()
        {
            var expected = 3;
            var onDemand = 2.CreateOnDemand().AddOne();
            var actual = onDemand();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddingToOnDemand_DoesntExecuteOriginalOnDemand()
        {
            var original = new Func<int>(() => { throw new InvalidOperationException("Got ya!"); });
            var onDemand = original.AddOne(); // shouldn't throw exception
            Assert.Throws<InvalidOperationException>(() => onDemand());
        }

        [Test]
        public void ApplyingFunctionToNullable_CreatesNullableWithValue()
        {
            var expected = 7;
            var actual = 2.CreateNullable().ApplyFunction(i => i + 5);
            Assert.Multiple(() =>
            {
                Assert.True(actual.HasValue);
                Assert.AreEqual(expected, actual.Value);
            });
        }

        [Test]
        public void ApplyingFunctionOnDemand_CreatesNewOnDemand()
        {
            var expected = 7;
            var onDemand = 2.CreateOnDemand().ApplyFunction(i => i + 5);
            var actual = onDemand();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ApplyFunction_CanTransformResult()
        {
            var expected = "CC";
            var onDemand = 2.CreateOnDemand().ApplyFunction(i => new string((char)('A' + i), i));
            var actual = onDemand();
            Assert.AreEqual(expected, actual);
        }
    }
}