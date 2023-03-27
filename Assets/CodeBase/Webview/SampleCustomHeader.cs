using UnityEngine;

namespace CodeBase.Webview
{
    /// <summary>
    /// Example for Custom header features.
    /// If you want to try custom headers, put this class as a component to the scene.
    /// </summary>
    public class SampleCustomHeader : MonoBehaviour
    {
        private const float BUTTON_HEIGHT = 50.0f;
        private const string CUSTOM_HEADER_KEY_NAME = "custom_timestamp";

        private WebViewObject _webviewObject;

        private void OnGUI()
        {
            float h = Screen.height;
            
            if (GUI.Button(new Rect(.0f, h - BUTTON_HEIGHT, Screen.width, BUTTON_HEIGHT), "check for request header"))
            {
                _webviewObject = GameObject.Find("WebViewObject").GetComponent<WebViewObject>();
                _webviewObject.LoadURL("http://httpbin.org/headers");
            }

            h -= BUTTON_HEIGHT;

            if (GUI.Button(new Rect(.0f, h - BUTTON_HEIGHT, Screen.width, BUTTON_HEIGHT), "add custom header"))
            {
                _webviewObject = GameObject.Find("WebViewObject").GetComponent<WebViewObject>();
                _webviewObject.AddCustomHeader(CUSTOM_HEADER_KEY_NAME, System.DateTime.Now.ToString());
            }

            h -= BUTTON_HEIGHT;

            if (GUI.Button(new Rect(.0f, h - BUTTON_HEIGHT, Screen.width, BUTTON_HEIGHT), "get custom header"))
            {
                _webviewObject = GameObject.Find("WebViewObject").GetComponent<WebViewObject>();
                Debug.Log("custom_timestamp is " + _webviewObject.GetCustomHeaderValue(CUSTOM_HEADER_KEY_NAME));
            }

            h -= BUTTON_HEIGHT;

            if (GUI.Button(new Rect(.0f, h - BUTTON_HEIGHT, Screen.width, BUTTON_HEIGHT), "remove custom header"))
            {
                _webviewObject = GameObject.Find("WebViewObject").GetComponent<WebViewObject>();
                _webviewObject.RemoveCustomHeader(CUSTOM_HEADER_KEY_NAME);
            }

            h -= BUTTON_HEIGHT;

            if (GUI.Button(new Rect(.0f, h - BUTTON_HEIGHT, Screen.width, BUTTON_HEIGHT), "clear custom header"))
            {
                _webviewObject = GameObject.Find("WebViewObject").GetComponent<WebViewObject>();
                _webviewObject.ClearCustomHeader();
            }

            h -= BUTTON_HEIGHT;
        }
    }
}