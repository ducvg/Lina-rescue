using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject hover; 

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.Play("buttonHover");
        normal.SetActive(false);
        hover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        normal.SetActive(true);
        hover.SetActive(false);
    }
}
