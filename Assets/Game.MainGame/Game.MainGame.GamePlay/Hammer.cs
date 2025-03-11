using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class Hammer : MonoBehaviour
    {
        public string hex;

        public void VoNat()
        {
            Color color;
            ColorUtility.TryParseHtmlString("#" + hex, out color);
            GameManager.Instance.particleHammer.startColor = color;
            GameManager.Instance.particleHammer.transform.position = transform.position;
            GameManager.Instance.particleHammer.Play();

        }

        public void DestroyOBj()
        {
            LeanPool.Despawn(this);
        }
    }
}
