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

namespace LOS {

	/// <summary>
	/// Base class for the system's objects, like light, obstacle, and camera.
	/// </summary>
	[ExecuteInEditMode]
	public abstract class LOSObjectBase : MonoBehaviour {

	

		[Tooltip("Will not be considered in the LOS system.")]
		public bool isStatic;

		protected Transform _trans;
		protected Vector3 _previousPosition;
		protected Quaternion _previousRotation;


		public Vector3 position {get {return _trans.position;}}
		public Quaternion rotation {get {return _trans.rotation;}}


		protected virtual void Awake () {
			_trans = transform;
		}

		public virtual bool CheckDirty () {
			return _previousPosition != _trans.position || _previousRotation != _trans.rotation;
		}

		public virtual void UpdatePreviousInfo () {
			_previousPosition = position;
			_previousRotation = rotation;
		}
	}

}