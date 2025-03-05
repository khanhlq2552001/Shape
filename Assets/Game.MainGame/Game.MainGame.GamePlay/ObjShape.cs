using UnityEngine;

namespace Game.MainGame
{
    public class ObjShape : MonoBehaviour
    {
        private bool _canMove = true;

        public Transform tranCentre;
        public Rigidbody2D rb;

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
