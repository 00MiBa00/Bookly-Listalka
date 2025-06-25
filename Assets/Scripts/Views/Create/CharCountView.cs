using UnityEngine;
using UnityEngine.UI;

namespace Views.Create
{
    public class CharCountView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateCount(int value)
        {
            _text.text = $"{value}/400";
        }
    }
}