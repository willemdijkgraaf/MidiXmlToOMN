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

            // assert
            reader.ScoreEventsCounter.Should().Be(0);
            var verticalContent = reader.VerticalContent;
            verticalContent.Systems.Count().Should().Be(1);

            var system = verticalContent.Systems[0];
            system.Id.Should().Be("P1");
            system.Name.Should().Be("Flute");
            system.CycleStatus.Should().Be(MeasureInterpretationCycle.NotInitialized);

            reader.Next();
            reader.ScoreEventsCounter.Should().Be(1);
            system.CycleStatus.Should().Be(MeasureInterpretationCycle.StartBarLine);


            reader.Next();
            verticalContent = reader.VerticalContent;
            reader.ScoreEventsCounter.Should().Be(2);

        }
    }
}