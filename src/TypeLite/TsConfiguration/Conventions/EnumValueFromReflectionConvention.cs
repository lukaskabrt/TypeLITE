using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeLite.TsConfiguration.Conventions
{
    public class EnumValueFromReflectionConvention : IEnumValueConvention {
        public TsEnumValueConfiguration Apply(FieldInfo enumValue) {
            var result = new TsEnumValueConfiguration();

            var fieldValue = enumValue.GetValue(null);
            var valueType = Enum.GetUnderlyingType(fieldValue.GetType());

            if (valueType == typeof(byte)) {
                result.Value = ((byte)fieldValue).ToString();
            }
            if (valueType == typeof(sbyte)) {
                result.Value = ((sbyte)fieldValue).ToString();
            }
            if (valueType == typeof(short)) {
                result.Value = ((short)fieldValue).ToString();
            }
            if (valueType == typeof(ushort)) {
                result.Value = ((ushort)fieldValue).ToString();
            }
            if (valueType == typeof(int)) {
                result.Value = ((int)fieldValue).ToString();
            }
            if (valueType == typeof(uint)) {
                result.Value = ((uint)fieldValue).ToString();
            }
            if (valueType == typeof(long)) {
                result.Value = ((long)fieldValue).ToString();
            }
            if (valueType == typeof(ulong)) {
                result.Value = ((ulong)fieldValue).ToString();
            }

            return result;
        }
    }
}
