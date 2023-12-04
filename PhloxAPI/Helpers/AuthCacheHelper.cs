using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Helpers
{
  // Brandon is very proud of this class :)
  public static class AuthCacheHelper
  {
    private const string pathToCacheWindows = @".\Cache\ssak.json";
    private const string pathToCacheLinux = "./Cache/ssak.json";

    public static string BuildPathToCache()
    {
      string pathToCache = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? pathToCacheLinux : pathToCacheWindows;
      string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
      var pathToFile = Path.Combine(currentDirectory, pathToCache);
      return Path.GetFullPath(pathToFile);
    }

    public static Dictionary<string, string> PullCache()
    {
      var cachePath = BuildPathToCache();

      string jsonString = File.ReadAllText(cachePath);

      Dictionary<string, string> cache = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString)!;
      if (cache != null)
      {
        return cache;
      }
      else return new Dictionary<string, string>();
    }
  }
}