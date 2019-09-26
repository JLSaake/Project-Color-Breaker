using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    static string path = Application.persistentDataPath + "/Game.data";

    public static void SaveGame()
    {
        GameData game = new GameData();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, game);
        stream.Close();

    }

    public static void LoadGame()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData game = formatter.Deserialize(stream) as GameData;
            stream.Close();

            game.UpdatePlayerData();
        }
        else
        {
            // should only ever be called on first instantiation of game, unless save game is never called
            Debug.LogWarning("Save File not found in " + path + " ... Using defaults");
        }
    }


}
