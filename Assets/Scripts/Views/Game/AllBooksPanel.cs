using System;
using System.Collections.Generic;
using UnityEngine;

using Views.General;
using Models;

namespace Views.Game
{
    public class AllBooksPanel : PanelView
    {
        public Action<int> PressItemAction { get; set; }

        [SerializeField] 
        private GameObject _descriptionPrefab;
        [SerializeField] 
        private Transform _contentTransform;

        private List<AllBooksDescriptionItemView> _allBooksDescriptionItemViews;
        
        public void Unsubscribe()
        {
            if (_allBooksDescriptionItemViews?.Count == 0)
            {
                return;
            }

            foreach (var allBooksDescriptionItemView in _allBooksDescriptionItemViews)
            {
                allBooksDescriptionItemView.PressBtnAction -= OnPressItem;
            }
        }

        public void SetBooks(List<BookModel> models)
        {
            DeleteBooks();
            
            _allBooksDescriptionItemViews = new List<AllBooksDescriptionItemView>();
            
            foreach (var model in models)
            {
                GameObject go = Instantiate(_descriptionPrefab, _contentTransform);

                AllBooksDescriptionItemView descriptionBook = go.GetComponent<AllBooksDescriptionItemView>();
                
                _allBooksDescriptionItemViews.Add(descriptionBook);

                descriptionBook.SetName(model.Name);
                descriptionBook.SetDescription(model.Description);
                descriptionBook.SetGenres(model.GenreIndexes);
                descriptionBook.SetRate(model.Stars);

                descriptionBook.PressBtnAction += OnPressItem;
            }
        }

        private void OnPressItem(AllBooksDescriptionItemView allBooksDescriptionItemView)
        {
            int index = _allBooksDescriptionItemViews.IndexOf(allBooksDescriptionItemView);
            
            PressItemAction?.Invoke(index);
        }

        private void DeleteBooks()
        {
            if (_allBooksDescriptionItemViews?.Count > 0)
            {
                foreach (var allBooksDescriptionItemView in _allBooksDescriptionItemViews)
                {
                    Destroy(allBooksDescriptionItemView.gameObject);
                }
            }
        }
    }
}