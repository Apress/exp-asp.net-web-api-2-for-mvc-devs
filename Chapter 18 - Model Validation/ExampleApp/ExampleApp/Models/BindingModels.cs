using System.Runtime.Serialization;

namespace ExampleApp.Models {

    [DataContract(Namespace = "")]
    public class Numbers {

        public Numbers() { /* do nothing */ }

        public Numbers(int first, int second) {
            First = first; Second = second;
        }

        [DataMember]
        public int First { get; set; }
        [DataMember]
        public int Second { get; set; }
        [DataMember]
        public Operation Op { get; set; }
        public string Accept { get; set; }
    }

    [DataContract(Namespace = "")]
    public class Operation {
        [DataMember]
        public bool Add { get; set; }
        [DataMember]
        public bool Double { get; set; }
    }
}
