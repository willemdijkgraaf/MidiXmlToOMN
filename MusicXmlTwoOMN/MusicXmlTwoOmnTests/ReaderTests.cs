using FluentAssertions;
using MusicXmlTwoOMN;

namespace MusicXmlTwoOmnTests
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        public void ShallRead_MovementTitle()
        {
            // arrange
            var path = "TestData/MusicXmlWithStaffValues.xml";
            var reader = new Reader(path);
            
            // act
            reader.Read();

            // assert
            reader.Score.MovementTitle.Should().Be("Im wunderschönen Monat Mai");
        }

        [TestMethod]
        public void ShallRead_OneBarOneNoteAndOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\OneBarOneNoteAndOneTimeSignature.musicxml";
            var reader = new Reader(path);

            // act
            reader.Read();

            var stave = reader.VerticalContent.Staves[0];
            var cycle = new List<MeasureInterpretationCycle>();
            while (reader.Next())
            {
                cycle.Add(stave.CycleStatus);
            }

            // assert
            cycle.Should().BeEquivalentTo(new List<MeasureInterpretationCycle> 
            {
                MeasureInterpretationCycle.StartBarLine,
                MeasureInterpretationCycle.TimeSignature,
                MeasureInterpretationCycle.Length,
                MeasureInterpretationCycle.Pitch,
                MeasureInterpretationCycle.Velocity,
                MeasureInterpretationCycle.Attribute,
                MeasureInterpretationCycle.EndBarLine
            });
            
        }

        [TestMethod]
        public void ShallRead_TwoBarsTwoNotesAndOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\TwoBarsTwoNotesAndOneTimeSignature.musicxml";
            var reader = new Reader(path);

            // act
            reader.Read();

            var stave = reader.VerticalContent.Staves[0];
            var cycle = new List<MeasureInterpretationCycle>();
            while (reader.Next())
            {
                cycle.Add(stave.CycleStatus);
            }

            // assert
            cycle.Should().BeEquivalentTo(new List<MeasureInterpretationCycle>
            {
                // first bar
                MeasureInterpretationCycle.StartBarLine,
                MeasureInterpretationCycle.TimeSignature,
                MeasureInterpretationCycle.Length,
                MeasureInterpretationCycle.Pitch,
                MeasureInterpretationCycle.Velocity,
                MeasureInterpretationCycle.Attribute,
                MeasureInterpretationCycle.EndBarLine,
                // second bar
                MeasureInterpretationCycle.NotInitialized, // todo: shall be removed from output but doesn't harm for now.
                MeasureInterpretationCycle.StartBarLine,
                MeasureInterpretationCycle.TimeSignature,
                MeasureInterpretationCycle.Length,
                MeasureInterpretationCycle.Pitch,
                MeasureInterpretationCycle.Velocity,
                MeasureInterpretationCycle.Attribute,
                MeasureInterpretationCycle.EndBarLine
            });

        }
    }
}