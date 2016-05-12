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
    public class PersonCRUD
    {
        [Fact]
        public void TestCreate()
        {
            var p = new Person()
            {
                Name = "New Yorker",
                GivenName = "Some",
                Surname = "One",
                BirthDate = DateTime.Now.AddYears(-20),
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

            var helper = new PeopleManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
        }

        [Fact]
        public void TestDelete()
        {
            var p = new Person()
            {
                Name = "ToBeDeleted",
                GivenName = "Some",
                Surname = "One",

            };

            var helper = new PeopleManager();
            var id = helper.Create(p);
            Assert.NotNull(id);

            var ok = helper.Delete(id);
            Assert.True(ok);
        }

        [Fact]
        public void TestRead()
        {
            var p = new Person()
            {
                Name = "ToBeRead",
                GivenName = "Some",
                Surname = "One",
                BirthDate = DateTime.Now.AddYears(-20),
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

            var helper = new PeopleManager();
            var id = helper.Create(p);
            Assert.NotNull(id);
            var pr = helper.Read(id);
            Assert.Equal("ToBeRead", pr.Name);
        }

        [Fact]
        public void TestUpdate()
        {
            var p = new Person()
            {
                Name = "ToBeRead",
                GivenName = "Some",
                Surname = "One",
                BirthDate = DateTime.Now.AddYears(-20),
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

            var helper = new PeopleManager();
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

        [Fact]
        public void TestSearch()
        {
            var keywordToSearch = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var p = new Person()
            {
                Name = "New Yorker",
                GivenName = "SearchSome "+keywordToSearch,
                Surname = "One",
                BirthDate = DateTime.Now.AddYears(-20),
                Addresses =
                {
                    new Address()
                    {
                        Street1= "NoName Street ",
                        City="New York",
                        State="New York",
                        Country="USA",
                        AddressType= AddressType.Home,
                    }
                },
            };

            var helper = new PeopleManager();
            var id = helper.Create(p);
            System.Threading.Thread.Sleep(500); //indexing and analyzing might take time.
            var response = EntityIndexClient.Instance.Client.Search<Person>(s => s.Query(q => q.Bool(b => b.Must(qcd => qcd.Term(e => e.Field(f => f.GivenName).Value(keywordToSearch))))));
            Assert.True(response.IsValid);
            Assert.Equal(1, response.Documents.Count());
            Assert.Equal(1, response.Total);
            Assert.Equal("SearchSome "+keywordToSearch, response.Documents.First().GivenName);
        }
    }
}
