using System;
using System.ComponentModel;
using System.Globalization;
using ExampleApp.Models;

namespace ExampleApp.Infrastructure {
    public class NumbersTypeConverter : TypeConverter {

        public override bool CanConvertFrom(ITypeDescriptorContext context,
                Type sourceType) {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                CultureInfo culture, object value) {

            string valueToParse = value as string;
            string[] elements = null;
            if (valueToParse != null
                    && (elements = valueToParse.Split(',')).Length == 4) {

                int firstVal, secondVal; bool addVal, doubleVal;
                if (int.TryParse(elements[0], out firstVal)
                     && int.TryParse(elements[1], out secondVal)
                     && bool.TryParse(elements[2], out addVal)
                     && bool.TryParse(elements[3], out doubleVal)) {

                    return new Numbers(firstVal, secondVal) {
                        Op = new Operation {
                            Add = addVal,
                            Double = doubleVal
                        }
                    };
                }
            }
            return null;
        }
    }
}
