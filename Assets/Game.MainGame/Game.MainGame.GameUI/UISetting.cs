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
            if(PlayerPrefs.GetInt("sound") == 0)
            {
                GameManager.Instance.UpdateSound(true);
            }
            else
            {
                GameManager.Instance.UpdateSound(false);
            }
            CheckSound();
        }

        public void BtnMusic()
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                GameManager.Instance.UpdateMusic(true);
            }
            else
            {
                GameManager.Instance.UpdateMusic(false);
            }
            CheckMusic();
        }

        public void BtnVibration()
        {
            if (PlayerPrefs.GetInt("vibra") == 0)
            {
                GameManager.Instance.UpdateVibration(true);
            }
            else
            {
                GameManager.Instance.UpdateVibration(false);
            }
            CheckVibra();
        }

        public void BtnNotification()
        {
            if (PlayerPrefs.GetInt("noti") == 0)
            {
                GameManager.Instance.UpdateNoti(true);
            }
            else
            {
                GameManager.Instance.UpdateNoti(false);
            }
            CheckNoti();
        }

        public void BtnRestore()
        {

        }

        public void BtnLanguage()
        {

        }

        public void BtnContinous()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIManager.Instance.QueuePop();
        }

        public void BtnQuit()
        {

            UIManager.Instance.QueuePush(GameManager.ScreenId_UIFadeScreen, null, "UIFadeScene", null);
            UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);

            if (ui != null && !ui.gameObject.active) ui.gameObject.SetActive(true);

            ui.SetAction(() => {
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIHome, null, "UIHome", null);
                UIGamePlay uigame = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                uigame.CloseUI();
                LevelManager.Instance.ClearCell();
                Close();
            });
        }

        public void BtnClose()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            Close();
        }

        public void Close()
        {
            UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_Setting);
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

        public void CheckSound()
        {
            if(PlayerPrefs.GetInt("sound") == 1)
            {
                _btnSound.GetComponent<Image>().sprite = _sprOn;
                _txtSound.text = "ON";
            }
            else
            {
                _btnSound.GetComponent<Image>().sprite = _sprOff;
                _txtSound.text = "OFF";
            }
        }

        public void CheckMusic()
        {
            if (PlayerPrefs.GetInt("music") == 1)
            {
                _btnMusic.GetComponent<Image>().sprite = _sprOn;
                _txtMusic.text = "ON";
            }
            else
            {
                _btnMusic.GetComponent<Image>().sprite = _sprOff;
                _txtMusic.text = "OFF";
            }
        }

        public void CheckVibra()
        {
            if (PlayerPrefs.GetInt("vibra") == 1)
            {
                _btnVibration.GetComponent<Image>().sprite = _sprOn;
                _txtVibration.text = "ON";
            }
            else
            {
                _btnVibration.GetComponent<Image>().sprite = _sprOff;
                _txtVibration.text = "OFF";
            }
        }

        public void CheckNoti()
        {
            if (PlayerPrefs.GetInt("noti") == 1)
            {
                _btnNotification.GetComponent<Image>().sprite = _sprOn;
                _txtNotification.text = "ON";
            }
            else
            {
                _btnNotification.GetComponent<Image>().sprite = _sprOff;
                _txtNotification.text = "OFF";
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

            CheckMusic();
            CheckNoti();
            CheckSound();
            CheckVibra();
        }
    }
}
