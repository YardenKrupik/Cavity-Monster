using UnityEngine;
using UnityEngine.EventSystems;

public class DebugEventSystem : MonoBehaviour
{
    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log("Selected: " + EventSystem.current.currentSelectedGameObject.name);
        }
    }
}
