namespace Coco.Core.Web
{
    using System.Reflection;

    internal static class MemberInfoExtensions
    {
        public static string Id(this MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}