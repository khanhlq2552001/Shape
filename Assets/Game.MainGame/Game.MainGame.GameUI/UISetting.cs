using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UISetting : BlitzyUI.Screen
    {
        [SerializeField] private Sprite _sprOn;
        [SerializeField] private Sprite _sprOff;
        [SerializeField] private Button _btnSound;
        [SerializeField] private Text _txtSound;
        [SerializeField] private Button _btnMusic;
        [SerializeField] private Text _txtMusic;
        [SerializeField] private Button _btnVibration;
        [SerializeField] private Text _txtVibration;
        [SerializeField] private Button _btnNotification;
        [SerializeField] private Text _txtNotification;
        [SerializeField] private Button _btnRestore;
        [SerializeField] private Button _btnLaguage;
        [SerializeField] private Button _btnContinous;
        [SerializeField] private Button _btnQuit;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Text _txtTilePopup;

        public void BtnSound()
        {

        }

        public void BtnMusic()
        {

        }

        public void BtnVibration()
        {

        }

        public void BtnNotification()
        {

        }

        public void BtnRestore()
        {

        }

        public void BtnLanguage()
        {

        }

        public void BtnContinous()
        {
            UIManager.Instance.QueuePop();
        }

        public void BtnQuit()
        {

        }

        public void BtnClose()
        {
            UIManager.Instance.QueuePop();
        }

        public void OpenSetting()
        {
            _btnQuit.gameObject.SetActive(false);
            _btnContinous.gameObject.SetActive(false);
            _btnRestore.gameObject.SetActive(true);
            _btnLaguage.gameObject.SetActive(true);
            _txtTilePopup.text = "Setting";
        }

        public void OpenPause()
        {
            _btnQuit.gameObject.SetActive(true);
            _btnContinous.gameObject.SetActive(true);
            _btnRestore.gameObject.SetActive(false);
            _btnLaguage.gameObject.SetActive(false);
            _txtTilePopup.text = "Pause";
        }

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            PopFinished();
        }

        public override void OnPush(Data data)
        {
            PushFinished();
            GetComponent<Canvas>().overrideSorting = false;
            if(data != null)
            {
                bool isPause = data.Get<bool>("pause");
                if (isPause)
                {
                    OpenPause();
                }
                else
                {
                    OpenSetting();
                }
            }
        }

        public override void OnSetup()
        {
            _btnSound.onClick.AddListener(BtnSound);
            _btnMusic.onClick.AddListener(BtnMusic);
            _btnVibration.onClick.AddListener(BtnVibration);
            _btnNotification.onClick.AddListener(BtnNotification);
            _btnRestore.onClick.AddListener(BtnRestore);
            _btnLaguage.onClick.AddListener(BtnLanguage);
            _btnContinous.onClick.AddListener(BtnContinous);
            _btnQuit.onClick.AddListener(BtnQuit);
            _btnClose.onClick.AddListener(BtnClose);
        }
    }
}
