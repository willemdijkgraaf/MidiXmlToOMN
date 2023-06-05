﻿using MusicXml;
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
               
                VerticalContent.Systems.Add(horizontalContent);
            }
        }

        public bool Next()
        {
            var hasMore = false;
            foreach (var system in VerticalContent.Systems)
            {
                hasMore = system.Next();
            }

            ScoreEventsCounter++;
            return hasMore;
        }
    }
}