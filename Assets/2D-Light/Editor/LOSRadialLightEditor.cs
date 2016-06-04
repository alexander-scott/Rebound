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
	
	[CustomEditor (typeof(LOSRadialLight))]
	public class LOSRadialLightEditor : LOSLightBaseEditor {
		protected SerializedProperty _radius;
		protected SerializedProperty _flashFrequency;
		protected SerializedProperty _flashOffset;

		private static float _previousTime;
		

		protected override void OnEnable () {
			base.OnEnable ();

			_radius = serializedObject.FindProperty("radius");
			_flashFrequency = serializedObject.FindProperty("flashFrequency");
			_flashOffset = serializedObject.FindProperty("flashOffset");

			var light = (LOSRadialLight) target;
			light.invertMode = false;

			serializedObject.ApplyModifiedProperties();
		}


	

		public override void OnInspectorGUI () {
			base.OnInspectorGUI ();

			EditorGUILayout.PropertyField(_radius);

			EditorGUILayout.PropertyField(_flashFrequency);
			if (_flashFrequency.intValue > 0) {
				EditorGUILayout.PropertyField(_flashOffset);

				if (_flashOffset.floatValue < 0) {
					EditorGUILayout.HelpBox("Flash offset should not be less than 0. Make it positive to work.", MessageType.Error);
				}
			}
			else if (_flashFrequency.intValue < 0) {
				EditorGUILayout.HelpBox("Flash frequency should not be less than 0. Make it positive to work.", MessageType.Error);
			}

			serializedObject.ApplyModifiedProperties();

			if (_flashFrequency.intValue > 0 && _flashOffset.floatValue > 0) {
				EditorUtility.SetDirty(target);
				var light = (LOSRadialLight) target;
				light.timeFromLastFlash += Time.realtimeSinceStartup - _previousTime;
				_previousTime = Time.realtimeSinceStartup;
			}
		}
	}
}
