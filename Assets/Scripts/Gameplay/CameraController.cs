// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Camera controller script. Listens to mouse input as well as 6-7-8 on the number line
// Performs camera movements and also makes switches between tower focus points.

using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace JengaGame.Gameplay
{
	public class CameraController : MonoBehaviour
	{
		#region REFERENCES
		
		public Transform cameraTransform;
		public Transform[] focusPointCenters;

		#endregion
		
		#region VARIABLES
		
		[Header("Camera Movement Parameters")]
		public float VerticalMoveSpeed;
		public float HorizontalRotateSpeed;
		public float ZoomSpeed;
		public float FocusSwitchDuration;

		[Header("Camera Movement Restrictions")]
		public float CameraMaxHeight;
		public float CameraMinHeight;
		[Space(5)] 
		public float CameraMaxDistance;
		public float CameraMinDistance;
		
		
		private bool _mousePressed;
		private bool _cameraMoving;
		private int _currentFocusCenter;
		
		#endregion
		
		#region MONOBEHAVIOUR

		private void Start()
		{
			// The camera looks at 7th tower at the beginning since it is the center.
			_currentFocusCenter = 1;
		}

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
			// Rotational Movement
			if (_mousePressed && !_cameraMoving)
			{
				float mouseXDelta = Input.GetAxis("Mouse X");
				float mouseYDelta = Input.GetAxis("Mouse Y");

				
				transform.Rotate(0f, mouseXDelta * HorizontalRotateSpeed, 0f);
				
				Vector3 cameraDesiredPosition = cameraTransform.position + Vector3.up * mouseYDelta * VerticalMoveSpeed * Time.deltaTime;
				float clampedYPosition = Mathf.Clamp(cameraDesiredPosition.y, CameraMinHeight, CameraMaxHeight);

				cameraTransform.position =
					new Vector3(cameraDesiredPosition.x, clampedYPosition, cameraDesiredPosition.z);
			}
			
			// Zoom Movement
			float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
			Vector3 cameraZoomPosition = cameraTransform.position + cameraTransform.forward * mouseScroll * ZoomSpeed;
			Vector3 axisCenter = DistanceMeasurementPoint(focusPointCenters[_currentFocusCenter]);
			Vector3 cameraDistanceVector =
				ClampedRadialVector(axisCenter, cameraZoomPosition);
			cameraTransform.position = axisCenter + cameraDistanceVector;
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
			StartCoroutine(MoveToFocus(0));
		}

		public void MoveToSeventhGrade()
		{
			StartCoroutine(MoveToFocus(1));
		}

		public void MoveToEighthGrade()
		{
			StartCoroutine(MoveToFocus(2));
		}

		private Vector3 ClampedRadialVector(Vector3 centerPoint, Vector3 satellitePoint)
		{
			Vector3 radialVector = satellitePoint - centerPoint;

			if (radialVector.magnitude < CameraMinDistance)
			{
				return radialVector.normalized * CameraMinDistance;
			}
			return Vector3.ClampMagnitude(radialVector, CameraMaxDistance);

		}
		
		private Vector3 DistanceMeasurementPoint(Transform focusPointCenter)
		{
			return new Vector3(focusPointCenter.position.x, cameraTransform.position.y, focusPointCenter.position.z);
		}
		
		#endregion
		
		#region COROUTINES
		
		private IEnumerator MoveToFocus(int focusIndex)
		{
			_cameraMoving = true;

			_currentFocusCenter = focusIndex;
			
			Vector3 startPos = transform.position;
			Vector3 endPos = focusPointCenters[focusIndex].position;
			
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