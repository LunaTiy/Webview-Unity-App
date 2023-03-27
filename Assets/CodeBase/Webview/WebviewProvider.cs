/*
 * Copyright (C) 2012 GREE, Inc.
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#if UNITY_2018_4_OR_NEWER
#endif

namespace CodeBase.Webview
{
    public class WebviewProvider : MonoBehaviour
    {
        public event Action<WebViewObject> WebviewInitialized;

        public string Url;
        public Text status;

        [SerializeField] private bool _isRenderNavigationBar;

        private WebViewObject _webView;

        private IEnumerator Start()
        {
            InitializeWebview();
            
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
            // cf. https://github.com/gree/unity-webview/pull/512
            // Added alertDialogEnabled flag to enable/disable alert/confirm/prompt dialogs. by KojiNakamaru · Pull Request #512 · gree/unity-webview
            //webViewObject.SetAlertDialogEnabled(false);

            // cf. https://github.com/gree/unity-webview/pull/728
            //webViewObject.SetCameraAccess(true);
            //webViewObject.SetMicrophoneAccess(true);

            // cf. https://github.com/gree/unity-webview/pull/550
            // introduced SetURLPattern(..., hookPattern). by KojiNakamaru · Pull Request #550 · gree/unity-webview
            //webViewObject.SetURLPattern("", "^https://.*youtube.com", "^https://.*google.com");

            // cf. https://github.com/gree/unity-webview/pull/570
            // Add BASIC authentication feature (Android and iOS with WKWebView only) by takeh1k0 · Pull Request #570 · gree/unity-webview
            //webViewObject.SetBasicAuthInfo("id", "password");

            //webViewObject.SetScrollbarsVisibility(true);

            _webView.SetMargins(0, 0, 0, 0);
            _webView
                .SetTextZoom(
                    100); // android only. cf. https://stackoverflow.com/questions/21647641/android-webview-set-font-size-system-default/47017410#47017410
            _webView.SetVisibility(true);

#if !UNITY_WEBPLAYER && !UNITY_WEBGL
            if (Url.StartsWith("http"))
            {
                _webView.LoadURL(Url.Replace(" ", "%20"));
            }
            else
            {
                string[] exts = new string[]
                {
                    ".jpg",
                    ".js",
                    ".html" // should be last
                };
                foreach (string ext in exts)
                {
                    string url = Url.Replace(".html", ext);
                    string src = System.IO.Path.Combine(Application.streamingAssetsPath, url);
                    string dst = System.IO.Path.Combine(Application.persistentDataPath, url);
                    byte[] result = null;
                    if (src.Contains("://"))
                    {
                        // for Android
#if UNITY_2018_4_OR_NEWER
                        // NOTE: a more complete code that utilizes UnityWebRequest can be found in https://github.com/gree/unity-webview/commit/2a07e82f760a8495aa3a77a23453f384869caba7#diff-4379160fa4c2a287f414c07eb10ee36d
                        var unityWebRequest = UnityWebRequest.Get(src);
                        yield return unityWebRequest.SendWebRequest();
                        result = unityWebRequest.downloadHandler.data;
#else
                    var www = new WWW(src);
                    yield return www;
                    result = www.bytes;
#endif
                    }
                    else
                    {
                        result = System.IO.File.ReadAllBytes(src);
                    }

                    System.IO.File.WriteAllBytes(dst, result);
                    if (ext == ".html")
                    {
                        _webView.LoadURL("file://" + dst.Replace(" ", "%20"));
                        break;
                    }
                }
            }
#else
        if (Url.StartsWith("http")) {
            webViewObject.LoadURL(Url.Replace(" ", "%20"));
        } else {
            webViewObject.LoadURL("StreamingAssets/" + Url.Replace(" ", "%20"));
        }
#endif
            yield break;
        }

        private void OnGUI()
        {
            if (!_isRenderNavigationBar)
                return;

            int x = 10;

            GUI.enabled = _webView.CanGoBack();
            if (GUI.Button(new Rect(x, 10, 80, 80), "<")) _webView.GoBack();

            GUI.enabled = true;
            x += 90;

            GUI.enabled = _webView.CanGoForward();
            if (GUI.Button(new Rect(x, 10, 80, 80), ">")) _webView.GoForward();

            GUI.enabled = true;
            x += 90;

            if (GUI.Button(new Rect(x, 10, 80, 80), "r")) _webView.Reload();

            x += 90;

            GUI.TextField(new Rect(x, 10, 180, 80), "" + _webView.Progress());
            x += 190;

            if (GUI.Button(new Rect(x, 10, 80, 80), "*"))
            {
                var g = GameObject.Find("WebViewObject");
                if (g != null)
                    Destroy(g);
                else
                    StartCoroutine(Start());
            }

            x += 90;

            if (GUI.Button(new Rect(x, 10, 80, 80), "c")) _webView.GetCookies(Url);

            x += 90;

            if (GUI.Button(new Rect(x, 10, 80, 80), "x")) _webView.ClearCookies();

            x += 90;

            if (GUI.Button(new Rect(x, 10, 80, 80), "D")) _webView.SetInteractionEnabled(false);

            x += 90;

            if (GUI.Button(new Rect(x, 10, 80, 80), "E")) _webView.SetInteractionEnabled(true);

            x += 90;
        }

        private void InitializeWebview()
        {
            _webView = new GameObject("WebViewObject").AddComponent<WebViewObject>();

            _webView.Init(
                (msg) =>
                {
                    Debug.Log($"CallFromJS[{msg}]");
                    status.text = msg;
                    status.GetComponent<Animation>().Play();
                },
                (msg) =>
                {
                    Debug.Log($"CallOnError[{msg}]");
                    status.text = msg;
                    status.GetComponent<Animation>().Play();
                },
                (msg) =>
                {
                    Debug.Log($"CallOnHttpError[{msg}]");
                    status.text = msg;
                    status.GetComponent<Animation>().Play();
                },
                started: (msg) => { Debug.Log($"CallOnStarted[{msg}]"); },
                hooked: (msg) => { Debug.Log($"CallOnHooked[{msg}]"); },
                cookies: (msg) => { Debug.Log($"CallOnCookies[{msg}]"); },
                ld: (msg) =>
                {
                    Debug.Log($"CallOnLoaded[{msg}]");
#if UNITY_EDITOR_OSX || (!UNITY_ANDROID && !UNITY_WEBPLAYER && !UNITY_WEBGL)
                // NOTE: depending on the situation, you might prefer
                // the 'iframe' approach.
                // cf. https://github.com/gree/unity-webview/issues/189
#if true
                webViewObject.EvaluateJS(@"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } else {
                    window.Unity = {
                      call: function(msg) {
                        window.location = 'unity:' + msg;
                      }
                    }
                  }
                ");
#else
                webViewObject.EvaluateJS(@"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } else {
                    window.Unity = {
                      call: function(msg) {
                        var iframe = document.createElement('IFRAME');
                        iframe.setAttribute('src', 'unity:' + msg);
                        document.documentElement.appendChild(iframe);
                        iframe.parentNode.removeChild(iframe);
                        iframe = null;
                      }
                    }
                  }
                ");
#endif
#elif UNITY_WEBPLAYER || UNITY_WEBGL
                webViewObject.EvaluateJS(
                    "window.Unity = {" +
                    "   call:function(msg) {" +
                    "       parent.unityWebView.sendMessage('WebViewObject', msg)" +
                    "   }" +
                    "};");
#endif
                    _webView.EvaluateJS(@"Unity.call('ua=' + navigator.userAgent)");
                }
                //transparent: false,
                //zoom: true,
                //ua: "custom user agent string",
                //radius: 0,  // rounded corner radius in pixel
                //// android
                //androidForceDarkMode: 0,  // 0: follow system setting, 1: force dark off, 2: force dark on
                //// ios
                //enableWKWebView: true,
                //wkContentMode: 0,  // 0: recommended, 1: mobile, 2: desktop
                //wkAllowsLinkPreview: true,
                //// editor
                //separated: false
            );
            
            WebviewInitialized?.Invoke(_webView);
        }
    }
}