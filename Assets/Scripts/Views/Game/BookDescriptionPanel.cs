using UnityEngine;
using Views.General;
using Models;

namespace Views.Game
{
    public class BookDescriptionPanel : PanelView
    {
        [SerializeField] 
        private DescriptionBook _descriptionBook;

        public void SetDescription(BookModel model)
        {
            _descriptionBook.SetName(model.Name);
            _descriptionBook.SetDescription(model.Description);
            _descriptionBook.SetGenres(model.GenreIndexes);
            _descriptionBook.SetRate(model.Stars);
        }

        public void SetActiveDeleteBtn(bool value)
        {
            base.Btns[1].interactable = value;
        }
    }
}