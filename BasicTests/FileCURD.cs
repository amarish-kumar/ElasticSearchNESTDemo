using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Fonlow.Demo.Contracts;
using ESDemoLib;

namespace BasicTests
{
    public class MediaFileCRUD
    {
        static byte[] ReadDing()
        {
            return System.IO.File.ReadAllBytes(@"C:\Windows\Media\ding.wav");
        }

        [Fact]
        public void TestCreate()
        {
            var p = new MediaFile()
            {
                Name = "Ding"+DateTime.Now.ToString("yyyyMMddHHmmsszzz"),
                Content=ReadDing(),
            };

            var helper = new FilesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
        }

        [Fact]
        public void TestDelete()
        {
            var p = new MediaFile()
            {
                Name = "ToBeDeleted" + DateTime.Now.ToString("yyyyMMddHHmmsszzz"),
                Content = ReadDing(),
            };

            var helper = new FilesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);

            var ok = helper.Delete(id);
            Assert.True(ok);
        }

        [Fact]
        public void TestRead()
        {
            var name = "ReadDing" + DateTime.Now.ToString("yyyyMMddHHmmsszzz");
            var p = new MediaFile()
            {
                Name = name,
                Content = ReadDing(),
            };

            var size = p.Content.Length;
            var helper = new FilesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
            var pr = helper.Read(id);
            Assert.Equal(name, pr.Name);
            Assert.Equal(size, pr.Content.Length);
        }

        [Fact]
        public void TestUpdate()
        {
            var p = new MediaFile()
            {
                Name = "ReadDing",
                Content = ReadDing(),
            };

            var helper = new FilesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
            var pr = helper.Read(id);
            Assert.Equal("ReadDing", pr.Name);

            pr.Name = "ReadDing";
            var ok = helper.Update(pr);
            Assert.True(ok);

            pr = helper.Read(id);
            Assert.Equal("ReadDing", pr.Name);
        }


    }
}
