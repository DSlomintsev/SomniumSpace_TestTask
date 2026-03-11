using System;
using Common.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.Components.UI
{
    public class ExtImage : Image, IPointerDownHandler
    {
        [SerializeField] private RectTransform rectTransform;
        
        public string Id { get; private set; }
        public GameObject GameObject => gameObject;
        public RectTransform RectTransform => rectTransform;
        public Action Action { get; set; }
        public Action<string> IdAction { get; set; }
        
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

        public void Init()
        {
        }
        
        public void DeInit()
        {
            Action = null;
            IdAction = null;
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Action.Call();
            IdAction.Call(Id);
        }
    }
}
