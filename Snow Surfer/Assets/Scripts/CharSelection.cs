using UnityEngine;

public class CharSelection : MonoBehaviour
{
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject dinoSprite;
    [SerializeField] GameObject frogSprite;
    void Start()
    {
        Time.timeScale = 0;
        dinoSprite.SetActive(false);
        frogSprite.SetActive(false);
    }

    public void BeginGame()
    {
        Time.timeScale = 1;

        scoreCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChooseDino()
    {
        dinoSprite.SetActive(true);
        BeginGame();
    }

    public void ChooseFrog()
    {
        frogSprite.SetActive(true);
        BeginGame();
    }
}
