using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Unity Settings")]
    MenuController mc;
    public Text goldCounter;
    public Text healthCounter;

    [Header("Game Settings")]
    public int maxHealth = 100;
    private int curHealth;
    public int startGold = 100;
    private int curGold = 0;

    // Start is called before the first frame update
    void Start()
    {
        mc = GameObject.Find("MenuController").GetComponent<MenuController>();
        goldCounter = GameObject.Find("GameUI/GoldCounter").GetComponent<Text>();
        healthCounter = GameObject.Find("GameUI/HealthCounter").GetComponent<Text>();
        SetCurGold(startGold);
        SetCurHealth(maxHealth);
        if(mc != null)
        {
            InvokeRepeating("checkDefeated", 0f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void checkDefeated()
    {
        if (curHealth <= 0)
        {
            mc.ShowGameLost();
            mc.FreezeGame();
        }
    }

    public void SetCurHealth(int health)
    {
        this.curHealth = health;
        if (healthCounter != null)
        {
            healthCounter.text = health.ToString();
        }
    }
    public int GetCurHealth()
    {
        return this.curHealth;
    }
    public void SetCurGold(int gold)
    {
        this.curGold = gold;
        if(goldCounter != null)
        {
            goldCounter.text = gold.ToString() + "$";
        }
    }
    public int GetCurGold()
    {
        return this.curGold;
    }
}
