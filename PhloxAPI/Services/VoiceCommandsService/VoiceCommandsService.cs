using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PhloxAPI.Services.VoiceCommandsService
{
    //TODO: Look at the speechContext -> boost & phrases fields in the JSON to hone in on certain words like "help", "at", "settings", etc.
    //https://cloud.google.com/speech-to-text/docs/speech-to-text-requests
    public class VoiceCommandsService : IVoiceCommandsService
    {
        public async Task<String> GetIntentFromAudio(String fileUrl)
        {

            String encoding = "LINEAR16";
            int sampleRate = 44100;
            String languageCode = "en-US";

            var config = new Dictionary<String, dynamic>();

            config.Add("encoding", encoding);
            config.Add("sampleRateHertz", sampleRate);
            config.Add("languageCode", languageCode);
            config.Add("enableWordTimeOffsets", false);

            var audio = new Dictionary<String, String>();

            audio.Add("uri", fileUrl);

            var reqDict = new Dictionary<String, dynamic>();

            reqDict.Add("config", config);
            reqDict.Add("audio", audio);

            String json = JsonConvert.SerializeObject(reqDict);

            Console.WriteLine(json);



            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer ya29.c.c0AY_VpZiLc5FN5C7n7YpjUZDVYpy2FdJdWp8wk-lFuM0O9c2OE6lCqWAKSKi3rAzLXESGHNtJMOdJqgfE78FATagy6PHiw623XHdQi42hfkVp_vyNc5X1AhnoqCT6GRYbS7vg2z0yBJSeznVSSoNTy1DAehnmtu26HbLe9dzYn7ifTf3thGgOjR4OpKxTTNH5lJUxIWBu9BDVDpb9ZoY96OnD7kcgXWCvsXz6y3wo7ZgQajFo3p8zhJYrCrgL0CA0CqnmD5xgnr7GVBoloPGQf_4nGXv-dFISpTiZzE4Fl0JsnCuYJrRcjaYhKVWgkBsMjJY-UtHn5mQ7VirrnVqOpREobQKc-4onYBCWf18WSTq2jAj0mwMX2FkbhkCj4cTUWmoOGbPVElglSEE0UgN0SsHImYMWMUrr-2XoOWUUYRRcU3QGAGYNCsv6__gfscl6v4fU3qTXXMCOTeNuWb1FVTAyYK9budQ2wceqiUDd8wXWPB5Slsm05ivjzt62kIWJ-yeyzpPJMLlaKXQalh1ij4Vu7Q5w6sDWW3mNncfLx3ttKGLWHXSiUhhnS6QonISpwcaSo33IxGCadadEHuroVdduPnIlogDfSyBCvoQtiAoF9yzVmCVPA73R3QN643CxX5I3U91JU90MU5mZR5_oIuIdOMWo0-Wp6lYogpafci39psXMV8v6yR-_bo9Ohbrnk0W0dmry4zryFw4OnwOkiehn5z5IWVYZYkVh5umVQhoXspV0mB7UWg80hJ46sdjvcUYW_mqsQJhfUJiFIRzFnJe5RkVvBlwoUZ7gMFggJXmhQ0lxyg8rZehvjeQ7qdQ06XkYWkZtjXt7omz705I5rh47583atu2-xZRs3Vpjntpo7_twJF9vyZq6eJjM1WacwZFtRWq8x6V4cm1JbWIBpRtZyUXw3enQk8iIse96Rah4-pVokVcdcOuourYjMgIoJ-dBzbdg140eVFMoSxbXRvjcUlluzq18c-vQw9kJntvtpehkih5msq3");
            var response = await client.PostAsync("https://speech.googleapis.com/v1/speech:recognize", new StringContent(json));
            Console.WriteLine(response.ToString());

            // var httpRequestMessage = new HttpRequestMessage
            // {
            //     Method = HttpMethod.Post,
            //     RequestUri = new Uri("https://speech.googleapis.com/v1/speech:recognize"),
            //     Headers = {
            //             { HttpRequestHeader.Authorization.ToString(), "Bearer ca3ab8c844df3a04cf7d18bf5972705736e5e888" },
            //             { HttpRequestHeader.Accept.ToString(), "application/json" },
            //         },
            //     Content = new StringContent(json),
            // };

            // var response = await client.PostAsync("https://speech.googleapis.com/v1/speech:recognize", new StringContent(json));

            // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            // client.DefaultRequestHeaders.Add("Authorization", "Bearer ca3ab8c844df3a04cf7d18bf5972705736e5e888");

            //TODO: Using dependency injection etc. instantiate only one HttpClient for the entire app lifetime
            // see https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#what-is-httpclientfactory
            // using (var httpClient = new HttpClient())
            // {
            //     using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://speech.googleapis.com/v1/speech:recognize"))
            //     {
            //         request.Headers.TryAddWithoutValidation("Authorization", "Bearer ca3ab8c844df3a04cf7d18bf5972705736e5e888");

            //         request.Content = new StringContent(json);
            //         request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            //         response = await httpClient.SendAsync(request);

            //         Console.WriteLine(response);
            //     }
            // }

            return response.Content.ToString();

            /*  curl -X GET \
                -H "Authorization: Bearer $(gcloud auth print-access-token --impersonate-service-account=PRIV_SA)" \
                "https://cloudresourcemanager.googleapis.com/v3/projects/PROJECT_ID"
            */
            /*  curl -s -H "Content-Type: application/json" \
                    -H "Authorization: Bearer "$(gcloud auth application-default print-access-token) \
                    https://speech.googleapis.com/v1/speech:recognize \
                    -d @sync-request.json
            */


            // String script = $"\"C:\\Windows\\System32\\curl.exe\" -curl -s -H \"Content-Type: application/json\" -H \"Authorization: Bearer $(gcloud auth print-access-token --impersonate-service-account=PRIV_SA)\" -d {json}";

            // Process process = new Process(){
            //     StartInfo = new ProcessStartInfo(){
            //         FileName = "cmd",
            //         Arguments = script,
            //     }
            // };

        }

        public Task<string> GetIntentFromAudioWithParams(string fileUrl, string encoding, int sampleRate, string languageCode)
        {
            throw new NotImplementedException();
        }
    }
}