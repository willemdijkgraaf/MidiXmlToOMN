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
    }
}