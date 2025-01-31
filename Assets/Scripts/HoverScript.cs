using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    private GameObject monster;
    private Button btn;

    private void Start()
    {
        monster = GameObject.Find("Monster");
        btn = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        MoveMonster();
    }

    public void OnSelect(BaseEventData eventData)
    {
        MoveMonster();
    }

    private void MoveMonster()
    {
        if (monster != null)
        {
            monster.transform.position = new Vector2(monster.transform.position.x, transform.position.y);
        }
    }
}
