using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhloxAPI.Services.VoiceCommandsService;

namespace PhloxAPI.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoiceCommandsController : ControllerBase
    {
        private readonly IVoiceCommandsService _voiceCommandsService;
        private readonly ILogger<VoiceCommandsController> _logger;

        public VoiceCommandsController(ILogger<VoiceCommandsController> logger, IVoiceCommandsService voiceCommandsService)
        {
            _voiceCommandsService = voiceCommandsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetIntentFromAudioWithParams(String fileUrl, String encoding = "LINEAR16", int sampleRate = 44100, String languageCode = "en-US")
        {
            var response = await _voiceCommandsService.GetIntentFromAudioWithParams(fileUrl, encoding, sampleRate, languageCode);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetIntentFromAudio(String fileUrl)
        {
            var response = await _voiceCommandsService.GetIntentFromAudio(fileUrl);
            return Ok(response);
        }

    }
}