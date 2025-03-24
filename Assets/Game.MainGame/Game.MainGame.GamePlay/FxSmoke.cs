using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class FxSmoke : MonoBehaviour
    {
        [SerializeField] private Color _color2;

        private ParticleSystem _par;

        private void Awake()
        {
            _par = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            Invoke("Pool", 1f);
        }

        private void Pool()
        {
            LeanPool.Despawn(gameObject);
        }

        public void SetColor(Color color)
        {
            var main = _par.main;
            main.startColor = color;
        }

    }
}
