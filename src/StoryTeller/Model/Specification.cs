using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StoryTeller.Grammars;
using StoryTeller.Model.Persistence;

namespace StoryTeller.Model
{
    public class Specification : Node, INodeHolder
    {
        [JsonProperty("tags")] public readonly IList<string> Tags = new List<string>();
        private readonly IList<Node> _children = new List<Node>();
        [JsonIgnore] public string FileName;

        [JsonProperty("lifecycle")] public Lifecycle Lifecycle = Lifecycle.Acceptance;

        [JsonProperty("max-retries")] public int MaxRetries;

        [JsonProperty("title")] public string Name;

        [JsonProperty("steps", ItemConverterType = typeof (NodeConverter))]
        public IList<Node> Children
        {
            get { return _children; }
        }

        public SpecificationPlan CreatePlan(FixtureLibrary library)
        {
            var sectionPlans = Children.OfType<Section>().Select(x => x.CreatePlan(library));
            return new SpecificationPlan(sectionPlans) {Specification = this};
        }

        public static string DetermineFilename(string name)
        {
            var filename = name + ".xml";

            if (filename.Contains(" "))
            {
                filename = filename.Replace(' ', '_');
            }

            return filename.EscapeIllegalChars();
        }

        public Section AddSection(string key)
        {
            var section = new Section(key) {id = Guid.NewGuid().ToString()};
            Children.Add(section);

            return section;
        }

        public SpecNode ToNode()
        {
            return new SpecNode
            {
                name = Name,
                id = id,
                lifecycle = Lifecycle.ToString()
            };
        }

        public void ReadNode(SpecNode node)
        {
            Name = node.name;
            id = node.id;
            Lifecycle = (Lifecycle) Enum.Parse(typeof(Lifecycle), node.lifecycle);
        }
    }
}