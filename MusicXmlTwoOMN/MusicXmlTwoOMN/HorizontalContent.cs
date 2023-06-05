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
            CycleStatus = BarContentInterpretationCycle.NotInitialized;
            _measures = measures;
            CurrentMeasureIndex = 0;
            CurrentMeasureElementIndex = 0;
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public BarContentInterpretationCycle CycleStatus { get; private set; }
        public Measure CurrentMeasure => _measures[CurrentMeasureIndex];
        public int CurrentMeasureIndex { get; private set; }
        public int CurrentMeasureElementIndex { get; private set; }
        public MeasureElement MeasureElement => _measures[CurrentMeasureIndex].MeasureElements[CurrentMeasureElementIndex];
        public bool Next()
        {
            if (CycleStatus == BarContentInterpretationCycle.End) return false;
            
            if (CycleStatus == BarContentInterpretationCycle.EndBarLine)
            {
                CycleStatus = BarContentInterpretationCycle.NotInitialized;
            } else
            {
                CycleStatus++;
            }

            SetIndexes();
            
            if (CurrentMeasureIndex > _measures.Count - 1) {
                CycleStatus = BarContentInterpretationCycle.End;
            }

            return true;
        }

        private void SetIndexes()
        {
            switch (CycleStatus)
            {
                case BarContentInterpretationCycle.StartBarLine:
                case BarContentInterpretationCycle.TimeLength:
                    return;
            }

            if (CurrentMeasureElementIndex < _measures[CurrentMeasureIndex].MeasureElements.Count - 1)
            {
                // Next measure element
                CurrentMeasureElementIndex++;
            }
            else
            {
                // next measure
                CurrentMeasureIndex++;
                CurrentMeasureElementIndex = 0;
            }
        }
    }
}