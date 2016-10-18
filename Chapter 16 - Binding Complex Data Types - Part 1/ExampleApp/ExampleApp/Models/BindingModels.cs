using System.Web.Http.ModelBinding;
using ExampleApp.Infrastructure;
using System.ComponentModel;

namespace ExampleApp.Models {

    //[ModelBinder(BinderType = typeof(NumbersBinder))]
    [TypeConverter(typeof(NumbersTypeConverter))]
    public class Numbers {
        private int first, second;

        public Numbers(int firstVal, int secondVal) {
            first = firstVal; second = secondVal;
        }

        public int First {
            get { return first; }
        }

        public int Second {
            get { return second; }
        }

        public Operation Op { get; set; }
        public string Accept { get; set; }
    }

    public class Operation {
        public bool Add { get; set; }
        public bool Double { get; set; }
    }
}
