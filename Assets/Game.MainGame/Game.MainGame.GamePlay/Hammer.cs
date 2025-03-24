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
            float scale = GameManager.Instance.cameraMain.orthographicSize / 5f;
            ParticleSystem _par = LeanPool.Spawn(GameManager.Instance.particleHammer, transform.position, Quaternion.identity);
            _par.transform.position = new Vector3(_par.transform.position.x, _par.transform.position.y, -2f);
            _par.transform.localScale = new Vector3(scale * 1.2f, scale * 1.2f, scale * 1.2f);
            _par.Play();
            _par.GetComponent<FxSmoke>().SetColor(color);
        }

        public void DestroyOBj()
        {
            LeanPool.Despawn(this);
        }
    }
}
