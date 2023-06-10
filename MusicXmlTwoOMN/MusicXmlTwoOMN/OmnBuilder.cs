using System.Text;

namespace MusicXmlTwoOMN
{
    public class OmnBuilder
    {
        private Reader _reader;
        private static Dictionary<MeasureInterpretationCycle, Func<HorizontalContent, string>> _toOmn = new Dictionary<MeasureInterpretationCycle, Func<HorizontalContent, string>>()
        {
            { MeasureInterpretationCycle.StartBarLine, x => StartBarLineToOmn(x) },
            { MeasureInterpretationCycle.EndBarLine, x => EndBarLineToOmn(x) },
            { MeasureInterpretationCycle.Length, x => LengthToOmn(x) },
            { MeasureInterpretationCycle.Pitch, x => PitchToOmn(x) },
        };

        public OmnBuilder(Reader reader)
        {
            _reader = reader;
            reader.Read();
        }

        public List<string> BuildStaves()
        {
            var stavesBuilder = InitializeStavesBuilder();
            BuildStaves(stavesBuilder);
            FinishStavesBuilder(stavesBuilder);

            return stavesBuilder.Select(x => x.Value.ToString()).ToList();
        }

        private void BuildStaves(Dictionary<HorizontalContent, StringBuilder> stavesBuilder)
        {
            while (_reader.Next())
            {
                foreach (var staff in _reader.VerticalContent.Staves)
                {
                    StaffCurrentPositionToOmn(stavesBuilder, staff);
                }
            }
        }

        private void FinishStavesBuilder(Dictionary<HorizontalContent, StringBuilder> stavesBuilder)
        {
            foreach (var staff in  stavesBuilder)
            {
                staff.Value.Append(")");
            }
        }

        private static string StartBarLineToOmn(HorizontalContent horizontalContent)
        {
            return "(";
        }

        private static string EndBarLineToOmn(HorizontalContent horizontalContent)
        {
            return ")";
        }

        private static string LengthToOmn(HorizontalContent horizontalContent)
        {
            return horizontalContent.CurrentMeasureElement.ToOmnLength();
        }

        private static string PitchToOmn(HorizontalContent horizontalContent)
        {
            return horizontalContent.CurrentMeasureElement.ToOmnPitch();
        }

        private void StaffCurrentPositionToOmn(Dictionary<HorizontalContent, StringBuilder> stavesBuilder, HorizontalContent staff)
        {
            var staffBuilder = stavesBuilder[staff];
            switch (staff.CycleStatus)
            {
                // not implemented yet
                case MeasureInterpretationCycle.TimeSignature:
                case MeasureInterpretationCycle.Velocity:
                case MeasureInterpretationCycle.Attribute:
                case MeasureInterpretationCycle.NotInitialized:
                    return;
            }
            var omn = _toOmn[staff.CycleStatus].Invoke(staff);
            staffBuilder.Append(omn);
        }

        private Dictionary<HorizontalContent, StringBuilder> InitializeStavesBuilder()
        {
            var stavesBuilder = new Dictionary<HorizontalContent, StringBuilder>();
            foreach (var horizontalContent in _reader.VerticalContent.Staves)
            {
                stavesBuilder.Add(horizontalContent, new StringBuilder("'("));
            }

            return stavesBuilder;
        }
    }
}
