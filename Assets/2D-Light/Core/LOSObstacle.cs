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
	/// LOSObstacles provides mechanism for the system to tell if the system is changed.
	/// </summary>
	public class LOSObstacle : LOSObjectBase {

		[Tooltip("If the obstacle's center is offscreen, how much distance from the screen edge should be enough for the system" +
			"to consider it is fully offscreen?")]
		public float offScreenDistance = 3;
		public virtual List<Vector2> vertices {get; set;}
		private LayerMask _previousLayerMask;


		protected override void Awake () {
			base.Awake ();

			_previousLayerMask = gameObject.layer;
		}

//		protected virtual void OnEnable () {
//			LOSManager.instance.AddObstacle(this);
//		}
//
//		protected virtual void OnDisable () {
//			if (LOSManager.TryGetInstance() != null) {		// Have to check in case the manager is destroyed when scene end.
//				LOSManager.instance.RemoveObstacle(this);
//			}
//		}

		public override bool CheckDirty () {
			bool withinScreen = SHelper.CheckWithinScreen(position, LOSManager.instance.losCamera.unityCamera, offScreenDistance) || !Application.isPlaying;
			return withinScreen && (base.CheckDirty () || gameObject.layer != _previousLayerMask);
		}

		public override void UpdatePreviousInfo () {
			base.UpdatePreviousInfo ();

			_previousLayerMask = gameObject.layer;
		}

	}

}