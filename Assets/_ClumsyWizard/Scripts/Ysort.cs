using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Ysort : MonoBehaviour
{
	[SerializeField] private bool runOnce;

	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private SpriteRenderer spriteRendererBelow;

	private void Awake()
	{
		if(runOnce)
			Sort();
	}

	void LateUpdate()
    {
		if (!runOnce)
			Sort();
	}

	private void Sort()
	{
        float basePos = spriteRenderer.bounds.min.y;
		spriteRenderer.sortingOrder = (int)(basePos * -100);
		if (spriteRendererBelow != null)
			spriteRendererBelow.sortingOrder = spriteRenderer.sortingOrder - 1;
    }
}
