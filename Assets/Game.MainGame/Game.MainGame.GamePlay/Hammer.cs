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

            ParticleSystem _par = LeanPool.Spawn(GameManager.Instance.particleHammer, transform.position, Quaternion.identity);
            _par.Play();
            _par.GetComponent<FxSmoke>().SetColor(color);
        }

        public void DestroyOBj()
        {
            LeanPool.Despawn(this);
        }
    }
}
