using UnityEngine;
using UnityEngine.UI;

#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Core.WTS.PPTerms
{
    [RequireComponent(typeof(Button))]
    public class OpenUrlButton : MonoBehaviour
    {
        [SerializeField] private string _url;
        
        private Button button;
        
	#if UNITY_IOS && !UNITY_EDITOR
    	[DllImport("__Internal")]
    	private static extern void OpenURL(string url);
    	#endif

        private void Awake()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
        	#if UNITY_IOS && !UNITY_EDITOR
        	OpenURL(_url);
        	#else
        	Application.OpenURL(_url);
        	#endif
            });
        }
    }
}
