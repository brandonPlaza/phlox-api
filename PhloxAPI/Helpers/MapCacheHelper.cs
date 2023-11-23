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
  // I am very proud of this class :)
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

    public static bool DoesCacheExist(){
      var pathToCache = BuildPathToCache();
      return File.Exists(pathToCache) ? true : false;
    }

    public static void BuildInitialCache(){
      DateTime minDate = DateTime.MinValue;
      MapCache cache = new MapCache(){
        LastUpdate = minDate,
        Nodes = new Dictionary<string, NodeCacheDTO>(),
        Connections = new Dictionary<string, ConnectionCacheDTO>()
      };
      WriteToCache(cache);
    }
  }
}