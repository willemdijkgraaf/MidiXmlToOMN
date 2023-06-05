namespace MusicXmlTwoOMN
{
    public enum MeasureInterpretationCycle 
    { 
        NotInitialized = 0,
        StartBarLine = 1,
        TimeSignature = 2,
        Length = 3,
        Pitch = 4,
        Velocity = 5,
        Attribute = 6,
        EndBarLine = 7,
        End = 8
    }
}