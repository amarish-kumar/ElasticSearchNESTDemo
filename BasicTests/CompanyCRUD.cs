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
    public class CompanyCRUD
    {
        [Fact]
        public void TestCreate()
        {
            var p = new Company()
            {
                Name = "New Yorker"+DateTime.Now.ToString("yyyyMMddHHmmsszzz"),
                BusinessNumber="1234567" + DateTime.Now.ToString("yyyyMMddHHmmsszzz"),
                BusinessNumberType="ABN",
                Addresses =
                {
                    new Address()
                    {
                        City="LA",
                        State="CA",
                        Country="USA",
                        AddressType= AddressType.Home,
                    }
                },
            };

            var helper = new CompaniesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
        }

        [Fact]
        public void TestDelete()
        {
            var p = new Company()
            {
                Name = "ToBeDeleted" + DateTime.Now.ToString("yyyyMMddHHmmsszzz"),

            };

            var helper = new CompaniesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);

            var ok = helper.Delete(id);
            Assert.True(ok);
        }

        [Fact]
        public void TestRead()
        {
            var name = "ToBeRead" + DateTime.Now.ToString("yyyyMMddHHmmsszzz");
            var p = new Company()
            {
                Name = name,
                Addresses =
                {
                    new Address()
                    {
                        City="New York",
                        State="New York",
                        Country="USA",
                        AddressType= AddressType.Home,
                    }
                },
            };

            var helper = new CompaniesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
            var pr = helper.Read(id);
            Assert.Equal(name, pr.Name);
        }

        [Fact]
        public void TestUpdate()
        {
            var p = new Company()
            {
                Name = "ToBeRead",
                Addresses =
                {
                    new Address()
                    {
                        City="New York",
                        State="New York",
                        Country="USA",
                        AddressType= AddressType.Home,
                    }
                },
            };

            var helper = new CompaniesManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
            var pr = helper.Read(id);
            Assert.Equal("ToBeRead", pr.Name);

            pr.Name = "ToBeUpdated";
            var ok = helper.Update(pr);
            Assert.True(ok);

            pr = helper.Read(id);
            Assert.Equal("ToBeUpdated", pr.Name);
        }


    }
}
