///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述： 扩展 - 可空类型类
///最后修改时间：2018/1/28
/// </summary>
namespace Diana.common.Util.Extension
{
    /// <summary>
    /// 扩展 - 可空类型
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
    }
}
