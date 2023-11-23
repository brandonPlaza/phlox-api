using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Helpers
{
  public static class MapCacheHelper
  {
    private const string pathToCache = @"..\..\..\Cache\updatecache.json";
    
    private static string BuildPathToCache(){
      string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
      var pathToFile = System.IO.Path.Combine(currentDirectory, pathToCache);
      var finalPath = Path.GetFullPath(pathToFile);

      return finalPath;
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