using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject[] bahan;
    public Transform[] posisiBahan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputBahan(int id)
    {
        Instantiate(bahan[id], posisiBahan[id].transform.position, posisiBahan[id].transform.rotation);
    }

    public void LoopSpawnPosition()
    {
        for(int i = 0; i <= posisiBahan.Length; i++)
        {
            //posisiBahan[i]
        }
    }
}
