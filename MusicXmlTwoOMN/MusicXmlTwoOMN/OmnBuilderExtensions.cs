using MusicXml.Domain;

namespace MusicXmlTwoOMN
{
    public static class OmnBuilderExtensions
    {
        public static string ToOmnLength (this MeasureElement measureElement)
        {
            var omn = "";
            switch (measureElement.Type)
            {
                case MeasureElementType.Note:
                    var durations = new Dictionary<int, string>() 
                    {
                        {1, "q " },
                        {2, "h " },
                        {4, "w " }
                    };
                    
                    var note = measureElement.Element as Note;
                    omn = durations[note.Duration];
                    break;
            }
            return omn;
        }

        public static string ToOmnPitch(this MeasureElement measureElement)
        {
            var omn = "";
            switch (measureElement.Type)
            {
                case MeasureElementType.Note:
                    var note = measureElement.Element as Note;
                    if (note.Pitch != null) 
                    {
                        omn = $"{note.Pitch.Step}{note.Pitch.ToOmnAccidental()}{note.Pitch.Octave}".ToLower();
                    }
                    break;
            }
            return omn;
        }

        public static string ToOmnAccidental(this Pitch pitch)
        {
            var accidentals = new Dictionary<int, string>()
                    {
                        {-2, "bb" },
                        {-1, "b" },
                        {0, "" },
                        {+1, "s" },
                        {+2, "ss" },
                    };
            
            return accidentals[pitch.Alter];
        }
    }
}
