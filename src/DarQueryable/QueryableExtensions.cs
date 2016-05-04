using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace DarQueryable
{
    /// <summary>
    /// 安全查询扩展
    /// </summary>
    public static class QueryableExtensions
    {

        /// <summary>
        /// 保护数据
        /// </summary>
        /// <param name="entity">实体本身</param>
        /// <param name="protectedFields">要保护的字段</param>
        /// <returns></returns>
        public static object ProtectData(this object entity, string[] protectedFields)
        {
            var type = entity.GetType();
            foreach (var prop in protectedFields)
            {
                var propInfo = type.GetProperty(prop);
                propInfo?.SetValue(entity, null);
            }

            return entity;
        }

        public static IEnumerable<T> Protect<T>(this IEnumerable<T> data, List<DataAccessRule> rules)
        {
            var list = data.ToList();
            var validRules =
                rules.Where(d => (d.Usage & RuleUsage.FilteringColumn) == RuleUsage.FilteringColumn && !string.IsNullOrEmpty(d.ProtectedField))
                    .ToList();
            foreach (var rule in validRules)
            {
                var needHandlelist = list.AsQueryable().Filter(new List<DataAccessRule> { rule }, ignoreUsage: true).ToList();

                foreach (var item in needHandlelist)
                {
                    item.ProtectData(rule.GetProtectedFields());
                }
            }
            return list;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, FilterArg filterArg)
        {
            if (filterArg != null && filterArg.Statements.Count > 0)
            {
                var predicate = filterArg.GetMergedStatement();

                if (!string.IsNullOrEmpty(predicate))
                {
                    queryable = queryable.Where(predicate, filterArg.GetValuesArray());
                }
            }

            return queryable;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, IEnumerable<DataAccessRule> rules,
            FilterArg filterArg = null, bool ignoreUsage = false)
        {
            if (filterArg == null)
            {
                filterArg = new FilterArg() { Logic = "or" };
            }
            if(rules == null)
            {
                return queryable;
            }
            var validRules = ignoreUsage ? rules.ToList() 
                : rules.Where(d => (d.Usage & RuleUsage.FilteringRow) == RuleUsage.FilteringRow).ToList();

            if (validRules.Any())
            {
                var predicate = DataAccessRule.GetPredicateFromList(validRules);
                filterArg.Statements.Add(predicate);
            }

            queryable = queryable.Filter(filterArg);

            return queryable;

        }
    }
}
