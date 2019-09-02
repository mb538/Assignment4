using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{

    private Text waveCounter;

    void Start()
    {
        waveCounter = gameObject.GetComponent<Text>();
    }

    public void ShowWaveCounter(int waveNumber)
    {
        if(waveCounter != null)
        {
            waveCounter.text = "Wave " + waveNumber.ToString();
            waveCounter.CrossFadeAlpha(1, 2f, false);
        }
        else
        {
            print("Unable to find the Text Component");
        }
    }

    public void HideWaveCounter()
    {
        if (waveCounter != null)
        {
            waveCounter.CrossFadeAlpha(0, 2f, false);
        }
        else
        {
            print("Unable to find the Text Component");
        }
    }
}
