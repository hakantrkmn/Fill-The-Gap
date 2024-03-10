using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildScreen : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level " + EventManager.GetLevelData().currentLevelIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
