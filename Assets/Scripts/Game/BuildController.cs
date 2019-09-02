using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildController : MonoBehaviour
{
    public static BuildController instance;

    [Header("Unity Settings")]
    public Camera cam;
    public GameObject[] towers;
    private GameObject selectedTower;
    public Button sellButton;
    public Button buildButton;
    public GameController gc;
    private TowerController tc;

    private bool sellMode = false;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    private void Start()
    {
        selectedTower = towers[0];
        tc = selectedTower.GetComponent<TowerController>();
        DisableSellMode();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Node" && sellMode == false)
                {
                    if (CanAfford(tc.cost) == true)
                    {
                        BuildTower(hit);
                    }
                }
                if (hit.collider.gameObject.tag == "Tower" && sellMode == true)
                {
                    print("Tower Hit");
                    SellTower(hit);
                }
                
                if (hit.collider.gameObject.tag == "Tower" && sellMode == false)
                {
                    TowerController towerController = hit.collider.GetComponent<TowerController>();
                    GameObject nextUpgrade = hit.collider.GetComponent<TowerController>().upgrade;
                    if (CanAfford(tc.upgradeCost) == true && nextUpgrade != null)
                    {
                        UpgradeTower(hit, towerController);
                    }
                }
            }
        }
    }

    public void SellTower(RaycastHit hit)
    {
        print("Selling Tower...");
        gc.SetCurGold(gc.GetCurGold() + (tc.cost - 10));
        Destroy(hit.collider.gameObject);
    }
    
    public void UpgradeTower(RaycastHit hit, TowerController tc)
    {
        if(tc != null)
        {
            gc.SetCurGold(gc.GetCurGold() - tc.upgradeCost);
            Instantiate(tc.upgrade, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
            Destroy(hit.collider.gameObject);
        }
        
    }
    
    private void BuildTower(RaycastHit hit)
    {
        if(selectedTower != null && tc != null)
        {
            gc.SetCurGold(gc.GetCurGold() - tc.cost);
            Instantiate(selectedTower, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
        }
        else
        {
            print("Unable to find Tower Controller or selectedTower");
        }
    }
    public bool CanAfford(int cost)
    {
        if (gc.GetCurGold() - cost >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnableSellMode()
    {
        sellMode = true;
        sellButton.gameObject.SetActive(false);
        buildButton.gameObject.SetActive(true);
    }
    public void DisableSellMode()
    {
        sellMode = false;
        sellButton.gameObject.SetActive(true);
        buildButton.gameObject.SetActive(false);
    }
}
