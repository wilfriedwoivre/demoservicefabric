using System.Runtime.Serialization;

namespace CalculatorHistory.Model
{
    [DataContract]
    public class Operation
    {
        [DataMember]
        public int LeftValue { get; set; }

        [DataMember]
        public int RightValue { get; set; }

        [DataMember]
        public Operand Operand { get; set; }
    }
}