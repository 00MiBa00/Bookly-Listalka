using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;
using Values;

namespace Models
{
    public class EditModel
    {
        private string _path;
        private int _index;
        private BookModel _changedBook;
        
        private const string SelectedBookIndexKey = "EditModel.SelectedBookIndex";
        
        public bool CanShowAds => !Subscription.HasSubscription();

        public string BookName => _changedBook.Name;
        public string BookDescription => _changedBook.Description;

        public int BookRate => _changedBook.Stars;
        public List<int> GenreIndexes => _changedBook.GenreIndexes;

        public EditModel()
        {
            _path = Path.Combine(Application.persistentDataPath, "Books.Path");
            
            _index = PlayerPrefs.GetInt(SelectedBookIndexKey, 0);

            _changedBook = new BookModel();

            BookModel model = GetBook();

            _changedBook.Name = model.Name;
            _changedBook.Description = model.Description;
            _changedBook.Stars = model.Stars;
            _changedBook.GenreIndexes = model.GenreIndexes;
        }

        public void SetIndex(int index)
        {
            PlayerPrefs.SetInt(SelectedBookIndexKey, index);
        }

        public void ChangeName(string name)
        {
            _changedBook.Name = name;
        }

        public void ChangeDescription(string description)
        {
            _changedBook.Description = description;
        }

        public void ChangeRate(int count)
        {
            _changedBook.Stars = count;
        }
        
        public bool? TryAddGenre(int value)
        {
            if (_changedBook.GenreIndexes.Count > 0)
            {
                bool isInList = _changedBook.GenreIndexes.IndexOf(value) != -1;

                if (isInList)
                {
                    int index = _changedBook.GenreIndexes.IndexOf(value);
                    
                    SubtractGenre(index);

                    return false;
                }
                else
                {
                    return AddGenre(value);
                }
            }
            else
            {
                return AddGenre(value);
            }
        }
        
        private bool? AddGenre(int value)
        {
            if (_changedBook.GenreIndexes.Count == 3)
            {
                return null;
            }

            _changedBook.GenreIndexes.Add(value);

            return true;
        }
        
        private void SubtractGenre(int index)
        {
            _changedBook.GenreIndexes.RemoveAt(index);
        }

        public async Task SaveBook()
        {
            List<BookModel> models = new List<BookModel>(GetBookModels());

            models[_index] = _changedBook;

            await BooksInfo.SaveBookModelAsync(models, _path);
        }
        
        public bool IsCanSave()
        {
            return !string.IsNullOrEmpty(_changedBook.Name) && _changedBook.GenreIndexes.Count > 0 && _changedBook.Stars > 0;
        }

        private BookModel GetBook()
        {
            List<BookModel> models = new List<BookModel>(GetBookModels());

            return models[_index];
        }

        private List<BookModel> GetBookModels()
        {
            List<BookModel> models = new List<BookModel>();

            models.AddRange(BooksInfo.LoadBooks(_path));

            return models;
        }
    }
}