using System;

namespace DarQueryable
{
    /// <summary>
    /// 规则作用
    /// </summary>
    [Flags]
    public enum RuleUsage
    {
        /// <summary>
        /// 筛选行
        /// </summary>
        FilteringRow,
        /// <summary>
        /// 筛选列
        /// </summary>
        FilteringColumn
    }
}