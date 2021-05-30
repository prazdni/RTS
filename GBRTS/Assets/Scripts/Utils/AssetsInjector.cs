using System.Reflection;

namespace System
{
    public static class AssetsInjector
    {
        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();

            while (targetType != null)
            {
                var fields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute(typeof(InjectAssetAttribute)) is InjectAssetAttribute injectAssetAttribute)
                    {
                        var prefab = context.GetAsset(injectAssetAttribute.AssetName);
                        field.SetValue(target, prefab);
                    }
                }

                targetType = targetType.BaseType;
            }

            return target;
        }
    }
}