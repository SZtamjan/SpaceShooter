using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private void Awake()
    {
        Instance = this;
        InitBorders();
    }

    public Vector2 minBounds, maxBounds;
    private Camera mainCamera;

    private void InitBorders()
    {
        mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 0.5f));
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 taczPos = mainCamera.ScreenToWorldPoint(Input.touches[i].position);

                taczPos.z = 0f;

                taczPos.x = Mathf.Clamp(taczPos.x, minBounds.x, maxBounds.x);
                taczPos.y = Mathf.Clamp(taczPos.y, minBounds.y, maxBounds.y);
                
                transform.position = taczPos;
            }
            
        }
    }

    public void Thor(bool isOn)
    {
        if (isOn)
        {
            
            
            
        }
        else
        {
            
        }
    }
}
