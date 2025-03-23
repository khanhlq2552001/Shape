using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class Hammer : MonoBehaviour
    {
        public int hex;

        public void VoNat()
        {
            Color color = GameManager.Instance.dataColors.colorsShape[hex].colorMul;

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
