using NUnit.Framework;
using System;

namespace Monads.Answers
{
    public static class ApplyExtensions{
        public static Nullable<int> AddOne(this Nullable<int> value){
            if (value.HasValue){
                var unwrapped = value.Value;
                var result = unwrapped + 1;
                return new Nullable<int>(result);
            }

            return new Nullable<int>();
        }

             public static Func<int> AddOne(this Func<int> value){
            return () => {

                        var unwrapped = value();
            var result = unwrapped + 1;
            return result;
            };
        }
    }

        public static class ApplyFunctionExtension{
        public static Nullable<int> ApplyFunction(this Nullable<int> value, Func<int, Nullable<int>> function){
            if (value.HasValue){
                var unwrapped = value.Value;
                var result = function(unwrapped);
                return result;
            }

            return new Nullable<int>();
        }

        public static Func<int> ApplyFunction(this Func<int> value, Func<int, int> function){
                    return () => {

                        var unwrapped = value();
            var result = function(unwrapped);
            return result;
            };
        }

        public static Func<TOut> ApplyFunction<TIn, TOut>(this Func<TIn> value, Func<TIn, TOut> function){
                               return () => {

                        var unwrapped = value();
            var result = function(unwrapped);
            return result;
            };
        }
    }
}