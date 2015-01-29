﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FubuCore;
using NUnit.Framework;
using Storyteller.Core.Engine;
using Storyteller.Core.Model;
using Storyteller.Core.Model.Persistence;
using StoryTeller.Samples;

namespace Storyteller.Core.Testing
{
    [TestFixture, Explicit]
    public class performance_tuning
    {
        private readonly string _folder = ".".ToFullPath()
            .ParentDirectory().ParentDirectory().ParentDirectory()
            .AppendPath("Storyteller.Samples", "Specs");

        private Suite _hierarchy;
        private SpecNode[] _allSpecs;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _hierarchy = HierarchyLoader.ReadHierarchy(_folder);
            _allSpecs = _hierarchy.GetAllSpecs().ToArray();
        }

        [Test]
        public void run_everything_crudely()
        {
            var system = new GrammarSystem();
            var task = FixtureLibrary.CreateForAppDomain(CellHandling.Basic());
            task.Wait();

            var library = task.Result;

            var observer = new NulloObserver();
            var stopConditions = new StopConditions();

            _allSpecs.Each(node =>
            {
                var spec = XmlReader.ReadFromFile(node.filename);
                var plan = spec.CreatePlan(library);

                using (var execution = system.CreateContext())
                {
                    var context = new SpecContext(observer, stopConditions, execution.Services);
                    var executor = new SynchronousExecutor(context);
                    plan.AcceptVisitor(executor);

                    Debug.WriteLine(node.path + ": " + context.Counts);
                }
            });
        }
    }
}