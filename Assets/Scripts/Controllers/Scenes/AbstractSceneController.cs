using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using SO;
using Enums;
using Sounds;

namespace Controllers.Scenes
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        [SerializeField] 
        private SoundsController _soundsController;
        [SerializeField]
        private SceneSounds _sceneSounds;

        private MusicController _musicController;

        private void OnEnable()
        {
            _musicController = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>();
            
            _sceneSounds.SetAudioClip();
            
            Initialize();
            Subscribe();
            OnSceneEnable();
        }

        private void Start()
        {
            OnSceneStart();
            PlayMusic();
        }

        private void OnDisable()
        {
            Unsubscribe();
            OnSceneDisable();
        }

        protected abstract void OnSceneEnable();
        protected abstract void OnSceneStart();
        protected abstract void OnSceneDisable();
        protected abstract void Initialize();
        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        protected void LoadScene(string sceneName, bool isSingle = true)
        {
            SetClickClip();
            
            StartCoroutine(DelayLoadScene(sceneName, isSingle));
        }
        
        protected void UnloadScene(string sceneName)
        {
            SetClickClip();
            
            StartCoroutine(DelayCloseScene(sceneName));
        }

        protected void SetClickClip()
        {
            PlaySound(AudioNames.ClickClip.ToString());
        }

        protected void PlaySound(string audioName)
        {
            AudioClip clip = GetAudioClip(audioName);
            
            _soundsController.TryPlaySound(clip);
        }

        protected void PlayMusic()
        {
            AudioClip clip = GetAudioClip(AudioNames.MusicClip.ToString());
            
            _musicController.TryPlayMusic(clip);
        }
        
        private AudioClip GetAudioClip(string clipName)
        {
            return _sceneSounds.GetAudioClipByName(clipName);
        }
        
        private IEnumerator DelayCloseScene(string sceneName)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.UnloadSceneAsync(sceneName);
        }
        
        private IEnumerator DelayLoadScene(string sceneName, bool isSingle)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName, isSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }
    }
}