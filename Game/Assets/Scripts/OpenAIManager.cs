// using System;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using UnityEngine;

// public class OpenAIManager : MonoBehaviour
// {
//     private static string apiKey;
//     private static string apiUrl;
//     private static string apiDeployment;
//     private static string apiVersion;

//     private static HttpClient client;

//     void Awake()
//     {
//         // Charger les variables d'environnement depuis le fichier .env situé dans le répertoire parent de Game/
//         string envFilePath = Application.dataPath + "/../../.env"; // Ajustez ce chemin selon la structure de votre projet
//         EnvLoader.LoadEnv(envFilePath);

//         apiKey = Environment.GetEnvironmentVariable("AZURE_API_KEY");
//         apiUrl = Environment.GetEnvironmentVariable("AZURE_API_URL");
//         apiDeployment = Environment.GetEnvironmentVariable("AZURE_API_DEPLOYMENT");
//         apiVersion = Environment.GetEnvironmentVariable("AZURE_API_VERSION");

//         if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(apiDeployment) || string.IsNullOrEmpty(apiVersion))
//         {
//             Debug.LogError("One or more environment variables are not set.");
//             return;
//         }

//         client = new HttpClient();
//         client.DefaultRequestHeaders.Add("api-key", apiKey);
//     }

//     public async Task<string> GenerateNPCResponse(string context)
//     {
//         try
//         {
//             if (client == null)
//             {
//                 Debug.LogError("HttpClient is null");
//                 return "HttpClient is null";
//             }

//             var requestBody = new
//             {
//                 prompt = $"{context}\nNPC:",
//                 max_tokens = 150,
//                 temperature = 0.7,
//             };

//             var content = new StringContent(JsonUtility.ToJson(requestBody), Encoding.UTF8, "application/json");

//             string requestUri = $"{apiUrl}openai/deployments/{apiDeployment}/completions?api-version={apiVersion}";
//             var response = await client.PostAsync(requestUri, content);
//             if (response == null)
//             {
//                 Debug.LogError("Response is null");
//                 return "Response is null";
//             }

//             response.EnsureSuccessStatusCode();

//             var responseBody = await response.Content.ReadAsStringAsync();
//             if (string.IsNullOrEmpty(responseBody))
//             {
//                 Debug.LogError("Response body is null or empty");
//                 return "Response body is null or empty";
//             }

//             var result = JsonUtility.FromJson<OpenAIResponse>(responseBody);
//             if (result == null || result.choices == null || result.choices.Length == 0)
//             {
//                 Debug.LogError("Result is null or choices are null or empty");
//                 return "Result is null or choices are null or empty";
//             }

//             return result.choices[0].text.Trim();
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError($"Error generating NPC response: {ex}");
//             return "Erreur lors de la génération de la réponse. Détails de l'erreur : " + ex.ToString();
//         }
//     }

//     [Serializable]
//     private class OpenAIResponse
//     {
//         public Choice[] choices;
//     }

//     [Serializable]
//     private class Choice
//     {
//         public string text;
//     }
// }
