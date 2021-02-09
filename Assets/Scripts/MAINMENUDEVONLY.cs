using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Saving;
public class MAINMENUDEVONLY : MonoBehaviour
{
    SaveData saveData = new SaveData();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            saveData = SaveSystem.singleton.LoadGame();
            saveData.ResetHighScore();
            SaveSystem.singleton.SaveGame(saveData);
        }
    }
}
