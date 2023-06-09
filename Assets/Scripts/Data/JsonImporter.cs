// ------------------------
// Onur Ereren - May 2023
// ------------------------

// JsonImporter imports the json file at a given URL and returns the result.

using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace JengaGame.Data
{

    public class JsonImporter
    {
        #region METHODS
        
        public async Task<string> ImportJsonAsync(string url)
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

        public async Task<string> ImportJson(string url)
        {
            string json = await ImportJsonAsync(url);

            if (!string.IsNullOrEmpty(json))
            {
                Debug.Log("JSON imported successfully:\n" + json);
                return json;
            }
            else
            {
                Debug.LogError("Failed to import JSON");
                return null;
            }
        }
        
        #endregion
    }
}