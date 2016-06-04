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
using UnityEditor;
using System.Collections;

namespace LOS.Editor {
	
	[CustomEditor (typeof(LOSFullScreenLight))]
	public class LOSFullScreenLightEditor : LOSLightBaseEditor {
		protected SerializedProperty _invertMode;


		protected override void OnEnable () {
			base.OnEnable();

			_invertMode = serializedObject.FindProperty("invertMode");
		}

		public override void OnInspectorGUI () {
			var light = (LOSFullScreenLight) target;

			if (!SHelper.CheckWithinScreen(light.transform.position, LOSManager.instance.losCamera.unityCamera, 0)) {
				EditorGUILayout.HelpBox(
					"Full Screen Light requires its position within the camera. " +
					"Otherwise, wired shadow will be present. I'm working on the next version to better solve the issue.", 
					MessageType.Warning);
			}

			base.OnInspectorGUI();

			EditorGUILayout.PropertyField(_invertMode);

			serializedObject.ApplyModifiedProperties();
		}
	}
}
