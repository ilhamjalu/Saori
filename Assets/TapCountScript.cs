using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapCountScript : MonoBehaviour
{
    public MasakScript ms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (ms.aduk)
        {
            ms.jumlahTap += 1;

            if (ms.jumlahTap >= 3)
            {
                ms.stop.interactable = true;

                ms.bahanAkhir.gameObject.SetActive(true);
                ms.bahanAkhir.GetComponent<SpriteRenderer>().sprite = ms.bahanAkhirList[PlayerPrefs.GetInt("noResep")];
                GameObject.Find("ParentBahan").gameObject.SetActive(false);
            }
        }
    }
}
