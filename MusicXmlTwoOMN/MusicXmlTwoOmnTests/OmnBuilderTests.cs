using FluentAssertions;
using MusicXmlTwoOMN;

namespace MusicXmlTwoOmnTests
{
    [TestClass]
    public class OmnBuilderTests
    {
        [TestMethod]
        public void ShallBuildOmn_OneBarOneNoteAndOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\OneBarOneNoteAndOneTimeSignature.musicxml";
            var reader = new Reader(path);
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("'((w c4 ))");
        }

        [TestMethod]
        public void ShallBuildOmn_TwoBarsTwoNotesAndOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\TwoBarsTwoNotesAndOneTimeSignature.musicxml";
            var reader = new Reader(path);
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("'((w c4 )(w d4 ))");
        }

        [TestMethod]
        public void ShallBuildOmn_ChromaticScaleWithFlatsAndSharpsAccidentals()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\ChromaticScaleWithFlatsAndSharpsAccidentals.musicxml";
            var reader = new Reader(path);
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("'((q cb4 q c4 q cs4 q db4 )(q d4 q ds4 q eb4 q e4 )(q es4 q fb4 q f4 q fs4 )(q gb4 q g4 q gs4 q ab4 )(q a4 q as4 q bb4 q b4 )(q bs4 -q -h ))");
        }

        [TestMethod]
        public void ShallBuildOmn_OneNoteLengthPerBarOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\OneNoteLengthPerBarOneTimeSignature.musicxml";
            var reader = new Reader(path);
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("'((x c4 -x -t -s -e -q -h )(t c4 -t -s -e -q -h )(s c4 -s -e -q -h )(e c4 -e -q -h )(q c4 -q -h )(h c4 -h )(w c4 ))");
        }

        [TestMethod]
        public void ShallBuildOmn_OneQuarterDottedNoteAndOneBarAndOneTimeSignature()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\OneQuarterDottedNoteAndOneBarAndOneTimeSignature.musicxml";
            var reader = new Reader(path);
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("'((q. c4 -e -h ))");
        }

        [TestMethod]
        public void ShallBuildOmn_DottedQuarterNotesFromNoDotsToFourDots()
        {
            // arrange
            var path = "C:\\repos\\MidiXmlToOMN\\MusicXmlTwoOMN\\MusicXmlTwoOmnTests\\TestData\\MusicXmlTestCases\\DottedQuarterNotesFromNoDotsToFourDots.musicxml";
            var reader = new Reader(path);
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("");
        }
    }
}
