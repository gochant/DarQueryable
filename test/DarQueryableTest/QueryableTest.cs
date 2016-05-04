using System;
using System.Collections.Generic;
using System.Linq;
using DarQueryable;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarQueryableTest
{
    [TestClass]
    public class QueryableTest
    {

        [TestMethod]
        public void FilterByData()
        {
            var data = TestData.GetList();

            var filterArgs = new FilterArg();
            filterArgs.Statements.Add("Birth == [DateTimeNow]");
            filterArgs.Values.Add(new KeyValuePair<string, object>("DateTimeNow", DateTime.Now));

            var result = data.AsQueryable().Filter(filterArgs);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void FilterByDataAndLogicIsAnd()
        {
            var data = TestData.GetList();

            var filterArgs = new FilterArg { Logic = "and" };
            filterArgs.Statements.Add("Name == [TestName]");
            filterArgs.Statements.Add("Birth == [TestDateTime]");
            filterArgs.Values.Add(new KeyValuePair<string, object>("TestName", "Zhang"));
            filterArgs.Values.Add(new KeyValuePair<string, object>("TestDateTime", new DateTime(1980, 5, 3)));

            var result = data.AsQueryable().Filter(filterArgs);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void FilterByDataAndLogicIsOr()
        {
            var data = TestData.GetList();

            var filterArgs = new FilterArg { Logic = "or" };
            filterArgs.Statements.Add("Name == [TestName]");
            filterArgs.Statements.Add("Birth == [TestDateTime]");
            filterArgs.Values.Add(new KeyValuePair<string, object>("TestName", "Zhang"));
            filterArgs.Values.Add(new KeyValuePair<string, object>("TestDateTime", new DateTime(1970, 2, 11)));

            var result = data.AsQueryable().Filter(filterArgs);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void FilterByRule()
        {
            var data = TestData.GetList();
            var rules = new List<DataAccessRule>
            {
                new DataAccessRule {
                    ResourceFilter = "Name == \"Zhang\""
                }
            };
            var result = data.AsQueryable().Filter(rules);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void FilterByRuleAndDateTimeJudge()
        {
         
            var data = TestData.GetList();
            var rules = new List<DataAccessRule>
            {
                new DataAccessRule {
                    ResourceFilter = "Birth == DateTime(1992, 9, 2)"
                }
            };
            var result = data.AsQueryable().Filter(rules);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ProtectData()
        {
            var data = TestData.GetList();
            var rules = new List<DataAccessRule>
            {
                new DataAccessRule {
                    ResourceFilter = "Name == \"Zhang\"",
                    ProtectedField = "Salary",
                    Usage = RuleUsage.FilteringColumn | RuleUsage.FilteringRow
                },
                new DataAccessRule {
                    ResourceFilter = "Name == \"Wang\"",
                    ProtectedField = "Birth",
                    Usage = RuleUsage.FilteringColumn
                }
            };

            var result = data.AsQueryable().Protect(rules).ToList();
            Assert.AreEqual(result.Count, data.Count);
            var first = result.First();
            var second = result.FirstOrDefault(d => d.Name == "Wang");
            Assert.AreEqual(default(decimal), first.Salary);
            Assert.AreEqual(new DateTime(1980, 5, 3), first.Birth);
            Assert.AreEqual(8000, second?.Salary);
            Assert.AreEqual(default(DateTime), second?.Birth);
        }
    }
}
