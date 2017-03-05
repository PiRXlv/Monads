using NUnit.Framework;
using System;

namespace Monads
{
    public static class ApplySpecialExtensions
    {
        public static Func<TOut> ApplySpecial<TIn, TOut>(this Func<TIn> value, Func<TIn, Func<TOut>> function)
        {
            return () =>
            {
                var unwrapped = value();
                var result = function(unwrapped);
                return result();
            };
        }
    }

    public class ApplySpecial
    {
        [Test]
        public void DoubleAmplifier_ShouldGetFolded()
        {
            var expected = 'F';
            var value = 5.CreateOnDemand();
            var function = new Func<int, Func<char>>(i => () => (char)('A' + i));
            var actual = value.ApplySpecial(function);
            Assert.AreEqual(expected, actual());
        }
    }

}