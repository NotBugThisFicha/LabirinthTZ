using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Material playerShieldMaterial;
    [SerializeField] private Material playerStockMaterial;
    private MeshRenderer playerRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerRenderer = gameObject.GetComponent<MeshRenderer>();
        playerRenderer.material = playerStockMaterial;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.playerReset && !GameManager.Instance.isPausedGame)
            agent.SetDestination(ZonesController.finishPos);
       if(Shield.Instance.shieldIsActiv)
            playerRenderer.material = playerShieldMaterial;
       else playerRenderer.material = playerStockMaterial;
    }
}
