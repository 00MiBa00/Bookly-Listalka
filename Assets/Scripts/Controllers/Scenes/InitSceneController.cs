using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

namespace Controllers.Scenes
{
    public class InitSceneController : MonoBehaviour
    {
        private InitModel _model;
        
        private void Awake()
        {
            _model = new InitModel();

            string sceneName = _model.GetNameNextScene();
            
            SceneManager.LoadScene(sceneName);
        }
    }
}