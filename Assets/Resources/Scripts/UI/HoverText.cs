using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject background;

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        background.SetActive(false);
    }
}