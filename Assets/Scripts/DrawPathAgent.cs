using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawPathAgent : MonoBehaviour
{
	private NavMeshAgent agentPlayer; 

	public GameObject point; 
	public GameObject line; 

	[SerializeField] private float distance = 1; 
	[SerializeField] private float height = 1f;

	private List<GameObject> points;
	private Vector3 agentPoint;
	private Vector3 lastPoint;
	private List<GameObject> lines;

	void Awake()
	{
	
	}
    private void Start()
    {
		agentPlayer = FindObjectOfType<NavMeshAgent>();
		agentPoint = agentPlayer.transform.position;
		points = new List<GameObject>();
		lines = new List<GameObject>();
		UpdatePath();

	}
	void ClearArray() 
	{
		foreach (GameObject obj in points)
		{
			Destroy(obj);
		}
		foreach (GameObject obj in lines)
		{
			Destroy(obj);
		}
		lines = new List<GameObject>();
		points = new List<GameObject>();
	}

	bool IsDistance(Vector3 distancePoint) 
	{
		bool result = false;
		float dis = Vector3.Distance(lastPoint, distancePoint);
		if (dis > distance) result = true;
		lastPoint = distancePoint;
		return result;
	}

	void UpdatePath() // ������ ����
	{
		lastPoint = agentPlayer.transform.position + Vector3.forward * 100f; // ����� ������� ����� � ������� �������


		ClearArray();

		for (int j = 0; j < agentPlayer.path.corners.Length; j++)
		{
			if (IsDistance(agentPlayer.path.corners[j]))
			{
				GameObject p = Instantiate(point) as GameObject;
				p.transform.position = agentPlayer.path.corners[j] + Vector3.up * height; // ������� ����� � ������������ ������� 
				points.Add(p);
			}
		}

		for (int j = 0; j < points.Count; j++)
		{
			if (j + 1 < points.Count)
			{
				Vector3 center = (points[j].transform.position + points[j + 1].transform.position) / 2; // ������� ����� ����� �������
				Vector3 vec = points[j].transform.position - points[j + 1].transform.position; // ������� ������ �� ����� �, � ����� �
				float dis = Vector3.Distance(points[j].transform.position, points[j + 1].transform.position)/10f; // ������� ��������� ����� � � �

				GameObject p = Instantiate(line) as GameObject;
				p.transform.position = center;
				p.transform.rotation = Quaternion.FromToRotation(Vector3.right, vec.normalized); // �������� �� �������
				p.transform.localScale = new Vector3(dis, p.transform.localScale.y, p.transform.localScale.z); // ����������� �� �
				lines.Add(p);
			}
		}

	}

	void Update()
	{
		 UpdatePath(); 
	
	}
}
