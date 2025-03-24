using UnityEngine;

namespace Game.MainGame
{
    public class PrefData : MonoBehaviour
    {
        public int GetTym()
        {
            return PlayerPrefs.GetInt("tym");
        }

        public int GetCoin()
        {
            return PlayerPrefs.GetInt("coin");
        }

        public int GetLevel()
        {
            return PlayerPrefs.GetInt("level");
        }

        public int GetCountBooster(int id)
        {
            switch (id)
            {
                case 0:
                    return PlayerPrefs.GetInt("boosterTime");
                case 1:
                    return PlayerPrefs.GetInt("boosterHammer");
                case 2:
                    return PlayerPrefs.GetInt("boosterMagic");
            }

            return 0;
        }

        public void SetIDnewBlock(int id)
        {
            PlayerPrefs.SetInt("idNewBlock", id);
        }

        public int GetIdNewBlock()
        {
            return PlayerPrefs.GetInt("idNewBlock");
        }

        public void SetInfiniteTime(bool isStatus)
        {
            if (isStatus)
            {
                PlayerPrefs.SetInt("InfiniteTime", 1);
            }
            else
            {
                PlayerPrefs.SetInt("InfiniteTime", 0);
            }
        }

        public bool GetInfiniteTime()
        {
            if(PlayerPrefs.GetInt("InfiniteTime") == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetTimeRemainingInfinite(float time)
        {
            PlayerPrefs.SetFloat("TimeRemainingInfinite", time);
        }

        public float GetTimeRemainingInfinite()
        {
            return PlayerPrefs.GetFloat("TimeRemainingInfinite");
        }
    }
}
