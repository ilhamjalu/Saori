using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetTitleScript : MonoBehaviour
{
    public string[] titles;
    public TextMeshProUGUI titleUI;
    public TextMeshProUGUI registeredSymbol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        titleUI.text = titles[PlayerPrefs.GetInt("noResep")];
        registeredSymbol.rectTransform.sizeDelta = new Vector2(titleUI.rectTransform.sizeDelta.x + 100f, titleUI.rectTransform.sizeDelta.y);
    }
}
