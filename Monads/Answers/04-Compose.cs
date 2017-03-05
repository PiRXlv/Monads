using NUnit.Framework;
using System;

namespace Monads.Answers
{
    public static class ComposeExtensions
    {

     public static Nullable<TOut> ApplySpecial<TIn, TOut>(this Nullable<TIn> value, Func<TIn, Nullable<TOut>> function) where TOut:struct where TIn: struct {
           return value.HasValue ? function(value.Value) : new Nullable<TOut>();
       }
       
       public static Func<X, Nullable<Z>> Compose<X, Y, Z>(Func<X, Nullable<Y>> f, Func<Y, Nullable<Z>> g) where X:struct where Y:struct where Z:struct{
            return x => ApplySpecial(f(x), g);
       }
    }
}