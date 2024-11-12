using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader: MonoBehaviour
{
    [SerializeField, Scene] public string sceneToLoad;

    Button buttonComponent; 


    private void Start()
    {
        buttonComponent = GetComponent<Button>();

        // This method of adding event listeners can be less appearent to a designer, but improves encapsulation.
        buttonComponent.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
