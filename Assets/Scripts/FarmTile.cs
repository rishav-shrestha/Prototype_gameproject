using UnityEngine;

public class FarmTile : MonoBehaviour
{
    public enum TileState { Empty, Growing, Ready }
    public TileState state = TileState.Empty;

    private SpriteRenderer sr;
    private float growthTimer = 0f;
    public float growthTime = 5f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void Update()
    {
        if (state == TileState.Growing)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= growthTime)
            {
                state = TileState.Ready;
                UpdateColor();
            }
        }
    }

    public void OnTap()
    {
        if (state == TileState.Empty)
        {
            state = TileState.Growing;
            growthTimer = 0f;
            UpdateColor();
        }
        else if (state == TileState.Ready)
        {
            state = TileState.Empty;
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        switch (state)
        {
            case TileState.Empty: sr.color = Color.brown; break;
            case TileState.Growing: sr.color = Color.green; break;
            case TileState.Ready: sr.color = Color.yellow; break;
        }
    }
}
