using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesController : MonoBehaviour
{
    private GeneratorWalls[,] walls;
    [SerializeField] private GameObject finishZone;
    [SerializeField] private GameObject dangerZone;

    public static Vector3 finishPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject finishZone = Instantiate(this.finishZone, new Vector3(0, 0, 0), Quaternion.identity);
        walls = GetComponent<LabirinthSpawner>().labirinthClone;
        for (int x = 0; x < walls.GetLength(0); x++)
        {
            for (int y = 0; y < walls.GetLength(1); y++)
            {
                if (walls[x, y].upZone) finishZone.transform.position = FinishPosition(finishZone, x, y, 0, 1);
                if (walls[x, y].downZone) finishZone.transform.position = FinishPosition(finishZone, x, y, 0, -1);
                if (walls[x, y].rightZone) finishZone.transform.position = FinishPosition(finishZone, x, y, 1, 0);
                if (walls[x, y].leftZone) finishZone.transform.position = FinishPosition(finishZone, x, y, -1, 0);
            }
        }
        StartCoroutine(DangerZoneCoroutine(dangerZone));
    }

    private Vector3 FinishPosition(GameObject finish, int X, int Z, float offsetX, float offsetZ)
    {
        finish.transform.position = new Vector3(X + offsetX, 0.03f, Z + offsetZ);
        finishPos = finish.transform.position;
        return finishPos;
    }
    
    private IEnumerator DangerZoneCoroutine(GameObject dangerZone)
    {
     
        while(!GameManager.Instance.isFinishedPlayer)
        {
            if(!GameManager.Instance.isPausedGame)
                dangerZone = Instantiate(dangerZone, new Vector3(Random.Range(0, walls.GetLength(0) - 1), 0.5f, Random.Range(0, walls.GetLength(1) - 1) + 0.4f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
        yield break;
    }
}
