using musicxml;
using System.Xml;
using System.Xml.Serialization;

namespace MusicXmlTwoOMN
{
    public class Reader
    {
        private readonly string _path;
        public Scorepartwise Score {  get; private set; }
        public int ScoreEventsCounter { get; private set; }
        public VerticalContent VerticalContent { get; private set; }

        public Reader(string path)
        {
            _path = path;
        }

        public void Read()
        {
            Score = DeserializeFromFile<Scorepartwise>(_path);
            ScoreEventsCounter = 0;
            InitializeContent();
        }

        private T DeserializeFromFile<T>(string xmlFilePath) where T : class
        {
            using (var reader = XmlReader.Create(xmlFilePath, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse }))
            {
                var xRoot = new XmlRootAttribute
                {
                    ElementName = "score-partwise",
                    IsNullable = false
                };
                var serializer = new XmlSerializer(typeof(T), xRoot);

                return (T)serializer.Deserialize(reader);
            }
        }

        private void InitializeContent()
        {
            VerticalContent = new VerticalContent();
            foreach (var part in Score.Partlist.Items)
            {
                //var horizontalContent = new HorizontalContent(
                //    part.Id,
                //    part.Id,// todo
                //    part.Measure);
               
                //VerticalContent.Staves.Add(horizontalContent);
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