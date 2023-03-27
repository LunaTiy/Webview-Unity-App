using CodeBase.Webview;
using UnityEngine;

namespace CodeBase.Inputs
{
    public class InputBackReader : MonoBehaviour
    {
        [SerializeField] private WebviewProvider _sampleWebview;

        private WebViewObject _webView;
        private bool _initialized;

        private void OnEnable() => 
            _sampleWebview.WebviewInitialized += WebviewInitializedHandler;

        private void Update()
        {
            if(!_initialized)
                return;
            
            if (Application.platform == RuntimePlatform.Android)
                if (Input.GetKey(KeyCode.Escape) && _webView.CanGoBack())
                    _webView.GoBack();
        }

        private void OnDisable() => 
            _sampleWebview.WebviewInitialized -= WebviewInitializedHandler;

        private void WebviewInitializedHandler(WebViewObject webViewObject)
        {
            _webView = webViewObject;
            _initialized = true;
        }
    }
}
