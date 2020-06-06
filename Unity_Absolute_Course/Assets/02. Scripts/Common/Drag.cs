using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Transform itemTr;
    private Transform inventroyTr;

    private Transform itemListTr;
    private CanvasGroup canvasGroup;

    public static GameObject dragginItem = null;

    // Start is called before the first frame update
    void Start()
    {
        itemTr = GetComponent<Transform>();
        inventroyTr = GameObject.Find("Inventory").GetComponent<Transform>();
        itemListTr = GameObject.Find("ItemList").GetComponent<Transform>();

        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        itemTr.position = Input.mousePosition;
        
    }
    // Update is called once per frame
 

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventroyTr);
        dragginItem = this.gameObject;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragginItem = null;
        canvasGroup.blocksRaycasts = true;
        if(itemTr.parent==inventroyTr)
        {
            itemTr.SetParent(itemListTr.transform);
            GameManager.instance.RemoveItem(GetComponent<ItemInfo>().itemData);
        }
    }
}
