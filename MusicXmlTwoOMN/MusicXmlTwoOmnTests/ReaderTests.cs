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
        public void ShallRead_OneNoteAndOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\OneNoteAndOneTimeSignature.musicxml";
            var reader = new Reader(path);

            // act
            reader.Read();

            var verticalContent = reader.VerticalContent;
            var cycle = new List<MeasureInterpretationCycle>();
            while (reader.Next())
            {
                cycle.Add(verticalContent.Systems[0].CycleStatus);
            }
            // assert
            
            verticalContent.Systems.Count().Should().Be(1);

            var system = verticalContent.Systems[0];
            system.Id.Should().Be("P1");
            system.Name.Should().Be("Flute");
            cycle.Should().BeEquivalentTo(new List<MeasureInterpretationCycle> 
            {
                MeasureInterpretationCycle.StartBarLine,
                MeasureInterpretationCycle.TimeSignature,
                MeasureInterpretationCycle.Length,
                MeasureInterpretationCycle.Pitch,
                MeasureInterpretationCycle.Velocity,
                MeasureInterpretationCycle.Attribute,
                MeasureInterpretationCycle.EndBarLine,
                MeasureInterpretationCycle.End,

            });
            
        }
    }
}