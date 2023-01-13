using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_CyclinderGenerator : MonoBehaviour
{

    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int radius = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private int snapDistance = 1;
    [SerializeField] private int distance = 1;
    [SerializeField] private int angle = 5;

    private List<Vector2> positions = new List<Vector2>();

    public void GenerateCyclinder()
    {
        positions.Clear();
        GameObject parent = new GameObject();
        for (int i = 0; i < 360; i += angle)
        {
            float newX = radius * Mathf.Cos(i * Mathf.PI / 180);
            float newY = radius * Mathf.Sin(i * Mathf.PI / 180);
            if (!positions.Contains(new Vector2(Mathf.CeilToInt(newX), Mathf.CeilToInt(newY))))
            {
                positions.Add(new Vector2(Mathf.CeilToInt(newX), Mathf.CeilToInt(newY)));
            }

        }

        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject newObj = Instantiate(objectToSpawn, parent.transform);
                newObj.transform.position = new Vector3(positions[i].x * distance, j * distance, positions[i].y * distance);
            }
        }

    }


}
