using MusicXml;
using MusicXml.Domain;

namespace MusicXmlTwoOMN
{
    public class Reader
    {
        private readonly string _path;
        public Score Score {  get; private set; }
        public int ScoreEventsCounter { get; private set; }
        public VerticalContent VerticalContent { get; private set; }

        public Reader(string path)
        {
            _path = path;
        }

        public void Read()
        {
            Score = MusicXmlParser.GetScore(_path);
            ScoreEventsCounter = 0;
            InitializeContent();
        }

        private void InitializeContent()
        {
            VerticalContent = new VerticalContent();
            foreach (var part in Score.Parts)
            {
                var horizontalContent = new HorizontalContent(
                    part.Id,
                    part.Name,
                    part.Measures);
               
                VerticalContent.Staves.Add(horizontalContent);
            }
        }

        public bool Next()
        {
            var hasMore = true;
            foreach (var system in VerticalContent.Staves)
            {
                system.Next();
                if (!system.HasMore) hasMore = false;
            }

            ScoreEventsCounter++;
            return hasMore;
        }
    }
}