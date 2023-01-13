using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_AreaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField]private int row = 5;
    [SerializeField]private int col = 5;
    [SerializeField] private int distance = 1;

    public void GenerateArea()
    {
        if (objectToSpawn != null)
        {
            GameObject parent = new GameObject();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GameObject newObj = Instantiate(objectToSpawn, parent.transform);
                    newObj.transform.position = new Vector3(i * distance, 0, j * distance);
                }
            }
        }
    }
}

