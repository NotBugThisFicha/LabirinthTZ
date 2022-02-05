using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup GameOverCanvasGr;
    [SerializeField] private GameObject panelResumeGame;
    [SerializeField] private GameObject panelStopGame;

    public float timeAnimUIFinish = 3f;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnDetectFinish.AddListener(BlackScreenAnim);
    }

    public void PauseGame()
    {
        StartCoroutine(FadeTo(GameOverCanvasGr, 1f, 1f));
        panelResumeGame.gameObject.SetActive(true);
        panelStopGame.gameObject.SetActive(true);
        GameManager.Instance.isStopGame(true);
    }

    public void ResumeGame()
    {
        StartCoroutine(FadeTo(GameOverCanvasGr, 0f, 1f));
        panelResumeGame.gameObject.SetActive(false);
        panelStopGame.gameObject.SetActive(false);
        GameManager.Instance.isStopGame(false);
    }

    private void BlackScreenAnim()
    {
        StartCoroutine(FadeTo(GameOverCanvasGr, 1f, timeAnimUIFinish));
    }
    IEnumerator FadeTo(CanvasGroup canvasGroup, float aValue, float aTime)
    {
        float alpha = canvasGroup.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            canvasGroup.alpha = Mathf.Lerp(alpha, aValue, t);
            yield return null;
        }
        yield break;
    }
}
