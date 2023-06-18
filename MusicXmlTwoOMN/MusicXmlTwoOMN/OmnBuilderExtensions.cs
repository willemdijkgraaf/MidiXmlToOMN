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
            var _lengths = new Dictionary<string, string>()
            {
                { "whole", "w"},
                { "half", "h"},
                { "quarter", "q"},
                { "eighth", "e"},
                { "16th","s"},
                { "32nd","t"},
                { "64th","x"},
                { "128th","u"}
            };

            var omn = "";
            switch (measureElement.Type)
            {
                case MeasureElementType.Note:
                    if (measureElement.Element is not Note note) break;
                    var restAsOnm = note.IsRest ? "-":"";
                    var duration = note.Duration;
                    var dots = "";
                    var remainingDuration = duration;
                    var remainingDivision = divisions;
                    while (remainingDuration > 1 && remainingDuration % remainingDivision != 0)
                    {
                        remainingDuration = remainingDuration % divisions;
                        remainingDivision = remainingDivision / 2;
                        dots = $"{dots}.";
                    }
                    
                    omn = $"{restAsOnm}{_lengths[note.Type]}{dots} ";
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
