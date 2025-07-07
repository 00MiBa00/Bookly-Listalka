using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class DescriptionBook : MonoBehaviour
    {
        [SerializeField] 
        private List<Sprite> _genreSprites;
        [SerializeField] 
        private List<Image> _genreImages;
        [SerializeField] 
        private List<Image> _rateImages;
        [SerializeField] 
        private List<Sprite> _rateSprites;
        [SerializeField] 
        private Text _bookNameText;
        [SerializeField] 
        private Text _descriptionText;
        [SerializeField] 
        private float _reduce;

        public void SetGenres(List<int> indexes)
        {
            for (int i = 0; i < _genreImages.Count; i++)
            {
                if (i < indexes.Count)
                {
                    _genreImages[i].sprite = _genreSprites[indexes[i]];
                    _genreImages[i].gameObject.SetActive(true);

                    SetSetImageSize(_genreImages[i]);

                    if (i > 0)
                    {
                        SetPos(_genreImages[i].rectTransform, GetPosX(i));
                    }
                }
                else
                {
                    _genreImages[i].gameObject.SetActive(false);
                }
            }
        }

        public void SetRate(int count)
        {
            for (int i = 0; i < _rateImages.Count; i++)
            {
                _rateImages[i].sprite = i < count ? _rateSprites[0] : _rateSprites[1];
            }
        }

        public void SetName(string value)
        {
            _bookNameText.text = value;
        }

        public void SetDescription(string value)
        {
            if (_descriptionText == null)
            {
                return;
            }

            _descriptionText.text = value;
        }

        private void SetSetImageSize(Image image)
        {
            image.SetNativeSize();

            RectTransform rect = image.rectTransform;
            
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width/_reduce);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.rect.height/_reduce);
        }

        private void SetPos(RectTransform rect, float posX)
        {
            rect.anchoredPosition = new Vector2(posX, rect.anchoredPosition.y);
        }

        private float GetPosX(int index)
        {
            float weightPrevImage = _genreImages[index - 1].rectTransform.rect.width;
            float weightCurrentImage = _genreImages[index].rectTransform.rect.width;

            float posX = _genreImages[index - 1].rectTransform.anchoredPosition.x + weightPrevImage / 2 + 2 +
                         weightCurrentImage / 2;

            return posX;
        }
    }
}