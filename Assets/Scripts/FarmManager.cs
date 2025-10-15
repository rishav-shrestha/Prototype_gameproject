using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject tilePrefab;

    void Start()
    {
        GenerateFarm();
    }

    public void GenerateFarm()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
