using System;
using Common.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Components.UI
{
    public class ExtBtn : Button
    {
        [SerializeField] private RectTransform rectTransform;
        //[SerializeField] private BaseHoverBeh hoverBeh;

        public string Id { get; private set; }
        public GameObject GameObject => gameObject;
        public RectTransform RectTransform => rectTransform;

        private Action _action;

        protected override void Awake()
        {
            base.Awake();

            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }

        public void Init(string id)
        {
            Id = id;

            onClick.RemoveListener(OnClick);
            onClick.AddListener(OnClick);
        }

        public void Init(Action action)
        {
            _action = action;

            onClick.RemoveListener(OnClick);
            onClick.AddListener(OnClick);
        }

        public void DeInit()
        {
            onClick.RemoveListener(OnClick);
            _action = null;
        }

        public void Rebuild()
        {
        }

        private void OnClick() => _action.Call();
    }
}