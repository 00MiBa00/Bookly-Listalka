using UnityEngine;

namespace Views.Game
{
    public class VIPView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _vipGo;

        public void SetState(bool value)
        {
            _vipGo.SetActive(value);
        }
    }
}