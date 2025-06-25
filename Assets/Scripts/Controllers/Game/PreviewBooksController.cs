using System;
using System.Collections.Generic;
using UnityEngine;

using Views.Game;
using Models;
using UnityEngine.UI;

namespace Controllers.Game
{
    public class PreviewBooksController : MonoBehaviour
    {
        public Action<int> PressReadBtnAction { get; set; }
        public Action PressAllBtnAction { get; set; }

        [SerializeField] 
        private SwipeController _swipeController;
        [SerializeField] 
        private SelectorView _selectorView;
        [SerializeField] 
        private DescriptionBook _descriptionBook;
        [SerializeField] 
        private List<GameObject> _sideBooksGameObjects;

        [SerializeField] 
        private Button _allbtn;
        [SerializeField] 
        private Button _readbtn;

        private int _maxIndex;
        private int _currentIndex;
        private List<BookModel> _bookModels;

        public void Initialize(List<BookModel> bookModels)
        {
            _bookModels = new List<BookModel>(bookModels);
            _currentIndex = 0;

            _maxIndex = Mathf.Min(5, _bookModels.Count);

            if (_maxIndex > 1)
            {
                _selectorView.SetSelectorActive(_maxIndex);
                SetCheckSwipe(true);
            }
            else
            {
                _selectorView.SetSelectorActive(0);
                SetCheckSwipe(false);
            }

            OnSwiped(0);
        }

        public void Subscribe()
        {
            _allbtn.onClick.AddListener(OnPressAllBtn);
            _readbtn.onClick.AddListener(OnPressReadBtn);
            _swipeController.SwipeAction += OnSwiped;
        }

        public void Unsubscribe()
        {
            _allbtn.onClick.RemoveAllListeners();
            _readbtn.onClick.RemoveAllListeners();
            _swipeController.SwipeAction = null;
        }

        public void SetCheckSwipe(bool value)
        {
            _swipeController.SetCanCheck(value);
        }

        private void SetDescriptionBook(int index)
        {
            _descriptionBook.SetName(_bookModels[index].Name);
            _descriptionBook.SetDescription(_bookModels[index].Description);
            _descriptionBook.SetGenres(_bookModels[index].GenreIndexes);
            _descriptionBook.SetRate(_bookModels[index].Stars);
        }

        private void SetStateSideBooksActive(int index)
        {
            _currentIndex = index;
            
            if (index == 0)
            {
                _sideBooksGameObjects[0].SetActive(false);
                _sideBooksGameObjects[1].SetActive(true);
            }
            else if (index == _maxIndex-1)
            {
                _sideBooksGameObjects[0].SetActive(true);
                _sideBooksGameObjects[1].SetActive(false);
            }
            else
            {
                _sideBooksGameObjects[0].SetActive(true);
                _sideBooksGameObjects[1].SetActive(true);
            }
            
            if(_maxIndex == 1)
            {
                _sideBooksGameObjects[0].SetActive(false);
                _sideBooksGameObjects[1].SetActive(false);
            }
        }

        private void OnSwiped(int direction)
        {
            int newIndex = _currentIndex + direction;

            if (newIndex < 0 || newIndex >= _maxIndex)
            {
                return;
            }

            SetDescriptionBook(newIndex);
            SetStateSideBooksActive(newIndex);
            _selectorView.SetState(newIndex);
        }

        private void OnPressReadBtn()
        {
            SetCheckSwipe(false);
            PressReadBtnAction?.Invoke(_currentIndex);
        }

        private void OnPressAllBtn()
        {
            SetCheckSwipe(false);
            PressAllBtnAction?.Invoke();
        }
    }
}