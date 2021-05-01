using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler
{
    Button bttn;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        bttn = GetComponent<Button>();

        bttn.Select();
    }
}
