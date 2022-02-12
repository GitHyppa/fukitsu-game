using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;

    public Text Text_AmmoCount;
    public Text Text_Reloading;

    void Update()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        Text_AmmoCount.transform.position = new Vector3(40, 20, 0);
        Text_Reloading.transform.position = new Vector3(Text_AmmoCount.transform.position.x + 90, 20, 0);
    }
}
