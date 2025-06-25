using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.Game
{
    public class StartPanel : PanelView
    {
        [SerializeField] 
        private Text _phraseText;
        [SerializeField] 
        private Text _authorText;

        [SerializeField] 
        private List<string> _phrases;
        [SerializeField] 
        private List<string> _authors;

        public void SetPhrase(int index)
        {
            _phraseText.text = _phrases[index];
            _authorText.text = _authors[index];
        }
    }
}