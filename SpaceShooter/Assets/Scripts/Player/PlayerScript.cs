using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    private Coroutine playerMoveCor;

    private void Awake()
    {
        Instance = this;
        InitBorders();
    }

    public Vector2 minBounds, maxBounds, center;
    private Camera mainCamera;

    private void InitBorders()
    {
        mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        
        //Center
        center = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.25f));
    }

    public void SwitchPlayerMovement(bool on)
    {
        if (!on)
        {
            StartPlayerMovement();
        }
        else
        {
            //I use it only when the game is ended
            StopPlayerMovement();
            StartCoroutine(MovePlayerToCenter());
            GetComponent<PlayerShoot>().gunState = PlayerShoot.GunState.StopFiring;
        }
    }

    public void StartPlayerMovement()
    {
        playerMoveCor = StartCoroutine(UpdatePlayerPos());
    }
    
    public void StopPlayerMovement()
    {
        StopCoroutine(playerMoveCor);
    }

    private IEnumerator UpdatePlayerPos()
    {
        while (true)
        {
            if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject())
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

            yield return null;
        }
    }

    private IEnumerator MovePlayerToCenter()
    {
        while ((Vector2)transform.position != center)
        {
            transform.position = Vector2.MoveTowards(transform.position, center, 2 * Time.deltaTime);
            yield return null;
        }
    }
}
