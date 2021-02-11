using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Objects;
using Bloopy.GameManagement;

namespace Bloopy.Platform
{
    public class PlatformBehavior : ObjectBehaviors
    {
        int platformPower = 0;

        public SpriteRenderer innerFillSpriteRenderer, outerFillSpriteRenderer;
        public ParticleSystem platformParticles;
        public Color platformFillColor = Color.black;
        public int PlatformPower
        {
            get
            {
                return platformPower;
            }
        }

        public void IncreasePlatformPower()
        {
            if (platformPower < 2)
            {
                switch (platformPower)
                {
                    case 0:
                        platformPower = 1;
                        innerFillSpriteRenderer.color = platformFillColor;
                        break;
                    case 1:
                        platformPower = 2;
                        outerFillSpriteRenderer.color = platformFillColor;
                        break;
                }
            }
        }

        protected override void Start()
        {
            base.Start();
            platformFillColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
            var psMain = platformParticles.main;
            psMain.startColor = platformFillColor;
        }
    }
}