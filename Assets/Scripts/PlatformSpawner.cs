using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Platform;

namespace Bloopy.GameManagement
{
    public class PlatformSpawner : MonoBehaviour
    {
        public static PlatformSpawner singleton;

        public GameObject platformPrefab;
        public GameObject platformInstance = null;
        public void SpawnPlatform(Vector3 _clickPos)
        {
            //Determine platforms rotation
            float platformRotation = (_clickPos.x / 3) * 45;

            //Determine platform position
            Vector3 _platformPosition = _clickPos;
            _platformPosition.z = 0;

            //Spawn platform
            Transform newPlatform = Instantiate(platformPrefab).transform;

            //Set position and rotation
            newPlatform.position = _platformPosition;
            newPlatform.localEulerAngles = new Vector3(0, 0, platformRotation);

            //If a platform already exists
            if(platformInstance != null)
            {
                //Destroy old platform
                Destroy(platformInstance.gameObject);
            }

            //Replace old platform with newPlatform
            platformInstance = newPlatform.gameObject;

            #region Old Code
            //int tempPlatformTracker = 0;//Used to track which platform is going to be replaced by the new platform, if any

            ////For each platform in platforms[]
            //for (int i = 0; i < 3; i++)
            //{
            //    //If platforms[i] is null
            //    if(platforms[i] == null)
            //    {
            //        //Set platforms[i] to newPlatform
            //        platforms[i] = newPlatform.GetComponent<PlatformBehavior>();

            //        newPlatform.GetComponent<PlatformBehavior>().platformsIndex = i;
            //        //End function
            //        Debug.Log("Empty platforms[] spot found.");
            //        return;
            //    }
            //    //Else if platforms[i] was spawned before paltforms[tempPlatformTracker]
            //    else if(platforms[i].spawnTimestamp < platforms[tempPlatformTracker].spawnTimestamp)
            //    {
            //        tempPlatformTracker = i;
            //    }
            //}

            //Destroy old platform
            //Destroy(platforms[tempPlatformTracker].gameObject);
            //newPlatform.GetComponent<PlatformBehavior>().platformsIndex = tempPlatformTracker;
            #endregion

        }
        private void Awake()
        {
            if(singleton == null)
            {
                singleton = this;
            }
            else if(singleton != this)
            {
                Destroy(gameObject);
            }

            //platforms = new PlatformBehavior[3];
        }
        //private void Update()
        //{
        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        Debug.Log("click");
        //        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        float platformRotation = 0;
        //        platformRotation = (clickPos.x / 3) * 45;
        //        SpawnPlatform(clickPos, platformRotation);
        //    }
        //}
    }
}