using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public ObjectAction[] action;
    public List<Sprite> orderCut;
    BoxCollider2D box;
    RectTransform sizeObj;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        sizeObj = GetComponent<RectTransform>();

        box.size = new Vector2(sizeObj.rect.width, sizeObj.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
