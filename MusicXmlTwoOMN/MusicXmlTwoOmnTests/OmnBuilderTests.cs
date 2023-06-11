using FluentAssertions;
using MusicXmlTwoOMN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            reader.Read();
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
            reader.Read();
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
            reader.Read();
            var omnBuilder = new OmnBuilder(reader);

            // act
            var staffs = omnBuilder.BuildStaves();

            // assert
            staffs[0].Should().Be("'((q cb4 q c4 q cs4 q db4 )(q d4 q ds4 q eb4 q e4 )(q es4 q fb4 q f4 q fs4 )(q gb4 q g4 q gs4 q ab4 )(q a4 q as4 q bb4 q b4 )(q bs4 -q -h ))");
        }
    }
}
