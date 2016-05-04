using System.Collections.Generic;
using System.Linq;

namespace DarQueryable
{
    /// <summary>
    /// 数据访问规则
    /// </summary>
    public class DataAccessRule : IDataAccessRule
    {
        /// <summary>
        /// 用户筛选条件
        /// </summary>
        public string UserFilter { get; set; }

        /// <summary>
        /// 资源类型，该类型的全名称
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// 资源筛选条件
        /// </summary>
        public string ResourceFilter { get; set; }

        /// <summary>
        /// 规则类型
        /// </summary>
        public RuleType RuleType { get; set; } = RuleType.Allow;

        /// <summary>
        /// 规则作用
        /// </summary>
        public RuleUsage? Usage { get; set; } = RuleUsage.FilteringRow;

        /// <summary>
        /// 受保护的字段
        /// </summary>
        public string ProtectedField { get; set; }

        /// <summary>
        /// 功能权限
        /// </summary>
        public string ProtectedAction { get; set; }

        /// <summary>
        /// 获取该规则需要保护的字段列表
        /// </summary>
        /// <returns></returns>
        public string[] GetProtectedFields()
        {
            return ProtectedField.Split(',');
        }

        /// <summary>
        /// 获取表达式谓词
        /// </summary>
        /// <returns></returns>
        public string GetPredicate()
        {
            var predicate = string.Empty;
            // 一条规则，UserFilter 和 ResourceFilter 是 and 关系
            if (!string.IsNullOrEmpty(UserFilter))
            {
                predicate = "(" + UserFilter + ")";
            }
            if (!string.IsNullOrEmpty(ResourceFilter))
            {
                if (!string.IsNullOrEmpty(predicate))
                {
                    predicate += " and ";
                }
                predicate += "(" + ResourceFilter + ")";
            }


            return predicate;
        }

        /// <summary>
        /// 从列表中获取表达式谓词
        /// </summary>
        /// <param name="rules">规则集</param>
        /// <returns></returns>
        public static string GetPredicateFromList(List<DataAccessRule> rules)
        {
            var result = rules.Select(rule => $"({rule.GetPredicate()})").ToList();

            // 不同规则间是 or 关系
            return string.Join(" or ", result);
        }
    }
}
