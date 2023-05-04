// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JengaGame.Data
{

    public class JsonImporter : MonoBehaviour
    {
        private string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

        void Start()
        {
            ImportJsonAsync();
        }

        async void ImportJsonAsync()
        {
            string json = await ImportJson(url);

            if (!string.IsNullOrEmpty(json))
            {
                // Process the JSON data here
                // For example, you can parse the JSON using JSONUtility or a third-party library

                Debug.Log("JSON imported successfully:\n" + json);
            }
            else
            {
                Debug.LogError("Failed to import JSON");
            }
        }

        async Task<string> ImportJson(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                var operation = webRequest.SendWebRequest();
                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    return webRequest.downloadHandler.text;
                }
                else
                {
                    return null;
                }
            }
        }
        
        
        IEnumerator ImportJson()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string json = webRequest.downloadHandler.text;

                    // Process the JSON data here
                    // For example, you can parse the JSON using JSONUtility or a third-party library

                    Debug.Log("JSON imported successfully:\n" + json);
                }
                else
                {
                    Debug.LogError("Failed to import JSON: " + webRequest.error);
                }
            }
        }
    }
}