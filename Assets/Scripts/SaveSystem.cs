﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bloopy.Saving
{

    [System.Serializable]
    public class SaveData
    {
        public int highScore = 0;
        public bool firstTimePlaying = true;

        public void ToggleFTP()
        {
            if (firstTimePlaying)
            {
                firstTimePlaying = false;
            }
            else
            {
                firstTimePlaying = true;
            }
        }

        public void SetHighScore(int _newHighScore)
        {
            highScore = _newHighScore;
        }

        public void ResetHighScore()
        {
            highScore = 0;
            Debug.Log("High score has been reset.");
        }
    }


    public class SaveSystem : MonoBehaviour
    {
        static public SaveSystem singleton;

        public void SaveGame(SaveData _saveData)
        {
            string filePath = Application.persistentDataPath + "/saveData.tsf";

            FileStream file;

            if(File.Exists(filePath))
            {
                file = File.OpenWrite(filePath);
            }
            else
            {
                file = File.Create(filePath);
            }

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, _saveData);

            file.Close();
        }

        public SaveData LoadGame()
        {
            string filePath = Application.persistentDataPath + "/saveData.tsf";

            if (File.Exists(filePath))
            {
                FileStream dataStream = new FileStream(filePath, FileMode.Open);

                BinaryFormatter converter = new BinaryFormatter();
                SaveData saveData = converter.Deserialize(dataStream) as SaveData;

                dataStream.Close();
                return saveData;
            }
            else
            {
                Debug.LogError("No save file found in " + filePath);
                return null;
            }
        }

        #region Test Methods DO NOT USE
        //public void DeleteSaveFile()
        //{
        //    string filePath = Application.persistentDataPath + "/saveData.tsf";

        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //        Debug.Log("File " + filePath + " has been deleted.");
        //    }
        //    else
        //    {
        //        Debug.LogError("No file at " + filePath + " has been found.");
        //    }
        //}

        //public void CheckIfFileExists()
        //{

        //    string filePath = Application.persistentDataPath + "/saveData.tsf";

        //    if (File.Exists(filePath))
        //    {
        //        Debug.Log("File " + filePath + " does exist.");
        //        return;
        //    }
        //    Debug.Log("File " + filePath + " does not exist.");
        //}
        #endregion

        private void Awake()
        {
            #region Singleton
            if (singleton == null)
            {
                singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion

            string filePath = Application.persistentDataPath + "/saveData.tsf";

            if (!File.Exists(filePath))
            {
                SaveData saveData = new SaveData();
                SaveGame(saveData);
            }
        }
    }
}