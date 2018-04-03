using System;

namespace LanguageServer.Parameters
{
    public class Position : IEquatable<Position>
    {
        public long Line { get; set; }
        public long Character { get; set; }

        public Position()
        {
            Line = 0;
            Character = 0;
        }

        public Position(long line, long character)
        {
            Line = line;
            Character = character;
        }

        public static bool operator <(Position p1, Position p2)
        {

            return Comparison(p1, p2) < 0;

        }

        public static bool operator >(Position p1, Position p2)
        {

            return Comparison(p1, p2) > 0;

        }

        public static bool operator ==(Position p1, Position p2)
        {

            return Comparison(p1, p2) == 0;

        }

        public static bool operator !=(Position p1, Position p2)
        {

            return Comparison(p1, p2) != 0;

        }

        public override bool Equals(object obj)
        {

            if (!(obj is Position)) return false;

            return this.Character == ((Position)obj).Character && this.Line == ((Position)obj).Line;

        }

        public static bool operator <=(Position p1, Position p2)
        {

            return Comparison(p1, p2) <= 0;

        }

        public static bool operator >=(Position p1, Position p2)
        {

            return Comparison(p1, p2) >= 0;

        }

        public static int Comparison(Position p1, Position p2)
        {
            if (p1.Line < p2.Line)
                return -1;
            else if (p1.Line > p2.Line)
                return 1;

            if (p1.Character < p2.Character)
                return -1;
            else if (p1.Character > p2.Character)
                return 1;

            return 0;

        }

        public bool Equals(Position other)
        {
            return other != null &&
                   Line == other.Line &&
                   Character == other.Character;
        }

        public override int GetHashCode()
        {
            var hashCode = 1927683087;
            hashCode = hashCode * -1521134295 + Line.GetHashCode();
            hashCode = hashCode * -1521134295 + Character.GetHashCode();
            return hashCode;
        }
    }
}