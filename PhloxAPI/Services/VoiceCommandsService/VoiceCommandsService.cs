using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using Google.Cloud.Speech.V1;
using Google.Apis.Auth.OAuth2;
using System.Runtime.InteropServices;
using Google.Apis.SQLAdmin.v1;
using PhloxAPI.Helpers;

/**
 * NOTES TO MY FUTURE SELF, this was genuinely impossible to figure out due to lack of documentation surrounding the dotnet google cloud api.
    Here are the links that have helped me the most and the process I used:
    https://developers.google.com/identity/protocols/oauth2/service-account this was helpful; follow the Java examples, the dotnet libraries follow similar conventions.
    https://developers.google.com/api-client-library/dotnet/apis Here's where to find what libraries need to be installed for what API (click on the version number for the desired library; I looked at what was imported in Java examples and found ones with the same/similar names)
    https://developers.google.com/api-client-library/dotnet/apis/sqladmin/v1 this wasn't very helpful but technically here is the dotnet API documentation
    https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Speech.V1/latest documentation thats similar to below
    https://codelabs.developers.google.com/codelabs/cloud-speech-text-csharp#4 coding lab that shows basic code for speech to text (WITHOUT CREDENTIALS)

    How this works:
    You need to create (or use the same one I'm using) a valid service account key JSON from a service account that has permissions for the task. In this case the permissions the service account has are:
        - Cloud Speech Administrator
        - Editor
        - Service Account Token Creator

    That key will be downloaded into a file if you do it from the GCP Console or from gcloud (CLI). Place that file on the server somewhere that the project can access it (NOTE: do NOT have this file somehwere insecure ex. in code repositories from having it be git-tracked). That will be the "keyPath" variable that's used. From there you should be able to just copy what's done here.

    Keep in mind for other services it seems they all have "builders" (ex SpeechClient has SpeechClientBuilder). Use these instead of the default Create() methods to be able to set parameters (including but not limited to GoogleCredential). You can read the actual implementation code to see what those parameters are and what they accept (either through IDE or by the following link, which appears to show the builder format for all API Client objects)
    https://github.com/googleapis/gax-dotnet/blob/6f2d3e64dd92f0f7a4f02a7db56cf6ed409615f2/Google.Api.Gax.Grpc/ClientBuilderBase.cs
*/

namespace PhloxAPI.Services.VoiceCommandsService
{
    //TODO: Look at the speechContext -> boost & phrases fields in the JSON to hone in on certain words like "help", "at", "settings", etc.
    //https://cloud.google.com/speech-to-text/docs/speech-to-text-requests
    public class VoiceCommandsService : IVoiceCommandsService
    {
        public async Task<String> GetTranscriptFromAudio(String fileUrl)
        {
            //local path to private key
            //var keyPath = "D:\\Capstone\\speech-service-account-key.json";

            //cached path
            var keyPath = AuthCacheHelper.BuildPathToCache();

            using FileStream fs = File.OpenRead(keyPath);

            GoogleCredential credential = GoogleCredential.FromStream(fs).CreateScoped(new string[] { SQLAdminService.ScopeConstants.SqlserviceAdmin });

            var speech = new SpeechClientBuilder
            {
                GoogleCredential = credential,

            }.Build();

            //var speech = SpeechClient.Create();
            var config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 44100,
                LanguageCode = LanguageCodes.English.UnitedStates
            };


            var audio = RecognitionAudio.FromStorageUri(fileUrl);

            var response = speech.Recognize(config, audio);

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }

            var transcript = response.Results[0].Alternatives[0].Transcript;

            return transcript;

        }

        public Task<string> GetTranscriptFromAudioWithParams(string fileUrl, string encoding, int sampleRate, string languageCode)
        {
            throw new NotImplementedException();
        }
    }
}