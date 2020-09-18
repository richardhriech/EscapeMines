using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumUtil
    {
        public static T NextEnumValue<T>(this T value) where T : Enum
        {
            List<T> higherValues =
                OrderedEnumValues<T>()
                    .Where(val => Convert.ToInt32(val) > Convert.ToInt32(value))
                    .ToList();

            T nextEnumValue = higherValues.Any() ? higherValues.First() : OrderedEnumValues<T>().First();

            return nextEnumValue;
        }
        
        public static T PreviousEnumValue<T>(this T value) where T : Enum
        {
            List<T> lowerValues =
                OrderedEnumValues<T>()
                    .Where(val => Convert.ToInt32(val) < Convert.ToInt32(value))
                    .ToList();

            T previousValue = lowerValues.Any() ? lowerValues.Last() : OrderedEnumValues<T>().Last();

            return previousValue;
        }

        private static IOrderedEnumerable<T> OrderedEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .OrderBy(val => Convert.ToInt32(val));
        }
    }
}
