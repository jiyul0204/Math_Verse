using UnityEngine;

using DG.Tweening;

public class RocketMovement : MonoBehaviour
{
	private Transform target;
	public PathType pathType = PathType.CatmullRom;
	public Vector3[] waypoints;

	private void Awake()
    {
		target = GetComponent<Transform>();
    }

    private void Start()
	{
		// Create a path tween using the given pathType, Linear or CatmullRom (curved).
		// Use SetOptions to close the path
		// and SetLookAt to make the target orient to the path itself
		Tween t = target.DOPath(waypoints, 4, pathType, PathMode.Sidescroller2D)
			.SetOptions(false)
			.SetLookAt(0.001f);

		// Then set the ease to Linear and use infinite loops
		t.SetEase(Ease.Linear).SetLoops(-1);
	}
}