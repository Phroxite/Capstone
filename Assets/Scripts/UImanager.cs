using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{

    public GameObject SettingUI;
    public GameObject Chat;
    // Start is called before the first frame update
    void Start()
    {
        SettingUI.SetActive(false);
        Chat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ToggleSettingUI()
    {
        SettingUI.SetActive(!SettingUI.activeSelf);
    }
}
