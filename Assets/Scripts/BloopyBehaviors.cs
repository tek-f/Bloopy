using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.GameManagement;

namespace Bloopy.Player
{
    public class BloopyBehaviors : MonoBehaviour
    {
        Rigidbody2D playerRigidbody;
        float launchForce = 25.0f;
        public float screenRange;
        public void Launch()
        {
            playerRigidbody.AddForce(new Vector2(0, launchForce), ForceMode2D.Impulse);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("Platform"))
            {
                Destroy(collision.gameObject);
            }
        }
        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (NewGameManager.singleton.GamePlaying)
            {
                if (transform.position.x > screenRange)
                {
                    Vector3 tempPos = transform.position;
                    tempPos.x = -screenRange + 0.01f;
                    transform.position = tempPos;
                }
                if (transform.position.x < -screenRange)
                {
                    Vector3 tempPos = transform.position;
                    tempPos.x = screenRange - 0.01f;
                    transform.position = tempPos;
                }
                if (transform.position.y < Camera.main.transform.position.y - 5.5f)
                {
                    NewGameManager.singleton.EndGame();
                }
            }
        }
    }
}