﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ultramarine.Generators.Serialization.Providers;
using Ultramarine.Generators.Tasks;
using Ultramarine.Workspaces;

namespace Ultramarine.Generators.Tests
{
    [TestClass]
    public class StringManipulationTests
    {
        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\StringManipulationTest.gen.json", "Samples")]
        public void ShouldDeserializeGeneratorConfig()
        {
            var generatorPath = "Samples\\StringManipulation\\StringManipulationTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);

            Assert.IsNotNull(generator);
            Assert.AreEqual(generator.Name, "generator1");
            Assert.AreEqual(generator.Description, "generator1description");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\StringManipulationTest.gen.json", "Samples")]
        public void ShouldDeserializeTaskCollection()
        {
            var generatorPath = "Samples\\StringManipulation\\StringManipulationTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            Assert.IsNotNull(generator.Tasks);
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\StringManipulationTest.gen.json", "Samples")]
        public void TaskCollectionShouldHaveCreateFolderTest()
        {
            var generatorPath = "Samples\\StringManipulation\\StringManipulationTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            Assert.IsNotNull(generator.Tasks.FirstOrDefault());
            var stringManipulationTask = generator.Tasks.First();
            Assert.IsInstanceOfType(stringManipulationTask, typeof(StringManipulation));
            Assert.AreEqual(stringManipulationTask.Name, "stringManipulationTask1");
            Assert.AreEqual(stringManipulationTask.Description, "stringManipulationTask1Description");
        }

        //[TestMethod]
        //[DeploymentItem("Samples\\StringManipulationTest.gen.json", "Samples")]
        //public void TestStringManipulation()
        //{
        //    var generatorPath = "Samples\\StringManipulationTest.gen.json";
        //    var generator = GeneratorSerializer.Instance.Load(generatorPath);
        //    Assert.IsNotNull(generator.Tasks.FirstOrDefault(x => x.Name == "stringManipulationTask1").Input);
        //    var input = generator.Tasks.FirstOrDefault(x => x.Name == "stringManipulationTask1");

        //    var stringMan = new StringManipulation();

        //    var ReplacedString = stringMan.Replace(input, "e", "1");
        //    Assert.AreEqual(ReplacedString, "N1wVariabl1");

        //    //var fs = "This is {0} string";
        //    //var FormattedString = stringMan.Format("new", fs);
        //    //Assert.AreEqual(FormattedString, "This is new string");

        //    stringMan.Input = "NewVariable";
        //    var camelCaseString = stringMan.Convert(CaseType.CamilCase);
        //    Assert.AreEqual(camelCaseString, "newVariable");

        //    var lowerString = stringMan.Convert(CaseType.Lower);
        //    Assert.AreEqual(lowerString, "newvariable");

        //    var upperString = stringMan.Convert(CaseType.Upper);
        //    Assert.AreEqual(upperString, "NEWVARIABLE");

        //    stringMan.Input = "newVariable";
        //    var hungarianString = stringMan.Convert(CaseType.Hungarian);
        //    Assert.AreEqual(hungarianString, "NewVariable");
        //}

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\StringManipulationFormatTest.gen.json", "Samples")]
        public void ShouldFormatInputWhenFormatIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\StringManipulationFormatTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "This is NewVariable");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\CaseType\\StringManipulationUpperCaseTypeTest.gen.json", "Samples")]
        public void ShouldUpperInputWhenUpperCaseTypeIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\CaseType\\StringManipulationUpperCaseTypeTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "NEWVARIABLE");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\CaseType\\StringManipulationLowerCaseTypeTest.gen.json", "Samples")]
        public void ShouldLowerInputWhenLowerCaseTypeIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\CaseType\\StringManipulationLowerCaseTypeTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "newvariable");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\CaseType\\StringManipulationCamelCaseTypeTest.gen.json", "Samples")]
        public void ShouldCamelCaseInputWhenCamelCaseTypeIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\CaseType\\StringManipulationCamelCaseTypeTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "newVariable");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\CaseType\\StringManipulationHungarianCaseTypeTest.gen.json", "Samples")]
        public void ShouldHungarianInputWhenHungarianCaseTypeIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\CaseType\\StringManipulationHungarianCaseTypeTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "NewVariable");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\StringManipulationReplaceTest.gen.json", "Samples")]
        public void ShouldReplaceInputWhenReplacementAndPatternIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\StringManipulationReplaceTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "Test testing REPLACEMENT");
        }

        [TestMethod]
        [DeploymentItem("Samples\\StringManipulation\\StringManipulationTest.gen.json", "Samples")]
        public void ShouldFormatReplaceAndChangeCaseTypeInputWhenAllIsProvided()
        {
            var generatorPath = "Samples\\StringManipulation\\StringManipulationTest.gen.json";
            var generator = GeneratorSerializer.Instance.Load(generatorPath);
            var loggerMock = new Mock<ILogger>();

            generator.SetLogger(loggerMock.Object);
            generator.Execute();

            var task = generator.Tasks.First();
            Assert.AreEqual(task.Output, "Name is newVariable");
        }
    }
}
