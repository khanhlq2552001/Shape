using UnityEngine;

namespace Game.MainGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DropShadow : MonoBehaviour
    {
        public Vector2 shadowOffset;
        public Material shadowMaterial;

        SpriteRenderer _spriteRenderer;
        GameObject _shadowGameobject;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            //create a new gameobject to be used as drop shadow
            _shadowGameobject = new GameObject("Shadow");

            //create a new SpriteRenderer for Shadow gameobject
            SpriteRenderer shadowSpriteRenderer = _shadowGameobject.AddComponent<SpriteRenderer>();

            //set the shadow gameobject's sprite to the original sprite
            shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
            //set the shadow gameobject's material to the shadow material we created
            shadowSpriteRenderer.material = shadowMaterial;

            //update the sorting layer of the shadow to always lie behind the sprite
            shadowSpriteRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;
            shadowSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;
        }
    }
}
