using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class Wall : MonoBehaviour
    {
        public Transform centre;
        public List<int> idsItemGrid;

        [SerializeField] private List<CalculateXY> _calculateXies;
        [SerializeField] private List<GameObject> _listMasks;
        [SerializeField] private SpriteRenderer _spr;
        [SerializeField] private List<SpriteRenderer> _sprShadows;
        [SerializeField] private GameObject[] _arrowTopDown = new GameObject[2];
        [SerializeField] private GameObject[] _arrowRightLeft = new GameObject[2];
        [SerializeField] private TypeWall _typeWall;

        private Color _colorLight;
        private Vector3 _posStartSpr = Vector3.zero;

        private int _idGate;

        private void Awake()
        {
            _posStartSpr = _spr.transform.localPosition;
            idsItemGrid.Clear();
            for (int i=0; i< _calculateXies.Count; i++)
            {
                idsItemGrid.Add(0);
            }
        }

        public TypeWall Type
        {
            get => _typeWall;

            set => _typeWall = value;
        }

        public int IDGate
        {
            get => _idGate;

            set => _idGate = value;
        }

        public void SetColor(string strHex)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#" + strHex, out color);
            _spr.color = color;
            _colorLight = color;

            Color darkerColor = new Color(color.r *0.4f, color.g * 0.35f, color.b * 0.35f, color.a);

            for (int i = 0; i < _sprShadows.Count; i++)
            {
                _sprShadows[i].color = darkerColor;
            }
        }

        public void ResetAtribute()
        {
            if (_arrowTopDown[0] != null) _arrowTopDown[0].SetActive(false);
            if (_arrowTopDown[1] != null) _arrowTopDown[1].SetActive(false);
            if (_arrowRightLeft[0] != null) _arrowRightLeft[0].SetActive(false);
            if (_arrowRightLeft[1] != null) _arrowRightLeft[1].SetActive(false);
        }

        public void TweenAnVatThe()
        {
            Color darkerColor = new Color(_colorLight.r *0.4f, _colorLight.g * 0.35f, _colorLight.b * 0.35f, _colorLight.a);
            _spr.DOColor(darkerColor, 0.1f);
            _spr.transform.DOMove(transform.position, 0.1f).SetEase(Ease.InOutSine);

            if(_typeWall == TypeWall.top)
            {
                ParticleBreak par = LeanPool.Spawn(GameManager.Instance.particleBreak);
                par.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f);
                par.transform.rotation = Quaternion.Euler(0, 0, 180);
                par.particleSystem.transform.localScale = 0.3f * new Vector3(_calculateXies.Count, _calculateXies.Count, 0);
                par.SetColor(_colorLight);
                par.PlayParticle();
            }
            else if(_typeWall == TypeWall.bot)
            {
                ParticleBreak par = LeanPool.Spawn(GameManager.Instance.particleBreak);
                par.transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f);
                par.transform.rotation = Quaternion.Euler(0, 0, 0);
                par.particleSystem.transform.localScale = 0.3f * new Vector3(_calculateXies.Count, _calculateXies.Count, 0);
                par.SetColor(_colorLight);
                par.PlayParticle();
            }
            else if (_typeWall == TypeWall.right)
            {
                ParticleBreak par = LeanPool.Spawn(GameManager.Instance.particleBreak);
                par.transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y);
                par.transform.rotation = Quaternion.Euler(0, 0, 90);
                par.particleSystem.transform.localScale = 0.3f * new Vector3(_calculateXies.Count, _calculateXies.Count, 0);
                par.SetColor(_colorLight);
                par.PlayParticle();
            }
            else if (_typeWall == TypeWall.left)
            {
                ParticleBreak par = LeanPool.Spawn(GameManager.Instance.particleBreak);
                par.transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y);
                par.transform.rotation = Quaternion.Euler(0, 0, -90);
                par.particleSystem.transform.localScale = 0.3f * new Vector3(_calculateXies.Count, _calculateXies.Count, 0);
                par.SetColor(_colorLight);
                par.PlayParticle();
            }
        }

        public void TweenNayLen()
        {
            _spr.DOColor(_colorLight, 0.1f);
            _spr.transform.DOLocalMove(_posStartSpr, 0.1f).SetEase(Ease.InOutSine);
        }

        public void SetMask(TypeWall type)
        {
            Type = type;

            for(int i=0; i< _listMasks.Count; i++)
            {
                _listMasks[i].SetActive(false);
            }

            switch (type)
            {
                case TypeWall.top:
                    _listMasks[0].SetActive(true);
                    break;
                case TypeWall.bot:
                    _listMasks[1].SetActive(true);
                    break;
                case TypeWall.left:
                    _listMasks[0].SetActive(true);
                    break;
                case TypeWall.right:
                    _listMasks[1].SetActive(true);
                    break;
            }
        }

        public void SetArrow(TypeWall type)
        {
            switch (type)
            {
                case TypeWall.top:
                    _arrowTopDown[0].SetActive(true);
                    _arrowTopDown[1].SetActive(false);
                    break;
                case TypeWall.bot:
                    _arrowTopDown[1].SetActive(true);
                    _arrowTopDown[0].SetActive(false);
                    break;
                case TypeWall.left:
                    _arrowRightLeft[1].SetActive(true);
                    _arrowRightLeft[0].SetActive(false);
                    break;
                case TypeWall.right:
                    _arrowRightLeft[0].SetActive(true);
                    _arrowRightLeft[1].SetActive(false);
                    break;
            }
        }

        public void CalculatorCoordinates(int id, bool isWall, int iDGate)
        {
            int x = id % LevelManager.Instance.GetData().width;
            int y = id / LevelManager.Instance.GetData().width;
            int newX = 0;
            int newY = 0;

            for (int i = 0; i < _calculateXies.Count; i++)
            {
                newX = x + _calculateXies[i].x;
                newY = y + _calculateXies[i].y;
                idsItemGrid[i] = newY * LevelManager.Instance.GetData().width + newX;
                LevelManager.Instance.GetListItemGrid()[idsItemGrid[i]].GetComponent<ItemGrid>().AddInfoWall(this, _typeWall, iDGate);
            }
        }
    }
}
