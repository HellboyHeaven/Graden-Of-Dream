using UnityEngine;

public static class SaveSystem
{
   public static void SaveData<T>(this T saveData) where T : ISaveData
   {
      string json = JsonUtility.ToJson(saveData);
      string path = $"{Application.persistentDataPath}/{saveData.path}";
      Debug.Log(path);
      System.IO.File.WriteAllText(path, json);
   }

   public static T LoadData<T>(this T saveData) where  T : ISaveData
   {
      if (saveData == null) return default;
      if (!saveData.HasFile()) return default;
      string path = $"{Application.persistentDataPath}/{saveData.path}";
      string json = System.IO.File.ReadAllText(path);
      return JsonUtility.FromJson<T>(json);
   }

   public static bool TryLoadData<T>(this T saveData, out T obj) where T : ISaveData
   {
      obj = saveData.LoadData();
      return obj != null;
   }

   public static bool HasFile<T>(this T saveData) where T : ISaveData
   {
      string path = $"{Application.persistentDataPath}/{saveData.path}";
      return System.IO.File.Exists(path);
   }
}
