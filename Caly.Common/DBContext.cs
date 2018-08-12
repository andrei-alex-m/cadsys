using System;
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
    }
}
