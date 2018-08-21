using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Caly.Common
{
    public static class DBContextExtensions
    {
        public static void DeleteAll<T>(this DbContext context)
        where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }
        }

        public static OperationResult CompareInSet<T>(this T item, DbContext context, Func<T, object> returnProp, Func<T, object> identifier, params Func<T, object>[] props) where T:class
        {
            var set = context.Set<T>();

            var result = new OperationResult();

            foreach(var theother in set)
            {
                if (identifier(item) == identifier(theother)) continue;

                if (Compare(item, theother, props))
                {
                    result.Observations.Add(returnProp(theother));
                }
            }

            result.Result = result.Observations.Count == 0;

            return result;
        }

        private static bool Compare<T>(T one, T theother, params Func<T, object>[] props) where T:class
        {
            for (var i = 0; i < props.Length; i++)
            {
                if (props[i](one) == props[i](theother)) return false;
            }
            return true;
        }

        // trying to gener=ically get a db set, compare its entites with a given item by an array of props (funcs). goodluck motherfucker
    }
}
