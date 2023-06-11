using MusicXml.Domain;

namespace MusicXmlTwoOMN
{
    public class HorizontalContent
    {
        private List<Measure> _measures { get; set; }

        public HorizontalContent(
            string id, 
            string name,
            List<Measure> measures)
        {
            Id = id;
            Name = name;
            CycleStatus = MeasureInterpretationCycle.NotInitialized;
            _measures = measures;
            CurrentMeasureIndex = 0;
            CurrentMeasureElementIndex = 0;
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public MeasureInterpretationCycle CycleStatus { get; private set; }
        public Measure CurrentMeasure => _measures[CurrentMeasureIndex];
        public int CurrentMeasureIndex { get; private set; }
        public int CurrentMeasureElementIndex { get; private set; }
        public bool HasMore { get; private set; }
        public MeasureElement CurrentMeasureElement => _measures[CurrentMeasureIndex].MeasureElements[CurrentMeasureElementIndex];
        public void Next()
        {
            if (CycleStatus == MeasureInterpretationCycle.End) return;
            SetIndexes();
        }

        private void SetIndexes()
        {
            
            HasMore = true;
            
            switch (CycleStatus)
            {
                case MeasureInterpretationCycle.NotInitialized:
                case MeasureInterpretationCycle.StartBarLine:
                case MeasureInterpretationCycle.TimeSignature:
                case MeasureInterpretationCycle.Length:
                case MeasureInterpretationCycle.Pitch:
                case MeasureInterpretationCycle.Velocity:
                //case MeasureInterpretationCycle.Attribute:
                //case MeasureInterpretationCycle.EndBarLine:
                    CycleStatus++;
                    return;
            }

            if (CycleStatus == MeasureInterpretationCycle.Attribute && !MeasureHasMoreElements())
            {
                CycleStatus = MeasureInterpretationCycle.EndBarLine;
                return;
            }

            if (!StaffHasMoreElements() && CycleStatus == MeasureInterpretationCycle.EndBarLine)
            {
                HasMore = false;
                CycleStatus = MeasureInterpretationCycle.End;
                return;
            }

            if (MeasureHasMoreElements())
            {
                CurrentMeasureElementIndex++;
                CycleStatus = MeasureInterpretationCycle.Length;
                return;
            }

            // next measure
            CurrentMeasureIndex++;
            CurrentMeasureElementIndex = 0;
            CycleStatus = MeasureInterpretationCycle.StartBarLine;
        }

        private bool StaffHasMoreElements()
        {
            return CurrentMeasureIndex < _measures.Count - 1;
        }

        private bool MeasureHasMoreElements()
        {
            return CurrentMeasureElementIndex < _measures[CurrentMeasureIndex].MeasureElements.Count - 1;
        }
    }
}