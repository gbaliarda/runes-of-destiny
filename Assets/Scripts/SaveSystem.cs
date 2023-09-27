using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveSettings(SettingsManager settingsManager)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings";
        Debug.Log($"Saving to {path}: {settingsManager.FullScreen}, {settingsManager.Volume}, {settingsManager.Resolution.width}");
        FileStream fileStream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settingsManager);

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            SettingsData data = binaryFormatter.Deserialize(fileStream) as SettingsData;
            fileStream.Close();

            return data;
        } else
        {
            Debug.Log($"Settings file not found in {path}");
            return null;
        }
    }
}
