using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Helpers
{
  public static class MapCacheHelper
  {
    private const string pathToCacheWindows = @"\Cache\updatecache.json";
    private const string pathToCacheLinux = "./Cache/updatecache.json";

    private static string BuildPathToCache(){
      string pathToCache = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? pathToCacheLinux : pathToCacheWindows;
      string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
      var pathToFile = Path.Combine(currentDirectory, pathToCache);
      return Path.GetFullPath(pathToFile);
    }

    public static MapCache PullCache(){
      var cachePath = BuildPathToCache();

      string jsonString = File.ReadAllText(cachePath);

      MapCache cache = JsonSerializer.Deserialize<MapCache>(jsonString)!;
      if (cache != null){
        return cache;
      }
      else return new MapCache();
    }

    public static void WriteToCache(MapCache newCache){
      var cachePath = BuildPathToCache();
      
      newCache.LastUpdate = DateTime.Now;

      string jsonCache = JsonSerializer.Serialize(newCache);
      File.WriteAllText(cachePath, jsonCache);
    }
  }
}