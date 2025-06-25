using System.Collections.Generic;
using System.Threading.Tasks;
using Values;

namespace Models
{
    public class CreateBookModel
    {
        private string _bookName;
        private string _description;
        private string _path;

        private int _rateCount;
        private List<int> _selectedGenres;

        public bool IsBackBtnActive => BooksInfo.HaveBooks;
        public bool CanShowAds => !Subscription.HasSubscription();

        public CreateBookModel()
        {
            _bookName = "";
            _description = "";

            _rateCount = 0;
            _selectedGenres = new List<int>();
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        public void UpdateBookName(string name)
        {
            _bookName = name;
        }
        
        public void UpdateDescription(string description)
        {
            _description = description;
        }

        public void UpdateRate(int count)
        {
            _rateCount = count;
        }

        public async Task SaveBook()
        {
            BookModel model = new BookModel
            {
                Name = _bookName,
                Description = _description,
                GenreIndexes = new List<int>(_selectedGenres),
                Stars = _rateCount
            };
            
            List<BookModel> models = BooksInfo.LoadBooks(_path);
            models.Add(model);
            BooksInfo.SaveCount(models.Count);
            await BooksInfo.SaveBookModelAsync(models, _path);
        }

        public bool? TryAddGenre(int value)
        {
            if (_selectedGenres.Count > 0)
            {
                bool isInList = _selectedGenres.IndexOf(value) != -1;

                if (isInList)
                {
                    int index = _selectedGenres.IndexOf(value);
                    
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

        public bool IsCanSave()
        {
            return !string.IsNullOrEmpty(_bookName) && _selectedGenres.Count > 0 && _rateCount > 0;
        }

        private bool? AddGenre(int value)
        {
            if (_selectedGenres.Count == 3)
            {
                return null;
            }

            _selectedGenres.Add(value);

            return true;
        }

        private void SubtractGenre(int index)
        {
            _selectedGenres.RemoveAt(index);
        }
    }
}