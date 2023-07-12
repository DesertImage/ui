using UnityEngine;

namespace DesertImage.UI
{
    public interface IHierarchyItem
    {
        int HierarchyIndex { get; }

        void SetHierarchyIndex(int value);
        void SetLastHierarchyIndex();

        void SetParent(Transform parent, bool worldPositionsStay = true);
    }
}