using System;
using System.Collections.Generic;
using Common.Components.UI;
using Common.Utils;
using UnityEngine;

namespace SomniumSpace.UI.Items.ItemPanels
{
    public class ItemPanel<TItemData, TCategory> : MonoBehaviour
    {
        [SerializeField] private ExtTMPText titleLabel;

        [SerializeField] private ItemPanelCategoryUIBase<TCategory> categoryPrefab;
        [SerializeField] private RectTransform categoriesContainer;

        [SerializeField] private ItemPanelItemUIBase<TItemData> itemPrefab;
        [SerializeField] private RectTransform itemsContainer;
        
        private const string CATEGORY = "Category";

        private readonly List<ItemPanelCategoryUIBase<TCategory>> _categories = new();
        private readonly List<ItemPanelItemUIBase<TItemData>> _items = new();
        private Action<TItemData> _clickItem;

        private Type _itemDataType;
        private string _itemDataCategoryFieldName;
        private TCategory _currentCategory;
        public void SetTitle(string title) => titleLabel.SetText(title);
        public void SetAction(Action<TItemData> clickItem) => _clickItem = clickItem;

        private List<TCategory> _categoriesData = new();
        private List<TItemData> _itemsData;

        public void SetItems(List<TItemData> itemsData)
        {
            _itemsData = itemsData;
            _itemDataType = typeof(TItemData);

            GetCategories();
            if (_categoriesData.Count > 0)
            {
                _currentCategory = _categoriesData[0];

                UpdateCategoriesView();
                UpdateItemsView();    
            }
        }

        private void UpdateCategoriesView()
        {
            ClearCategories();
            SpawnCategories(_categoriesData);
        }

        private void UpdateItemsView()
        {
            ClearItems();
            SpawnItems(_itemsData);
        }
        
        private void SpawnCategories(List<TCategory> data)
        {
            categoryPrefab.gameObject.SetActive(true);
            foreach (var itemData in data)
            {
                var item = SpawnUtils.Instantiate(categoryPrefab, categoriesContainer);
                item.SetData(itemData);
                item.SetAction(OnClickCategory);
                _categories.Add(item);
            }
            categoryPrefab.gameObject.SetActive(false);
        }

        private void OnClickCategory(TCategory category)
        {
            _currentCategory = category;

            UpdateItemsView();
        }

        private void ClearCategories()
        {
            foreach (var category in _categories)
                category.DeInit();
            _categories.Clear();
        }

        // TODO add object pool
        private void SpawnItems(List<TItemData> data)
        {
            itemPrefab.gameObject.SetActive(true);
            foreach (var itemData in data)
            {
                if (!_itemDataType.GetField(_itemDataCategoryFieldName).GetValue(itemData).Equals(_currentCategory)) continue;

                var item = SpawnUtils.Instantiate(itemPrefab, itemsContainer);
                item.SetData(itemData);
                item.SetAction(_clickItem);
                _items.Add(item);
            }
            itemPrefab.gameObject.SetActive(false);
        }
        
        private void ClearItems()
        {
            foreach (var item in _items)
            {
                item.DeInit();
                SpawnUtils.Destroy(item.gameObject);
            }
            _items.Clear();
        }
        
        private void GetCategories()
        {
            var itemDataType = typeof(TItemData);
            var categoryType = typeof(TCategory);

            string fieldName = null;
            var fields = itemDataType.GetFields();
            if (categoryType == typeof(string))
            {
                foreach (var field in fields)
                {
                    if (field.Name.Contains(CATEGORY))
                    {
                        fieldName = field.Name;
                        break;
                    }
                }    
            }
            else
            {
                foreach (var field in fields)
                {
                    if (field.FieldType == categoryType)
                    {
                        fieldName = field.Name;
                        break;
                    }
                }
            }

            // add "all" category
            _categoriesData.Clear();
            foreach (var itemData in _itemsData)
            {
                var category = (TCategory)(itemDataType.GetField(fieldName).GetValue(itemData));
                if (!_categoriesData.Contains(category))
                    _categoriesData.Add(category);
            }
            
            _itemDataCategoryFieldName =  fieldName;
        }
    }
}