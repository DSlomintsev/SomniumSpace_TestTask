using UnityEngine;

namespace Common.Services.UIs
{
    public class UIContainer: MonoBehaviour
    {
        [SerializeField] public RectTransform dynamicContainer;
        [SerializeField] public RectTransform staticContainer;
        [SerializeField] public RectTransform worldContainer;
        [SerializeField] public RectTransform dialogsContainer;
    }
}