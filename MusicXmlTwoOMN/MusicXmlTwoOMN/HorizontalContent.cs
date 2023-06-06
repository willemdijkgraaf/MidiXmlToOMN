﻿using musicxml;

namespace MusicXmlTwoOMN
{
    public class HorizontalContent
    {
        private ScorepartwisePartMeasure[] _measures { get; set; }

        public HorizontalContent(
            string id, 
            string name,
            IEnumerable<ScorepartwisePartMeasure> measures)
        {
            Id = id;
            Name = name;
            CycleStatus = MeasureInterpretationCycle.NotInitialized;
            _measures = measures.ToArray();
            CurrentMeasureIndex = 0;
            CurrentMeasureElementIndex = 0;
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public MeasureInterpretationCycle CycleStatus { get; private set; }
        public ScorepartwisePartMeasure CurrentMeasure => _measures[CurrentMeasureIndex];
        public int CurrentMeasureIndex { get; private set; }
        public int CurrentMeasureElementIndex { get; private set; }
        public bool HasMore { get; private set; }
        public object CurrentNote => _measures[CurrentMeasureIndex].Items[CurrentMeasureElementIndex];
        public void Next()
        {
            if (CycleStatus == MeasureInterpretationCycle.End) return;
            
            if (CycleStatus == MeasureInterpretationCycle.EndBarLine)
            {
                CycleStatus = MeasureInterpretationCycle.NotInitialized;
            } else
            {
                CycleStatus++;
            }

            SetIndexes();
        }

        private void SetIndexes()
        {
            switch (CycleStatus)
            {
                case MeasureInterpretationCycle.StartBarLine:
                case MeasureInterpretationCycle.TimeSignature:
                case MeasureInterpretationCycle.Length:
                case MeasureInterpretationCycle.Pitch:
                case MeasureInterpretationCycle.Velocity:
                case MeasureInterpretationCycle.Attribute:
                case MeasureInterpretationCycle.EndBarLine:
                    HasMore = true;
                    return;
            }

            if (CurrentMeasureElementIndex < _measures[CurrentMeasureIndex].Items.Count - 1)
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

            if (CurrentMeasureIndex > _measures.Length - 1)
            {
                CycleStatus = MeasureInterpretationCycle.End;
                HasMore = false;
            }
        }
    }
}