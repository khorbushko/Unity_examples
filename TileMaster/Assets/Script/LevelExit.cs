using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] int currentLevel = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(ExitAction());
        }
    }

    IEnumerator ExitAction()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        var nextIndex = currentLevel + 1;
        if (nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 1;
        }
        SceneManager.LoadScene($"Level 0{nextIndex}");
    }
}
