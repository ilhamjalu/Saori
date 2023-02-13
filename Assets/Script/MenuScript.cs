using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetTouchInput())
        {
            SceneManager.LoadScene("PilihResep");
        }
    }

    bool GetTouchInput()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);

            //if (touch.phase == TouchPhase.Began)
            //{
            //    firstPos = new Vector3(touch.position.x, touch.position.y);
            //}

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("TAP DONE");
                return true;
            }
        }
        return false;
    }
}
