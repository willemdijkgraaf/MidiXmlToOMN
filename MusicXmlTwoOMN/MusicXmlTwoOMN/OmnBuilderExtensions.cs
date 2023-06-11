using MusicXml.Domain;

namespace MusicXmlTwoOMN
{
    public static class OmnBuilderExtensions
    {
        private static Dictionary<int, string> _accidentals = new Dictionary<int, string>()
        {
            {-2, "bb" },
            {-1, "b" },
            {0, "" },
            {+1, "s" },
            {+2, "ss" },
        };

        public static string ToOmnLength (this MeasureElement measureElement, int divisions, int beats)
        {
            var measureLength = divisions * beats;

            var _lengths = new Dictionary<int, string>()
            {
                { 1, "w "},
                { 2, "h "},
                { 4, "q "},
                { 8, "e "},
                { 16,"s "},
                { 32,"t "},
                { 64,"x "},
                { 128,"u "}
            };

            var omn = "";
            switch (measureElement.Type)
            {
                case MeasureElementType.Note:var note = measureElement.Element as Note;
                    var restAsOnm = note.IsRest ? "-":"";
                    var duration = note.Duration;
                    var length = measureLength / duration;
                    omn = $"{restAsOnm}{_lengths[length]}";
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
                    if (note != null && !note.IsRest) 
                    {
                        omn = $"{note.Pitch.Step}{note.Pitch.ToOmnAccidental()}{note.Pitch.Octave} ".ToLower();
                    }
                    break;
            }
            return omn;
        }

        public static string ToOmnAccidental(this Pitch pitch)
        {
            return _accidentals[pitch.Alter];
        }
    }
}
