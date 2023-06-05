using MusicXml;
using MusicXml.Domain;

namespace MusicXmlTwoOMN
{
    public class Reader
    {
        private readonly string _path;
        public Score Score {  get; private set; }
        public Reader(string path)
        {
            _path = path;
        }

        public void Read()
        {
            Score = MusicXmlParser.GetScore(_path);
        }
    }
}