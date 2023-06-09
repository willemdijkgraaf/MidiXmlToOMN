using System.Text;

namespace MusicXmlTwoOMN
{
    public class OmnBuilder
    {
        private Reader _reader;

        public OmnBuilder(Reader reader)
        {
            _reader = reader;
            reader.Read();
        }

        public List<string> BuildStaves()
        {
            var stavesBuilder = InitializeStavesBuilder();

            while (_reader.Next())
            {
                foreach (var staff in _reader.VerticalContent.Staves)
                {
                    StaffCurrentPositionToOmn(stavesBuilder, staff);
                }
            }

            FinishStavesBuilder(stavesBuilder);

            return stavesBuilder.Select(x => x.Value.ToString()).ToList();
        }

        private void FinishStavesBuilder(Dictionary<HorizontalContent, StringBuilder> stavesBuilder)
        {
            foreach (var staff in  stavesBuilder)
            {
                // staff.Value.Append(")");
            }
        }

        private void StaffCurrentPositionToOmn(Dictionary<HorizontalContent, StringBuilder> stavesBuilder, HorizontalContent staff)
        {
            var staffBuilder = stavesBuilder[staff];
            var omn = "";
            switch (staff.CycleStatus)
            {
                case MeasureInterpretationCycle.StartBarLine:
                    omn = "(";
                    break;
                case MeasureInterpretationCycle.EndBarLine:
                    omn = ")";
                    break;
                case MeasureInterpretationCycle.Length:
                    omn = staff.CurrentMeasureElement.ToOmnLength();
                    break;
                case MeasureInterpretationCycle.Pitch:
                    omn = staff.CurrentMeasureElement.ToOmnPitch();
                    break;

            }
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
