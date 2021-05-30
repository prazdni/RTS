using System.Linq;
using UnityEngine;

namespace System
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy/" + nameof(AssetsContext))]
    public class AssetsContext : ScriptableObject
    {
        [SerializeField] private GameObject[] _assets;

        public GameObject GetAsset(string assetName)
        {
            return _assets.FirstOrDefault(asset => asset.gameObject.name == assetName);
        }
    }
}