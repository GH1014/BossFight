using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicPigGames
{
    public class HUD : MonoBehaviour
    {
        // Start is called before the first frame update

        public enum InfoType {BossHealth, Health};
        public InfoType type;

        Text myText;
        private ProgressBar myProgressBar;

        void Awake()
        {
            myText = GetComponent<Text>();
            myProgressBar = GetComponent<ProgressBar>();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            switch (type) {

                case InfoType.Health:
                    float curHealth = GameManager.instance.health;
                    float maxHealth = GameManager.instance.maxHealth;
                    myProgressBar.SetProgress(curHealth / maxHealth);
                    break;

                case InfoType.BossHealth:
                    float curBosshealth = GameManager.instance.bossHealth;
                    float maxBosshealth = GameManager.instance.bossMaxHealth;
                    myProgressBar.SetProgress(curBosshealth / maxBosshealth);    
                    break;
            }
        }
    }
}
