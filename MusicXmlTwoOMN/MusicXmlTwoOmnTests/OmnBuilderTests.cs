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
            staffs[0].Should().Be("'(w c4)");
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
            staffs[0].Should().Be("'(w c4)(w d4)");
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
            staffs[0].Should().Be("'(w c4)(w c4)");
        }
    }
}
