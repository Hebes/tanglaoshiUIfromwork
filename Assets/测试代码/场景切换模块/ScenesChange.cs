using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesChange : MonoBehaviour
{
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(()=> 
        {
            ScenesMgr.Instance.LoadSceneAsyn("Game", () =>
            {
                Debug.Log("«–ªª¡À≥°æ∞");
            });
        });
    }
}
