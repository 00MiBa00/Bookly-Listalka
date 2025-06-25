using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Values;

namespace Models
{
    public class GameModel
    {
        private string _path;
        private int _selectedBookIndex;

        public bool IsVipActive => Subscription.HasSubscription();
        public string StatusString => BooksInfo.Status;
        public int SelectedBookIndex => _selectedBookIndex;

        public bool CanOpenStartPanel => PlayerPrefs.GetInt(LastDayOpenedStartPanelKey, 0) != DateTime.UtcNow.Day;
        public int PhraseIndex => PlayerPrefs.GetInt(PhraseIndexKey, 0);

        private const string LastDayOpenedStartPanelKey = "GameModel.LastDay";
        private const string PhraseIndexKey = "GameModel.PhraseIndex";
        
        public GameModel()
        {
            _path = Path.Combine(Application.persistentDataPath, "Books.Path");
        }

        public void SetSelectedBookIndex(int index)
        {
            _selectedBookIndex = index;
        }

        public void SetIndexPhrase()
        {
            int index = PhraseIndex + 1;

            if (index == 20)
            {
                index = 0;
            }
            
            PlayerPrefs.SetInt(PhraseIndexKey, index);
            PlayerPrefs.SetInt(LastDayOpenedStartPanelKey, DateTime.UtcNow.Day);
        }

        public async Task DeleteBook()
        {
            List<BookModel> models = new List<BookModel>(GetBookModels());
            
            models.RemoveAt(_selectedBookIndex);

            BooksInfo.SaveCount(models.Count);
            await BooksInfo.SaveBookModelAsync(models, _path);
        }

        public List<BookModel> GetBookModels()
        {
            List<BookModel> models = new List<BookModel>();

            models.AddRange(BooksInfo.LoadBooks(_path));

            return models;
        }
    }
}