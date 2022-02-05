using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agentPlayer;
    [HideInInspector] public Vector3 firstPlayerPos;
    [HideInInspector] public bool isFinishedPlayer;
    public static GameManager Instance;
    [HideInInspector] public bool playerReset;
    [HideInInspector] public bool isPausedGame;
    [SerializeField] private GameObject confettiVFX;
    [SerializeField] private GameObject DiePlayerVFX;

    public static UnityEvent OnDetectFinish = new UnityEvent();

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        player = Instantiate(player, player.transform.position, Quaternion.identity);
        firstPlayerPos = player.transform.position;
        agentPlayer = player.GetComponent<NavMeshAgent>();
        isFinishedPlayer = false;
    }
    public void ResetPlayer()
    {
        StartCoroutine(ResetorPlayer());

    }

    private IEnumerator ResetorPlayer()
    {
        playerReset = true;
        DiePlayerVFX = Instantiate(DiePlayerVFX, player.transform.position, Quaternion.identity);
        player.gameObject.SetActive(false);
        isPausedGame = true;
        yield return new WaitForSeconds(1f);
        player.gameObject.SetActive(true);
        player.transform.position = firstPlayerPos;
        isPausedGame = false;
        playerReset = false;
        yield break;
    }

    public void isStopGame(bool pauze)
    {
        isPausedGame = pauze;
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, ZonesController.finishPos) < 0.5f)
        {
            isFinishedPlayer = true;
            OnDetectFinish.Invoke();
            StartCoroutine(RestarterGame());
            confettiVFX = Instantiate(confettiVFX, player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        if (isPausedGame)
            agentPlayer.enabled = false;
        else agentPlayer.enabled = true;
    }

    private IEnumerator RestarterGame()
    {
        yield return new WaitForSeconds(UIManager.Instance.timeAnimUIFinish);
        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
