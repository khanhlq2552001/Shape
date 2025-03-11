using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public class ParticleBreak : MonoBehaviour
    {
        public ParticleSystem particleSystem;

        public void SetColor(Color color)
        {
            particleSystem.startColor = color;
        }

        public void PlayParticle()
        {
            particleSystem.Play();
        }

        public void StopParticle()
        {
            particleSystem.Stop();
        }
    }
}
