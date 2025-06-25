using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class SelectorView : MonoBehaviour
    {
        [SerializeField] 
        private List<Image> _selctorImages;
        [SerializeField] 
        private List<Sprite> _selectorSprites;

        public void SetSelectorActive(int index)
        {
            for (int i = 0; i < _selctorImages.Count; i++)
            {
                _selctorImages[i].gameObject.SetActive(i<index);
            }
        }

        public void SetState(int index)
        {
            foreach (var image in _selctorImages)
            {
                image.sprite = _selectorSprites[1];
            }

            _selctorImages[index].sprite = _selectorSprites[0];
        }
    }
}