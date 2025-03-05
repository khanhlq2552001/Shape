using UnityEngine;

namespace Game.MainGame
{
    public class Shape : MonoBehaviour
    {
        private bool _canMove = true;

        public Rigidbody2D rb;
        public ObjShape objShape;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public bool CanMove
        {
            get => _canMove;

            set => _canMove = value;
        }
    }
}
