namespace LanguageServer.Parameters
{
    public class Range
    {
        public Position Start { get; set; }
        public Position End { get; set; }

        public Range()
        {
            Start = new Position();
            End = new Position();
        }

        public Range(Position start, Position end)
        {
            Start = start;
            End = end;
        }

        public Range(Position pos)
        {
            Start = End = pos;
        }

        public Range(long startLine, long startColumn, long endLine, long endColumn)
        {
            Start = new Position(startLine, startColumn);
            End = new Position(endLine, endColumn);
        }

        public Range(long line, long column)
        {
            Start = End = new Position(line, column);
        }

        public bool Intersects(Position p)
        {
            return p >= Start && p <= End;
        }

        public bool Intersects(Range other)
        {
            return Start <= other.End && other.Start <= End;
        }
    }
}