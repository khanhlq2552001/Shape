using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class Magic : MonoBehaviour
    {
        public Transform tranStart;

        private Animator _anim;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void End()
        {
            _anim.SetBool("end", true);
        }

        public void Pool()
        {
            LeanPool.Despawn(gameObject);
        }
    }
}
