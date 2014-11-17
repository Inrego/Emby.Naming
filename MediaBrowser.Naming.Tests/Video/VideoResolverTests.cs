﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class VideoResolverTests : BaseVideoTest
    {
        [TestMethod]
        public void TestSimpleFile()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Brave (2007)\\Brave (2006).mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestSimpleFile2()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Bad Boys (1995)\\Bad Boys (1995).mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(1995, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Bad Boys", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestSimpleFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestExtra()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Brave (2007)\\Brave (2006)-trailer.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Brave", result.Name);
            Assert.AreEqual("trailer", result.ExtraType);
        }

        [TestMethod]
        public void TestExtraWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006)-trailer.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.AreEqual("trailer", result.ExtraType);
        }

        [TestMethod]
        public void TestStubFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestStubFile()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Brave (2007)\\Brave (2006).bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestExtraStubWithNumericNameNotSupported()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006)-trailer.bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestExtraStubNotSupported()
        {
            // Using a stub for an extra is currently not supported
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\brave (2007)\\brave (2006)-trailer.bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void Test3DFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).3d.sbs.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(true, result.Is3D);
            Assert.AreEqual("sbs", result.Format3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestBad3DFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).3d1.sbas.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
            Assert.IsNull(result.Format3D);
        }

        [TestMethod]
        public void Test3DFile()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\brave (2007)\\brave (2006).3d.sbs.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(true, result.Is3D);
            Assert.AreEqual("sbs", result.Format3D);
            Assert.AreEqual("brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }
    }
}
