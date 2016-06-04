//The MIT License (MIT)
//
//Copyright (c) 2015 Yifeng
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//	The above copyright notice and this permission notice shall be included in all
//	copies or substantial portions of the Software.
//
//	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//	SOFTWARE.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LOS {

	/// <summary>
	/// LOS manager is a singleton.
	/// It coordinates the system & provide common functions for others to use.
	/// </summary>
	[ExecuteInEditMode]
	public class LOSManager : MonoBehaviour {
		public PhysicsOpt physicsOpt;
		public float viewboxExtension = 1.01f;
		public bool debugMode;

		
//		public float collidersExtension = 1.001f;

		[HideInInspector]
		public Vector2 halfViewboxSize {get {return losCamera.halfViewboxSize;}}

		[HideInInspector]
		public bool is2D {get {return physicsOpt == PhysicsOpt.Physics_2D;}}

		private static LOSManager _instance;

		private List<LOSObstacle> _obstacles;
		private List<LOSLightBase> _lights;
		private Transform _losCameraTrans;
		private LOSCamera _losCamera;
		private bool _isDirty;


		/// <summary>
		/// Gets the instance of the singleton.
		/// </summary>
		/// <value>The instance.</value>
		public static LOSManager instance {
			get {
				if (_instance == null) {
					_instance = FindObjectOfType<LOSManager>();

					if (_instance == null) {
						var go = new GameObject();
						go.name = "LOSManager";
						_instance = go.AddComponent<LOSManager>();
						DontDestroyOnLoad(go);

						_instance.Init();
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// Tries the get instance.
		/// It is useful when you need to get the singleton near the end of an object's life cycle,
		/// 	as there are chances that it is the end of play mode, it's ok for the singleton to return null.
		/// </summary>
		/// <returns>The get instance.</returns>
		public static LOSManager TryGetInstance () {
			return _instance;
		}


		public List<LOSObstacle> obstacles {
			get {
				if (_obstacles == null) {
					_obstacles = new List<LOSObstacle>();
				}
				return _obstacles;
			}
		}

		public List<LOSLightBase> lights {
			get {
				if (_lights == null) {
					_lights = new List<LOSLightBase>();
				}
				return _lights;
			}
		}

		/// <summary>
		/// Gets the viewbox.
		/// Viewbox is the screen rect in world space.
		/// </summary>
		/// <value>The viewbox.</value>
		private List<LOSCamera.ViewBoxLine> viewbox {
			get {
				return losCamera.viewbox;
			}
		}

		/// <summary>
		/// Gets the four viewbox corners.
		/// </summary>
		/// <value>The viewbox corners.</value>
		public List<Vector3> viewboxCorners {
			get {
				return losCamera.viewboxCorners;
			}
		}

		public LOSCamera losCamera {
			get {
				if (_losCamera == null) {
					var losCameras = FindObjectsOfType<LOSCamera>();
					if (losCameras.Length == 0) {
						Debug.LogError("No LOSCamera is found in the scene! Please remember to attach LOSCamera to the camera gameobjeect.");
					}
					else if (losCameras.Length == 1) {
						_losCamera = losCameras[0];
					}
					else if (losCameras.Length > 1) {
						Debug.LogError("More than 1 LOSCamera found!");
					}
				}
				return _losCamera;
			}
		}
		public Transform losCameraTrans {
			get {
				if (_losCameraTrans == null) {
					_losCameraTrans = losCamera.transform;
				}
				return _losCameraTrans;
			}
		}


		void Start () {
			Init();

			StartCoroutine (CoUpdate ());
		}
			
		IEnumerator CoUpdate()
		{
			while (true) 
			{
				UpdateLights ();

				yield return null;
			}
		}

		void OnEnable () 
		{
			foreach (var light in lights) 
			{
				light.ToggleOnOff(true);
			}
		}

		void OnDisable () {
			foreach (var light in lights) {
				if (light != null) {
					light.ToggleOnOff(false);
				}
			}

			StopAllCoroutines ();
		}

		/// <summary>
		/// Updates the lights. 
		/// It is the place tells the lights to draw.
		/// </summary>
		public void UpdateLights () {
			if (losCamera.CheckDirty()) {
				UpdateViewingBox();
			}

			foreach (var light in lights) {
				light.TryDraw();
			}
			
			UpdatePreviousInfo();
		}

		private void UpdatePreviousInfo () {
			_isDirty = false;
			
			losCamera.UpdatePreviousInfo();

			foreach (LOSObstacle obstacle in obstacles) {
				obstacle.UpdatePreviousInfo();
			}
		}

		private void Init () {
			_instance = this;

			UpdateViewingBox();
		}

		private void UpdateViewingBox () {
			losCamera.UpdateViewingBox();
		}
		
		public void AddObstacle (LOSObstacle obstacle) {
			if (!obstacles.Contains(obstacle)) {
				_isDirty = true;
				obstacles.Add(obstacle);
			}
		}

		public void RemoveObstacle (LOSObstacle obstacle) {
			obstacles.Remove(obstacle);
			_isDirty = true;
		}

		public void AddLight (LOSLightBase light) {
			if (!lights.Contains(light)) {
				lights.Add(light);
			}
		}

		public void RemoveLight (LOSLightBase light) {
			lights.Remove(light);
		}

		public bool CheckDirty () {
			if (_isDirty) return true;

			return true;

			bool result = false;
			foreach (LOSObstacle obstacle in obstacles) {
				if (!obstacle.isStatic && obstacle.CheckDirty()) {
					result = true;
				}
			}

			if (!Application.isPlaying ) {
				UpdatePreviousInfo();
			}

			return result;
		}


		// ------------ Helper Functions ------------
		public Vector3 GetPointForRadius (Vector3 origin, Vector3 direction, float radius) {
			float c = direction.magnitude;
			
			float x = radius * direction.x / c + origin.x;
			float y = radius * direction.y / c + origin.y;
			return new Vector3(x, y, 0);
		}
		


		// ------------End------------


		public enum PhysicsOpt {
			Physics_3D,
			Physics_2D,
		}

	}
}
	
