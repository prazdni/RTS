namespace System
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttribute : Attribute
    {
        public readonly string AssetName;
        
        public InjectAssetAttribute(string assetName)
        {
            AssetName = assetName;
        }
    }
}