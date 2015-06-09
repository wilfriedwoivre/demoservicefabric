using System.Runtime.Serialization;

namespace CalculatorHistory.Model
{
    [DataContract]
    public enum Operand
    {
        [EnumMember]
        Add,
        [EnumMember]
        Substract
    }
}