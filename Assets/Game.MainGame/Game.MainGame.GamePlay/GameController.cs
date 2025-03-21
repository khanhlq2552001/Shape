using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public enum StateController
    {
        Drag,
        NoDrag,
        Pause
    }

    public class GameController : MonoBehaviour
    {
        [SerializeField] private float _speedMove;
        [SerializeField] private float _stopDistance;

        private StateController _stateController = StateController.NoDrag;
        private ObjShape _objShape;
        private Vector2 _touchPosition;

        public StateController StateController
        {
            get
            {
                return _stateController;
            }
            set
            {
                _stateController = value;
            }
        }

        private void Update()
        {
            if (_stateController == StateController.Pause) return;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                _touchPosition = touchPosition;

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider != null)
                    {
                        ObjShape obj = hit.collider.gameObject.GetComponent<ObjShape>();

                        if (obj != null && obj.CanMove)
                        {
                            _objShape = obj;
                            StateController = StateController.Drag;
                            _objShape.SetActiveBorder(true);
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (StateController == StateController.Drag)
                    {
                        MoveToCenter();
                    }
                    else
                    {

                    }
                }
                else if(touch.phase == TouchPhase.Canceled)
                {
                    if (StateController == StateController.Drag)
                    {
                        MoveToCenter();
                    }
                    else
                    {

                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (StateController == StateController.Drag)
            {
                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            Rigidbody2D rbShape = _objShape.rb;
            rbShape.bodyType = RigidbodyType2D.Dynamic;
            rbShape.gravityScale = 0f;
            rbShape.angularDrag = 0f;


            switch (_objShape.TypeLock)
            {
                case TypeBlock.DontBlock:
                    break;
                case TypeBlock.BlockHori:
                    _touchPosition = new Vector2(_touchPosition.x, _objShape.transform.position.y);
                    break;
                case TypeBlock.BlockVer:
                    _touchPosition = new Vector2(_objShape.transform.position.x,_touchPosition.y);
                    break;
            }

            float distance = Vector2.Distance((Vector2)_objShape.transform.position, _touchPosition);
            float ratio = (distance/2f) * 20f;

            if (distance > _stopDistance)
            {
                Vector2 direction = (_touchPosition - (Vector2)_objShape.transform.position).normalized;
                Vector2 targetVelocity = direction *ratio;
                rbShape.velocity = targetVelocity;
            }
            else
            {
                rbShape.velocity = Vector2.zero; // Dừng lại
            }
            CheckIdShape();
        }

        private void MoveToCenter()
        {
            StateController = StateController.NoDrag;
            _objShape.SetActiveBorder(false);
            List<GameObject> listItemGrid = LevelManager.Instance.GetListItemGrid();
            float distanceMin = float.MaxValue;
            GameObject objCentre =  listItemGrid[0];

            for (int i=0; i< listItemGrid.Count; i++)
            {
                float distance = Vector2.Distance((Vector2)listItemGrid[i].transform.position, (Vector2)_objShape.tranCentre.position);

                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    objCentre = listItemGrid[i];
                }
            }

            Vector2 offset = _objShape.tranCentre.localPosition;
            Vector2 posTarget = (Vector2)objCentre.transform.position - offset;

            StartCoroutine(MoveToTargetCoroutine(_objShape, posTarget, 10f));
        }

        private void CheckIdShape()
        {
            List<GameObject> listItemGrid = LevelManager.Instance.GetListItemGrid();
            float distanceMin = float.MaxValue;
            GameObject objCentre =  listItemGrid[0];

            for (int i = 0; i < listItemGrid.Count; i++)
            {
                float distance = Vector2.Distance((Vector2)listItemGrid[i].transform.position, (Vector2)_objShape.tranCentre.position);

                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    objCentre = listItemGrid[i];
                }
            }

            _objShape.CalculatorCoordinates(objCentre.GetComponent<ItemGrid>().ID);
        }

        IEnumerator MoveToTargetCoroutine(ObjShape shape,Vector2 destination, float speed)
        {
            shape.CanMove = false;
            Rigidbody2D rb = shape.rb;
            Vector2 direction = (destination - (Vector2)shape.transform.position).normalized;

            while (Vector2.Distance(shape.transform.position, destination) > 0.1f)
            {
                rb.velocity = direction * speed;
                yield return null;
            }

            shape.transform.position = destination;
            shape.CanMove = true;
            shape.rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            CheckIdShape();
        }
    }
}
