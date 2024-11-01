using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] private AnimationCurve zoomCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float zoomSpeed = 2f;  // Speed of zoom transition
    [SerializeField]
    private float playZoom = 3f;
    [SerializeField]
    private float pauseZoom = 5f;

    Vector3 _distance;
    bool isRunning = false;
    private Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            Debug.LogError("Camera component not found!");
    }
    private void OnEnable()
    {
        GameManager.OnGameEvent += OnGameEvent;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        GameManager.OnGameEvent -= OnGameEvent;
    }

    private void OnGameEvent(eGameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case eGameEvent.GAME_START:
                StartZoom(playZoom);
                isRunning = true;
                break;
            case eGameEvent.GAME_OVER:
                isRunning = false;
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _distance = player.position - transform.position;
        cam.orthographicSize = pauseZoom;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
            transform.position = new Vector3(player.position.x - _distance.x, transform.position.y, player.position.z - _distance.z);
    }
    public void StartZoom(float targetZoom)
    {
        StopAllCoroutines(); // Stop any existing zoom coroutines
        StartCoroutine(LerpZoom(targetZoom));
    }

    private IEnumerator LerpZoom(float targetZoom)
    {
        float startZoom = cam.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < zoomSpeed)
        {
            // Evaluate the curve based on the normalized time (elapsedTime / zoomSpeed)
            float curveValue = zoomCurve.Evaluate(elapsedTime / zoomSpeed);

            // Lerp using curve-adjusted value
            cam.orthographicSize = Mathf.Lerp(startZoom, targetZoom, curveValue);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure we reach the exact target size at the end
        cam.orthographicSize = targetZoom;
    }
}
