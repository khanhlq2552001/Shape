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
    }
}
