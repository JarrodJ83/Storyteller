﻿using ST.Docs.Samples;
using ST.Docs.Topics;

namespace ST.Docs.Transformation
{
    public class CodeSampleTransformHandler : ITransformHandler
    {
        private readonly ISampleCache _cache;

        public CodeSampleTransformHandler(ISampleCache cache)
        {
            _cache = cache;
        }

        public string Key
        {
            get { return "sample"; }
        }

        public string Transform(Topic current, string data)
        {
            var sample = _cache.Find(data);

            return new SampleTag(sample).ToString();
        }
    }
}