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
        public PlatformBehavior[] platforms;
        //void SpawnPlatform(Vector3 _platformPosition, float _platformZRotation)
        //{
        //    _platformPosition.z = 0;
        //    Transform newPlatform = Instantiate(platformPrefab).transform;
        //    newPlatform.position = _platformPosition;
        //    newPlatform.localEulerAngles = new Vector3(0, 0, _platformZRotation);
        //}
        public void SpawnPlatform(Vector3 _clickPos)
        {
            //Calculate platforms rotation
            float platformRotation = (_clickPos.x / 3) * 45;
            //Calculate platforms position
            Vector3 _platformPosition = _clickPos;
            _platformPosition.z = 0;

            //Spawn platform
            Transform newPlatform = Instantiate(platformPrefab).transform;

            //Set position and rotation
            newPlatform.position = _platformPosition;
            newPlatform.localEulerAngles = new Vector3(0, 0, platformRotation);

            int tempPlatformTracker = 0;//Used to track which platform is going to be replaced by the new platform, if any

            //For each platform in platforms[]
            for (int i = 0; i < 3; i++)
            {
                //If platforms[i] is null
                if(platforms[i] == null)
                {
                    //Set platforms[i] to newPlatform
                    platforms[i] = newPlatform.GetComponent<PlatformBehavior>();

                    newPlatform.GetComponent<PlatformBehavior>().platformsIndex = i;
                    //End function
                    Debug.Log("Empty platforms[] spot found.");
                    return;
                }
                //Else if platforms[i] was spawned before paltforms[tempPlatformTracker]
                else if(platforms[i].spawnTimestamp < platforms[tempPlatformTracker].spawnTimestamp)
                {
                    tempPlatformTracker = i;
                }
            }

            //Destroy old platform
            Destroy(platforms[tempPlatformTracker].gameObject);
            //Replace platforms[tempPlatformTracker] with newPlatform
            platforms[tempPlatformTracker] = newPlatform.GetComponent<PlatformBehavior>();

            newPlatform.GetComponent<PlatformBehavior>().platformsIndex = tempPlatformTracker;
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

            platforms = new PlatformBehavior[3];
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