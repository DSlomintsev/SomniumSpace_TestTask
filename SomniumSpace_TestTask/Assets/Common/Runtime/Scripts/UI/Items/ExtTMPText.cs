using TMPro;
using UnityEngine;


namespace Common.Components.UI
{
    public class ExtTMPText : TextMeshProUGUI
    {
        [SerializeField] private RectTransform rectTransform;
        
        public string Id { get; private set; }
        public GameObject GameObject => gameObject;
        public RectTransform RectTransform => rectTransform;

        protected override void Awake()
        {
            base.Awake();
            
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }

        public void Init(string id)
        {
            Id = id;
        }

        public void DeInit()
        {
        }

        public void Rebuild()
        {
            rectTransform.sizeDelta = new Vector2(preferredWidth, preferredHeight);
        }
    }
}
