using NUnit.Framework;
using System;

namespace Monads.Answers
{
    public static class ApplySpecialExtensions
    {
       public static Func<TOut> ApplySpecial<TIn, TOut>(this Func<TIn> value, Func<TIn, Func<TOut>> function){
           return () => {
                var unwrapped = value();
                var result = function(unwrapped);
                return result();
           };
       }

       public static Func<TOut> ApplySpecial<TIn, TOut>(this TIn value, Func<TIn, Func<TOut>> function){
            return value.CreateOnDemand().ApplySpecial(function);
       }
    }

}