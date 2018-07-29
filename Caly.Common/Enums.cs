using System;

namespace Caly.Common.Enums
{
    public class EnumTable<TEnum>
        where TEnum : struct
    {
        public TEnum Id { get; set; }
        public string Name { get; set; }

        protected EnumTable() { }

        public EnumTable(TEnum enumType)
        {
            ExceptionHelpers.ThrowIfNotEnum<TEnum>();

            Id = enumType;
            Name = enumType.ToString();
        }

        public static implicit operator EnumTable<TEnum>(TEnum enumType) => new EnumTable<TEnum>(enumType);
        public static implicit operator TEnum(EnumTable<TEnum> status) => status.Id;
    }

    static class ExceptionHelpers
    {
        public static void ThrowIfNotEnum<TEnum>()
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception($"Invalid generic method argument of type {typeof(TEnum)}");
            }
        }
    }
}
