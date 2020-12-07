using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.GameManagement
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] Transform bloopy;
        public float minYPos, maxYPos;

        private void Start()
        {
            minYPos = transform.position.y;
            maxYPos = 25;
        }
        void Update()
        {
            if(bloopy.position.y > minYPos && bloopy.position.y < maxYPos)
            {
                Vector3 newPos = transform.position;
                newPos.y = bloopy.transform.position.y;
                transform.position = newPos;
            }
        }
    }
}