using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public Text healthText;
    public Text manaText;
    public Text actionsText;

    public Image healthBar;
    public Image manaBar;

    float maxWidth = 0;



    private void Start()
    {
        maxWidth = healthBar.GetComponent<RectTransform>().sizeDelta.x;
    }

    public void SetStats(Hero hero)
    {
        healthText.text = hero.health.ToString() + " / " + hero.maxHealth.ToString() ;
        manaText.text = hero.mana.ToString() + " / " + hero.maxMana.ToString();

        float healthWidth = maxWidth * (hero.health / hero.maxHealth);
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthWidth, healthBar.GetComponent<RectTransform>().sizeDelta.y);

        actionsText.text = hero.actions.ToString();

        if (hero.maxMana > 0)
        {
            manaBar.transform.parent.gameObject.SetActive(true);
            manaText.gameObject.SetActive(true);

            float manaWidth = maxWidth * (hero.mana / hero.maxMana);
            manaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(manaWidth, manaBar.GetComponent<RectTransform>().sizeDelta.y);
        }
        else
        {
            manaBar.transform.parent.gameObject.SetActive(false);
            manaText.gameObject.SetActive(false);
        }
    }
}
