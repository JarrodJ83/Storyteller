﻿using System;
using System.Diagnostics;
using System.IO;
using FubuCore;
using FubuCore.CommandLine;

namespace ST.Docs
{
    [CommandDescription("Run the documentation in a live mode", Name = "doc-run")]
    public class DocRunCommand : FubuCommand<DocInput>
    {
        public override bool Execute(DocInput input)
        {
            var settings = input.ToSettings();

            using (var project = new DocProject(settings))
            {
                using (var server = project.LaunchRunner())
                {
                    Console.WriteLine("Launching the browser to " + server.BaseAddress);

                    Process.Start(server.BaseAddress);

                    tellUsersWhatToDo();
                    ConsoleKeyInfo key = Console.ReadKey();
                    while (key.Key != ConsoleKey.Q)
                    {
                    }
                }
            }

            return true;
        }

        private static void tellUsersWhatToDo()
        {
            Console.WriteLine("Press 'q' to quit");
        }
    }
}