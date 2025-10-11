using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InputManager : MonoBehaviour
{
	private const float MAX_RAYCAST_DISTANCE = 50;
	
	[SerializeField]
	private Camera mainCamera;
	[SerializeField]
	private LayerMask layer;

	private InputActions actions;

	public static event EventHandler<RoomCardPosition> OnSelectRoomPosition;

	private void Awake()
	{
        actions = new InputActions();

        EnableActions();
	}

    public void EnableActions()
    {
		actions.Enable();
	}

	public void DisableActions()
	{
		actions.Disable();
	}

	// Update is called once per frame
	void Update()
    {
        if(actions.Player.Attack.WasPressedThisFrame())
		{
			HandleClick();
		}
	}

	private void HandleClick()
	{
		var mousePosition = actions.UI.Point.ReadValue<Vector2>();
		Ray ray = mainCamera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
		if (Physics.Raycast(ray, out RaycastHit hit, MAX_RAYCAST_DISTANCE, layer))
		{
			var roomPosition = hit.collider.gameObject.GetComponentInParent<RoomCardPosition>();
			if (roomPosition != null)
			{
				OnSelectRoomPosition?.Invoke(this, roomPosition);
			}
		}
	}
}
