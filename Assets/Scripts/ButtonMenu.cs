using UnityEngine;
using UnityEngine.EventSystems;

public class AutoSelectButton : MonoBehaviour
{
    public GameObject firstButton;

    private void OnEnable()
    {
        if (firstButton != null && firstButton.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(null); // Reset selection first
            EventSystem.current.SetSelectedGameObject(firstButton); // Select the correct button
        }
    }
}
