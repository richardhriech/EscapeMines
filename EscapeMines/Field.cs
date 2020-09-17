using Common;

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
    }
}
