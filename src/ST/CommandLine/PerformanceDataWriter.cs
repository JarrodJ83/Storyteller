﻿using System.Collections.Generic;
using System.IO;
using FubuCore;
using FubuMVC.Core.Resources.Conneg;
using Storyteller.Core.Engine;
using Storyteller.Core.Remotes.Messaging;

namespace ST.CommandLine
{
    public static class PerformanceDataWriter
    {
        public static void WriteCSV(BatchRunResponse results, string file)
        {
            using (var writer = new StreamWriter(file))
            {

                results.records.Each(record =>
                {
                    var suite = record.header.SuitePath();
                    var name = record.header.name.Replace(',', ' ');
                    var id = record.header.id;

                    record.results.Performance.Each(x =>
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}",
                            id,
                            suite,
                            name,
                            x.Type,
                            x.Subject,
                            x.Duration,
                            x.Start,
                            x.End
                            );

                    });
                });
            }
        }

        public static void WriteJSON(BatchRunResponse results, string file)
        {
            new FileSystem().WriteStringToFile(file, JsonSerialization.ToIndentedJson(results.records));
        }


    }
}