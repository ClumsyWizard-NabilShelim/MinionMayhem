using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 horizontalLimit;
    [SerializeField] private Vector2 verticalLimit;

    private void LateUpdate()
    {
        Vector2 moveTo = (Vector2)transform.position + InputManager.Instance.InputAxis * moveSpeed * Time.deltaTime;
        moveTo = new Vector2(Mathf.Clamp(moveTo.x, horizontalLimit.x, horizontalLimit.y), Mathf.Clamp(moveTo.y, verticalLimit.x, verticalLimit.y));
        transform.position = new Vector3(moveTo.x, moveTo.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(horizontalLimit.y * 2, verticalLimit.y * 2));
    }

    protected override void CleanUpStaticData()
    {
    }
}
