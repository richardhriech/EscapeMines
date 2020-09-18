using Common;
using Common.Enums;

namespace EscapeMines
{
    public class Field
    {
        public Field(Position position, FieldType fieldType)
        {
            Position = position;
            FieldType = fieldType;

        }

        public Position Position { get; set; }

        public FieldType FieldType { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Field otherField 
                && otherField.Position == Position 
                && otherField.FieldType == FieldType;
        }
    }
}
