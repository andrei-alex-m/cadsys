using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Caly.Common
{
    public static class Reflection
    {
        static string[] dateFormats = 
        {
            "dd/MM/yyyy",
            "d/M/yyyy",
            "d/MM/yyyy",
            "dd/M/yyyy",
            "dd.MM.yyyy",
            "d.M.yyyy",
            "d.MM.yyyy",
            "dd.M.yyyy"
        };

        public static void FillInstanceFromDictionary(Dictionary<string, string> keyValues, Object instance, bool fieldNameCaseIgnore = true)
        {
            if (keyValues == null || keyValues.Count == 0) return;
            if (instance == null) return;

            Type t = instance.GetType();

            System.Reflection.BindingFlags flags=BindingFlags.Public | BindingFlags.Instance;

            if (fieldNameCaseIgnore) flags = flags | System.Reflection.BindingFlags.IgnoreCase;



            foreach(var kvp in keyValues)
            {
                var info = t.GetProperty(kvp.Key, flags);

                if (info != null)
                {
                    try
                    {
                        if (info.PropertyType == typeof(DateTime))
                        {
                            info.SetValue(instance, DateTime.ParseExact(kvp.Value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None));
                        }
                        else
                        {
                            info.SetValue(instance, Convert.ChangeType(kvp.Value, info.PropertyType));
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                }
            };
        }
    }
}
