using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrestHandler : MonoBehaviour { 

    private static GameObject gameOverText;
    private static GameObject gameOverScreen;

    void Awake() {
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverText = GameObject.Find("GameOverText");
        gameOverScreen.SetActive(false);
        gameOverText.SetActive(false);
    }

    //When player enters within the radius of the suspect stop time and display game over
    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Officer")
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
            gameOverScreen.SetActive(true);
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public static void ActivateGameOverScreen() {
        Time.timeScale = 0;

        gameOverText.SetActive(true);
        gameOverScreen.SetActive(true);

    }

    public static void EndGame() {
        if (GameObject.Find("Controller (right)").GetComponent<UILaser>().enabled == true) {
            GameObject.Find("Controller (right)").GetComponent<UILaser>().enabled = false;
            GameObject.Find("Controller (right)").GetComponent<SteamVR_LaserPointer>().enabled = false;
        }
        SceneManager.LoadScene(0);
    }
}
