using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LabirinthSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wallsPrefab;

    [HideInInspector] public GeneratorWalls[,] labirinthClone;

    public static UnityEvent NavMeshRebakerEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        LabirinthGenerator generator = new LabirinthGenerator();
        GeneratorWalls[,] labirinth = generator.GenerateLabirinth();
        labirinthClone = labirinth;
        for (int x = 0; x < labirinth.GetLength(0); x++)
        {
            for (int y = 0; y < labirinth.GetLength(1); y++)
            {
                Walls walls = Instantiate(wallsPrefab, new Vector3(x, 0, y), Quaternion.identity).GetComponent<Walls>();
                walls.wallLeft.SetActive(labirinth[x, y].WallLeft);
                walls.wallBottom.SetActive(labirinth[x, y].WallBottom);
             
            }
        }

        NavMeshRebakerEvent.Invoke();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
