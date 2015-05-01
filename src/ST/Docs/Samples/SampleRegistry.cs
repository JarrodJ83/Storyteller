using FubuCore;
using StructureMap.Configuration.DSL;

namespace ST.Docs.Samples
{
    public class SampleRegistry : Registry
    {
        public SampleRegistry()
        {
            For<ISampleScanner>().AddInstances(_ =>
            {
                _.Type<RazorScanner>();
                _.Type<RubySampleScanner>();

                _.Object(new CLangSampleScanner("cs", "csharp"));
                
                //_.Object(new CLangSampleScanner("js", "javascript"));
                //_.Object(new BlockCommentScanner("<!--", "-->", "htm", "markup"));
                //_.Object(new BlockCommentScanner("<!--", "-->", "html", "markup"));
                //_.Object(new BlockCommentScanner("<!--", "-->", "xml", "markup"));
                //_.Object(new BlockCommentScanner("/*", "*/", "css", "css"));
                                                             
            });

            For<IFileSystem>().Use<FileSystem>();

            ForSingletonOf<ISampleCache>().Use<SampleCache>();
            ForSingletonOf<ISampleBuilder>().Use<SampleBuilder>();
        }
    }
}