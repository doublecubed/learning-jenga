// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Camera controller script. Listens to mouse input as well as 6-7-8 on the number line
// Performs camera movements and also makes switches between tower focus points.

using System.Collections;
using UnityEngine;

namespace JengaGame.Gameplay
{
	public class CameraController : MonoBehaviour
	{
		#region REFERENCES
		
		public Transform cameraTransform;
		public Transform[] focusPointCenters;

		#endregion
		
		#region VARIABLES
		
		public float VerticalMoveSpeed;
		public float HorizontalRotateSpeed;
		public float ZoomSpeed;
		public float FocusSwitchDuration;
		
		private bool _mousePressed;
		private bool _cameraMoving;

		#endregion
		
		#region MONOBEHAVIOUR
		
		private void Update()
		{
			ResolveMousePresses();
			ResolveCameraMovement();
			ResolveGradeButtonPresses();
		}

		#endregion
		
		#region METHODS

		private void ResolveMousePresses()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_mousePressed = true;
				Cursor.visible = false;
			}

			if (Input.GetMouseButtonUp(0))
			{
				_mousePressed = false;
				Cursor.visible = true;
			}
		}

		private void ResolveCameraMovement()
		{
			if (_mousePressed && !_cameraMoving)
			{
				float mouseXDelta = Input.GetAxis("Mouse X");
				float mouseYDelta = Input.GetAxis("Mouse Y");

				
				transform.Rotate(0f, mouseXDelta * HorizontalRotateSpeed, 0f);
				cameraTransform.position += Vector3.up * mouseYDelta * VerticalMoveSpeed * Time.deltaTime;
			}
			
			
			float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
			cameraTransform.position += cameraTransform.forward * mouseScroll * ZoomSpeed;
		}

		private void ResolveGradeButtonPresses()
		{
			if (Input.GetKeyDown(KeyCode.Alpha6) && !_cameraMoving)
			{
				MoveToSixthGrade();
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha7) && !_cameraMoving)
			{
				MoveToSeventhGrade();
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha8) && !_cameraMoving)
			{
				MoveToEighthGrade();
			}
		}
		
		
		public void MoveToSixthGrade()
		{
			StartCoroutine(MoveToFocus(focusPointCenters[0]));
		}

		public void MoveToSeventhGrade()
		{
			StartCoroutine(MoveToFocus(focusPointCenters[1]));
		}

		public void MoveToEighthGrade()
		{
			StartCoroutine(MoveToFocus(focusPointCenters[2]));
		}

		#endregion
		
		#region COROUTINES
		
		private IEnumerator MoveToFocus(Transform focusPoint)
		{
			_cameraMoving = true;
			
			Vector3 startPos = transform.position;
			Vector3 endPos = focusPoint.position;
			
			float timer = 0f;
			while (timer < FocusSwitchDuration)
			{
				timer += Time.deltaTime;
				float lerp = Mathf.Clamp(timer / FocusSwitchDuration, 0f, 1f);

				transform.position = Vector3.Lerp(startPos, endPos, lerp);
				yield return null;
			}

			_cameraMoving = false;
		}
		
		#endregion
	}

}