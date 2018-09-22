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

        public static readonly Dictionary<string, string> matches = new Dictionary<string, string>()
        {
            {"CNP/CUI", "IDENTIFICATOR"},
            {"IDENTIFICATOR", "CNP/CUI"},
            {"INDEXPARCELA", "INDEX"},
            {"INDEXACT","INDEX"},
            {"INDEXPROPRIETAR","INDEX"}
        };

        public static void FillInstanceFromDictionary(Dictionary<string, string> keyValues, Object instance, bool fieldNameCaseIgnore = true)
        {
            if (keyValues == null || keyValues.Count == 0) return;
            if (instance == null) return;

            Type t = instance.GetType();

            BindingFlags flags =BindingFlags.Public | BindingFlags.Instance;

            if (fieldNameCaseIgnore)
            {
                flags = flags | BindingFlags.IgnoreCase;
            }

            foreach (var kvp in keyValues)
            {
                var info = t.GetProperty(kvp.Key, flags);

                if (info == null && matches.ContainsKey(kvp.Key))
                {
                    info = t.GetProperty(matches[kvp.Key], flags);
                }

                if (info != null)
                {
                    try
                    {
                        var type = info.PropertyType.IsGenericType
                           && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                           ? Nullable.GetUnderlyingType(info.PropertyType) : info.PropertyType;

                        if (string.IsNullOrEmpty(kvp.Value))
                        {
                            info.SetValue(instance, null);
                            continue;
                        }
                        
                        if (type == typeof(DateTime))
                        {
                            info.SetValue(instance, DateTime.ParseExact(kvp.Value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None));
                        }
                        else
                        {
                            info.SetValue(instance, Convert.ChangeType(kvp.Value, type));
                        }
                    }
                    catch(Exception ex)
                    {
                        info.SetValue(instance, null);
                    }

                }
            };
        }

        public static void FillDictionaryFromInstance(Dictionary<string, string> keyValues, Object instance, bool fieldNameCaseIgnore = true)
        {
            Type t = instance.GetType();

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            if (fieldNameCaseIgnore)
            {
                flags = flags | BindingFlags.IgnoreCase;
            }

            foreach (var info in t.GetProperties(flags))
            {
                var exportPropName = info.Name.ToUpper();
                if (matches.ContainsKey(exportPropName))
                {
                    exportPropName = matches[exportPropName];
                }

                var val = info.GetValue(instance);

                if (val!=null)
                {
                    var type = info.PropertyType.IsGenericType
                           && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                           ? Nullable.GetUnderlyingType(info.PropertyType) : info.PropertyType;

                    if (type == typeof(DateTime))
                    {
                        keyValues.Add(exportPropName, ((DateTime)val).ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        keyValues.Add(exportPropName, val.ToString());
                    }
                }
            }

        }
    }
}
