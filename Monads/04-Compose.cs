using NUnit.Framework;
using System;

namespace Monads
{
    public static class ComposeExtensions
    {

        public static Nullable<TOut> ApplySpecial<TIn, TOut>(this Nullable<TIn> value, Func<TIn, Nullable<TOut>> function) where TOut : struct where TIn : struct
        {
            return value.HasValue ? function(value.Value) : new Nullable<TOut>();
        }

        public static Func<X, Nullable<Z>> Compose<X, Y, Z>(Func<X, Nullable<Y>> f, Func<Y, Nullable<Z>> g) where X : struct where Y : struct where Z : struct
        {
            throw new InvalidOperationException("Implement me");
        }
    }

    [TestFixture]
    public class ComposeTests
    {
        private readonly Func<double, double?> log = i => i > 0 ? new Nullable<double>(Math.Log(i)) : new Nullable<double>();
        private readonly Func<double, decimal?> toDecimal = i => Math.Abs(i) < (double)decimal.MaxValue ? new Nullable<decimal>((decimal)i) : new Nullable<decimal>();

        [Test]
        public void MonadsShouldBeComposable()
        {
            var value = 50;
            var a = log(value);
            var b = toDecimal(a.Value);
            var logInDecimal = ComposeExtensions.Compose(log, toDecimal);
            var c = logInDecimal(value);
            Assert.AreEqual(b, c);
        }

        [Test]
        public void CrashOnNull()
        {
            var value = 0;
            var a = log(value);
            Assert.Throws<InvalidOperationException>(() => toDecimal(a.Value));
        }

        [Test]
        public void AvoidException()
        {
            var value = 0;
            var logInDecimal = ComposeExtensions.Compose(log, toDecimal);
            var a = logInDecimal(value);
            Assert.IsFalse(a.HasValue);
        }
    }
}