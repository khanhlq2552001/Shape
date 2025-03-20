using System;
using System.Collections;
using BlitzyUI;
using UnityEngine;

namespace Game.MainGame
{
    public class UIFadeScreen : BlitzyUI.Screen
    {
        [SerializeField] private Animator _anim;

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
;       }

        public override void OnPush(Data data)
        {
            PushFinished();
        }

        public override void OnSetup()
        {
        }

        public void SetAction(Action act, bool autoFadeEnd = false)
        {
            StartCoroutine(DelayAction(act, autoFadeEnd));
        }

        public void FadeEnd()
        {
            _anim.SetBool("end", true);
            Invoke("Close", 0.5f);
        }

        IEnumerator DelayAction(Action action, bool auto)
        {
            yield return new WaitForSeconds(0.7f);
            action?.Invoke();
            if (auto)
            {
                FadeEnd();
            }
        }

        private void Close()
        {
            UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_UIFadeScreen);
        }
    }
}
