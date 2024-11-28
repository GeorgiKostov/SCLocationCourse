using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class SceneMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject menuPanel; // The sliding menu panel
    public Button menuToggleButton; // The button to open/close the menu
    public Transform buttonContainer; // The parent object of the scroll view where buttons will be added
    public GameObject buttonPrefab; // The prefab for the buttons

    private bool isMenuVisible = false;

    void Awake()
    {
        // Ensure the menu persists between scenes
        DontDestroyOnLoad(gameObject);

        // Toggle menu visibility
        menuToggleButton.onClick.AddListener(ToggleMenu);

        // Generate scene buttons
        GenerateSceneButtons();

        // Automatically close menu when a new scene is loaded
        SceneManager.sceneLoaded += (scene, mode) => CloseMenu();
    }

    void GenerateSceneButtons()
    {
        // Clear existing buttons (if any)
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Get all scenes from the build settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            // Get the scene path and extract the name
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            // Create a button for the scene
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;

            // Add listener to load the corresponding scene
            int sceneIndex = i; // Local copy for closure
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadScene(sceneIndex));
        }
    }

    public void LoadScene(int sceneIndex)
    {
        // Load the selected scene
        SceneManager.LoadScene(sceneIndex);

        // Automatically close the menu
        CloseMenu();
    }

    public void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuPanel.SetActive(isMenuVisible);
    }

    public void CloseMenu()
    {
        isMenuVisible = false;
        menuPanel.SetActive(false);
    }
}
