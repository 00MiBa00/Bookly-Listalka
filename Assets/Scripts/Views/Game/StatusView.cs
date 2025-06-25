using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class StatusView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateStatus(string value)
        {
            _text.text = value;
        }
    }
}