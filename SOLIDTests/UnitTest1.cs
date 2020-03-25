using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using Types;
using UrlBLL;
using UrlDAL;

namespace SOLIDTests
{
    public class Tests
    {
        private const string sourcePath = @"SourceText.txt";
        private const string xmlPath = @"xmlResult.xml";
        private const string xmlDomPath = @"xmlDomResult.xml";
        private const string xmlWriterTestPath = @"xmlWriterResult.xml";
        private const string correctXmlResult = @"correctXmlResult.xml";

        [SetUp]
        public void Init()
        {
            File.Delete(xmlPath);
            File.Delete(xmlDomPath);
            File.Delete(xmlWriterTestPath);
        }

        public static IEnumerable<TestCaseData> UrlsAndStrings
        {
            get 
            {
                yield return new TestCaseData("https://github.com/AnzhelikaKravchuk?tab=repositories", 
                new URLContainer("https", "github.com", new string[]{"AnzhelikaKravchuk"}, new Dictionary<string, string>() {{"tab", "repositories"}}));
                yield return new TestCaseData("https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU",
                    new URLContainer("https", "github.com", new string[] {"AnzhelikaKravchuk", "2017-2018.MMF.BSU"}));
                yield return new TestCaseData("https://habrahabr.ru/company/it-grad/blog/341486/",
                    new URLContainer("https", "habrahabr.ru", new string[]{"company", "it-grad", "blog", "341486"}));
                yield return new TestCaseData("https://canvas.instructure.com/courses/1777354/assignments/13569501",
                    new URLContainer("https", "canvas.instructure.com", new string[]{"courses", "1777354", "assignments", "13569501"}));
                yield return  new TestCaseData("https://it.belstu.by/studentam/uchebnyj-process/",
                    new URLContainer("https", "it.belstu.by", new string[] {"studentam", "uchebnyj-process"}));
            }
        }

        [Test, TestCaseSource(nameof(UrlsAndStrings))]
        public void UrlTransformerTests_CorrectValues(string url, URLContainer urlContainer)
        {
            URLTransformer transformer = new URLTransformer();
            Assert.AreEqual(transformer.Transform(url).Host, urlContainer.Host);
            Assert.AreEqual(transformer.Transform(url).Scheme, urlContainer.Scheme);
            CollectionAssert.AreEqual(transformer.Transform(url).GetParameters(), urlContainer.GetParameters());
            CollectionAssert.AreEqual(transformer.Transform(url).GetPath(), urlContainer.GetPath());
        }

        [Test]
        public void UrlTransformerTests_MockExceptions()
        {
            Mock<URLTransformer> urlTransformerMock = new Mock<URLTransformer>();
            urlTransformerMock.Setup(transformer => transformer.Transform(It.Is<string>(s => !s.Contains("://"))))
                .Throws<ArgumentException>();
        }

        [Test]
        public void UrlReaderTests_Result()
        {
            URLReader reader = new URLReader();
            Assert.AreEqual(new string[] {"https://github.com/AnzhelikaKravchuk?tab=repositories", "https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU", 
                "https://canvas.instructure.com/courses/1777354/assignments/13569501", "https://it.belstu.by/studentam/uchebnyj-process/", "https://habrahabr.ru/company/it-grad/blog/341486/"},
                reader.ReadInfo(sourcePath));
        }

        [Test]
        public void UrlWriterTests_ResultsAreSame()
        {
            URLContainer[] urls = new[]
            {
                new URLContainer("https", "github.com", new string[] {"AnzhelikaKravchuk"},
                    new Dictionary<string, string>() {{"tab", "repositories"}}),
                new URLContainer("https", "habrahabr.ru", new string[] {"company", "it-grad", "blog", "341486"}),
                new URLContainer("https", "canvas.instructure.com",
                    new string[] {"courses", "1777354", "assignments", "13569501"})
            };
            URLXmlWriter xmlWriter = new URLXmlWriter();
            URLXmlDom xmlDom = new URLXmlDom();
            xmlWriter.Write(xmlPath, urls);
            xmlDom.Write(xmlDomPath, urls);
            Assert.AreEqual(File.ReadAllText(xmlPath).ToLowerInvariant(), File.ReadAllText(xmlDomPath).ToLowerInvariant());
        }

        [Test]
        public void UrlXmlWriterTests_CorrectResult()
        {
            URLContainer[] urls = 
            {
                new URLContainer("https", "github.com", new string[] {"AnzhelikaKravchuk"},
                    new Dictionary<string, string>() {{"tab", "repositories"}}),
                new URLContainer("https", "github.com", new string[] {"AnzhelikaKravchuk", "2017-2018.MMF.BSU"}),
                new URLContainer("https", "habrahabr.ru", new string[] {"company", "it-grad", "blog", "341486"})
            };

            URLXmlWriter xmlWriter = new URLXmlWriter();
            xmlWriter.Write(xmlWriterTestPath, urls);
            Assert.AreEqual(File.ReadAllText(correctXmlResult), File.ReadAllText(xmlWriterTestPath));
        }

        [Test]
        public void UrlXmlWriterTests_MoqExceptionTest()
        {
            Mock<URLXmlWriter> xmlMoq = new Mock<URLXmlWriter>();
            xmlMoq.Setup((writer =>
                    writer.Write("exception.xml",
                        It.Is<IEnumerable<URLContainer>>((containers => containers == null)))))
                .Throws(new ArgumentNullException());
        }

        [Test]
        public void UrlXmlDomTests_MoqExceptionTest()
        {
            Mock<URLXmlDom> xmlMoq = new Mock<URLXmlDom>();
            xmlMoq.Setup((writer =>
                    writer.Write("exception.xml",
                        It.Is<IEnumerable<URLContainer>>((containers => containers == null)))))
                .Throws(new ArgumentNullException());
        }
    }
}