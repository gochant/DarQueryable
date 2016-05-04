# DarQueryable

数据访问规则（Data Access Rule）筛选查询，一个 IQueryable 接口的扩展

## Dependencies

* [System.Linq.Dynamic](https://github.com/kahanu/System.Linq.Dynamic) (>=1.0.6)

## Examples

### 1. 参数筛选

```
var filterArg = new FilterArg();
filterArg.Statements.Add("Birth == [DateTimeNow]");
filterArg.Values.Add(new KeyValuePair<string, object>("DateTimeNow", DateTime.Now));

var result = list.Filter(filterArg);
```

### 2. 规则筛选

```
var rules = new List<DataAccessRule>
{
    new DataAccessRule {
        ResourceFilter = "Name == \"Zhang\"",
        Usage = RuleUsage.FilteringRow
    },
    new DataAccessRule {
        ResourceFilter = "Salary > 10000",
        ProtectedField = "Birth",
        Usage = RuleUsage.FilteringColumn
    }
};

var result = list.Filter(rules).Protect(rules).ToList();
```

