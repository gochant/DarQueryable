using System.Collections.Generic;
using System.Linq;

namespace DarQueryable
{
    /// <summary>
    /// 动态 Linq 的筛选数据
    /// </summary>
    public class FilterArg
    {
        public List<string> Statements { get; set; } = new List<string>();

        public List<KeyValuePair<string, object>> Values { get; set; } = new List<KeyValuePair<string, object>>();

        public string Logic { get; set; } = "";

        public string GetMergedStatement()
        {
            var predicate = string.Join($" {Logic} ", Statements);
            for (int i = 0; i < Values.Count; i++)
            {
                var arg = Values[i];
                predicate = predicate.Replace($"[{arg.Key}]", $"@{i}");
            }
            
            return predicate;
        }

        public object[] GetValuesArray()
        {
            return Values.Select(d => d.Value).ToArray();
        }
    }
}