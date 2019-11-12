using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    private readonly string pictureRoot = "Assets/Resources/Snapshots/";

    [SerializeField]
    private GameObject escapeMenu;

    private void Update() {
        // Check if menu needs to be opened
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (escapeMenu.activeInHierarchy) {
                ReturnToGame();
                escapeMenu.SetActive(false);
            } else {
                Time.timeScale = 0;
                escapeMenu.SetActive(true);
            }
        }
    }

    // Delete screenshots after application quit
    private void OnApplicationQuit() {
        DirectoryInfo di = new DirectoryInfo(pictureRoot);

        if (di == null)
            return;
        foreach (FileInfo file in di.GetFiles())
            file.Delete();
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }

    public void ReturnToGame() {
        Time.timeScale = 1;
        escapeMenu.SetActive(false);
    }

    public void End() {
        ArrestHandler.EndGame();
    }

    public void ArrestedSuspect() {
        ArrestHandler.ActivateGameOverScreen();
    }
}
