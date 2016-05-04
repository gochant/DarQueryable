namespace DarQueryable
{
    public interface IDataAccessRule
    {
        /// <summary>
        /// 用户筛选条件
        /// </summary>
        string UserFilter { get; set; }

        /// <summary>
        /// 资源类型，该类型的全名称
        /// </summary>
        string ResourceType { get; set; }

        /// <summary>
        /// 资源筛选条件
        /// </summary>
        string ResourceFilter { get; set; }

        /// <summary>
        /// 规则类型
        /// </summary>
        RuleType RuleType { get; set; }

        /// <summary>
        /// 规则作用
        /// </summary>
        RuleUsage? Usage { get; set; }

        /// <summary>
        /// 受保护的字段
        /// </summary>
        string ProtectedField { get; set; }

        /// <summary>
        /// 功能权限
        /// </summary>
        string ProtectedAction { get; set; }
    }
}