using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#else
using UnityEngine.SceneManagement;
#endif

public static class Utils
{

	public static void Shuffle<T>(this IList<T> list)  
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = Random.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}

	private static System.Random Random = new System.Random();


	public static Color GetRandomColor()
	{
		TUNNELColor randomColor = new TUNNELColor();
		randomColor.h = RandomValue(); 
		randomColor.s = RandomRange(0.3f, 0.4f);
		randomColor.l = RandomRange(0.45f, 0.6f); 
		randomColor.a = 1;

		return randomColor; 
	}

	public static bool IsEqual(this Color c, Color d)
	{
		if(c.r == d.r && c.g == d.g && c.b == d.b && c.a == d.a)
		{
			return true;
		}

		return false;
	}

	public static float RandomValue()
	{
		return (float)Random.NextDouble();
	}

	public static float RandomRange(float min, float max)
	{
		return min + ((float)Random.NextDouble() * (max - min));
	}

	public static int RandomRange(int min, int max)
	{
		return Random.Next(min, max);
	}

	public static bool IsVisibleFrom(this Transform transform, Camera camera)
	{
		if(!transform.gameObject.activeInHierarchy)
			return false;

		return transform.position.IsVisibleFrom(camera);
	}


	public static bool IsVisibleFrom(this Vector3 pos, Camera camera)
	{
		float width = camera.GetWidth();
		float height = camera.GetHeight();

		var left = camera.transform.position.x - width / 2f;
		var right = camera.transform.position.x + width / 2f;
		var top = camera.transform.position.y + height / 2f;
		var bottom = camera.transform.position.y - height / 2f;


		if(left < pos.x && pos.x < right && bottom < pos.y && pos.y < top)
			return true;

		return false;
	}


	public static float GetHeight(this Camera cam)
	{
		if(cam == null)
			return 0;

		return 2f * cam.orthographicSize;
	}

	public static float GetWidth(this Camera cam)
	{
		if(cam == null)
			return 0;

		return cam.GetHeight() * cam.aspect;
	}

	public static Color ToColor(TUNNELColor c)
	{
		float r, g, b, a = c.a;
		r = g = b = c.l;

		if (c.l <= 0)
			c.l = 0.001f;
		if (c.l >= 1)
			c.l = 0.999f;

		if (c.s != 0f)
		{
			var v2 = c.l < 0.5f ? c.l * (c.s + 1f) : c.l + c.s - c.l * c.s;
			var v1 = c.l * 2 - v2;
			r = GetRGB(v1, v2, c.h + 1f / 3f);
			g = GetRGB(v1, v2, c.h);
			b = GetRGB(v1, v2, c.h - 1f / 3f);
		}

		return new Color(r, g, b, a);
	}

	public static TUNNELColor FromColor(Color color)
	{
		float h = 0, s = 0, l = 0, a = color.a;

		float max = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
		float min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
		l = (max + min) / 2f;

		if (min != max)
		{
			float delta = max - min;
			s = l > 0.5f ? delta / (2f - max - min) : delta / (min + max);

			if (max == color.r)
			{
				h = (color.g - color.b) / delta + (color.g < color.b ? 6f : 0f);
			}
			else if (max == color.g)
			{
				h = (color.b - color.r) / delta + 2f;
			}
			else if (max == color.b)
			{
				h = (color.r - color.g) / delta + 4f;
			}

			h /= 6f;
		}

		return new TUNNELColor(h, s, l, a);
	}

	private static float GetRGB(float v1, float v2, float h)
	{
		if (h < 0) h += 1;
		if (h > 1f) h -= 1;
		if (h * 6f < 1f) return v1 + (v2 - v1) * h * 6f;
		if (h * 2f < 1f) return v2;
		if (h * 3f < 2f) return v1 + (v2 - v1) * (2f / 3f - h) * 6f;
		return v1;
	}

	public static void ReloadScene()
	{
		#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
		Application.LoadLevel (GetSceneName());
		#else
		SceneManager.LoadScene(GetSceneName());
		#endif
	}

	public static string GetSceneName()
	{
		#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
		return Application.loadedLevelName;
		#else
		return SceneManager.GetActiveScene().name;
		#endif
	}
}
