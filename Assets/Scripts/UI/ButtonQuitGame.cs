using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonQuitGame: MonoBehaviour
{
    Button buttonComponent; 


    private void Start()
    {
        buttonComponent = GetComponent<Button>();

        // This method of adding event listeners can be less appearent to a designer, but improves encapsulation.
        buttonComponent.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();
    }
}
