using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhloxAPI.Services.VoiceCommandsService
{
    public interface IVoiceCommandsService
    {
        Task<String> GetIntentFromAudioWithParams(String fileUrl, String encoding, int sampleRate, String languageCode);

        Task<String> GetIntentFromAudio(String fileUrl);

    }
}