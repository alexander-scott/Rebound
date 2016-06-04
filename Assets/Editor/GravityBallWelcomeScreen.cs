using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;

[InitializeOnLoad]
public class GravityBallWelcomeScreen : EditorWindow 
{
	
	private const string ONLINE_DOC_URL = "https://dl.dropboxusercontent.com/u/8289407/GravityBall/Documentation/html/annotated.html";
	private const string RATEUS_URL = "http://u3d.as/mns";

	private const string VERYSIMPLEAD_URL = "http://u3d.as/oWD";

	private const string FACEBOOK_URL = "https://facebook.com/appadvisory";
	private const string REQUEST_URL = "https://appadvisory.zendesk.com/hc/en-us/requests/new";
	private const string APPADVISORY_UNITY_CATALOG_URL = "http://u3d.as/9cs";
	private const string COMMUNITY_URL = "https://appadvisory.zendesk.com/hc/en-us/community/topics";

	private const float width = 500;
	private const float height = 710;

	private const string PREFSHOWATSTARTUP = "AppAdvisory.PREFSHOWATSTARTUP";

	private static bool showAtStartup;
	private static GUIStyle imgHeader;
	private static bool interfaceInitialized;
	private static Texture adsIcon;
	private static Texture onlineDocIcon;
	private static Texture moreGamesIcon;
	private static Texture rateIcon;
	private static Texture communityIcon;
	private static Texture topicIcon;
	private static Texture questionIcon;
	private static Texture facebookIcon;

	[MenuItem("Edit/APP ADVISORY/Gravity Ball/Welcome Screen", false, 0)]
	[MenuItem("GameObject/APP ADVISORY/Gravity Ball/Welcome Screen", false, 0)]
	[MenuItem("Assets/APP ADVISORY/Gravity Ball/Welcome Screen", false, 0)]
	[MenuItem("Tools/APP ADVISORY/Gravity Ball/Welcome Screen", false, 0)]
	[MenuItem("Window/APP ADVISORY/Gravity Ball/Welcome Screen", false, 0)]
	public static void OpenWelcomeWindow(){
		GetWindow<GravityBallWelcomeScreen>(true);
	}


	static GravityBallWelcomeScreen(){
		EditorApplication.playmodeStateChanged -= OnPlayModeChanged;
		EditorApplication.playmodeStateChanged += OnPlayModeChanged;
		
		showAtStartup = PlayerPrefs.GetInt(PREFSHOWATSTARTUP, 1) == 1;
		
		if (showAtStartup){
			EditorApplication.update -= OpenAtStartup;
			EditorApplication.update += OpenAtStartup;
		}
	}

	static void OpenAtStartup(){
		OpenWelcomeWindow();
		EditorApplication.update -= OpenAtStartup;
	}

	static void OnPlayModeChanged(){
		EditorApplication.update -= OpenAtStartup;
		EditorApplication.playmodeStateChanged -= OnPlayModeChanged;
	}
	
	void OnEnable(){
		#if UNITY_5_3_OR_NEWER
		titleContent = new GUIContent("Welcome To Gravity Ball"); 
		#endif

		maxSize = new Vector2(width, height);
		minSize = maxSize;
	}	

	public void OnGUI(){

		InitInterface();
		GUI.Box(new Rect(0, 0, width, 60), "", imgHeader);
//		GUI.Label( new Rect(width-90,45,200,20),new GUIContent("Version : " +VERSION));
		GUILayoutUtility.GetRect(position.width, 64);
		GUILayout.Space(40);
		GUILayout.BeginVertical();

		if (Button(adsIcon,"WANT TO MONETIZE THIS ASSET?","Get 'Very Simple Ad' on the Asset Store and earn money in a minute!")){
			Application.OpenURL(VERYSIMPLEAD_URL);
		}

		GUILayout.Space(10);

		if (Button(communityIcon,"Join the community and get access to direct download","Be informed of the latest updates.")){
			Application.OpenURL(COMMUNITY_URL);
		}

		if (Button(rateIcon,"Rate this asset","Write us a review on the asset store.")){
			Application.OpenURL(RATEUS_URL);
		}

		if (Button(moreGamesIcon,"More Unity assets from us","Have a look to our Unity's Asset Store catalog!")){
			Application.OpenURL(APPADVISORY_UNITY_CATALOG_URL);
		}

		if (Button(questionIcon,"A request? Looking for a freelancer?","Don't hesitate to write me, whatever you want.")){
			Application.OpenURL(REQUEST_URL);
		}

		if (Button(facebookIcon,"Facebook page","Follow us on Facebook.")){
			Application.OpenURL(FACEBOOK_URL);
		}

		if (Button(onlineDocIcon,"Online documentation","Read the full documentation.")){
			Application.OpenURL(ONLINE_DOC_URL);
		}

		GUILayout.Space(3);

		bool show = GUILayout.Toggle(showAtStartup, "Show at startup");
		if (show != showAtStartup){
			showAtStartup = show;
			int i = GetInt(showAtStartup);
			Debug.Log("toggle i = " + i);
			PlayerPrefs.SetInt(PREFSHOWATSTARTUP, i);
			PlayerPrefs.Save();
		}

		GUILayout.EndVertical();

	}

	int GetInt(bool b)
	{
		if(b)
			return 1;
		else
			return 0;
	}

	void InitInterface(){

		if (!interfaceInitialized){
			imgHeader = new GUIStyle();
			imgHeader.normal.background = (Texture2D)Resources.Load("appadvisoryBanner");
			imgHeader.normal.textColor = Color.white;

			adsIcon = (Texture)Resources.Load("btn_monetization") as Texture;
			onlineDocIcon = (Texture)Resources.Load("btn_onlinedoc") as Texture;
			communityIcon = (Texture)Resources.Load("btn_community") as Texture;
			moreGamesIcon = (Texture)Resources.Load("btn_moregames") as Texture;
			rateIcon = (Texture)Resources.Load("btn_rate") as Texture;
			questionIcon = (Texture)Resources.Load("btn_question") as Texture;
			facebookIcon = (Texture)Resources.Load("btn_facebook") as Texture;

			interfaceInitialized = true;
		}
	}
	
	bool Button(Texture texture, string heading, string body, int space=10){

		GUILayout.BeginHorizontal();
		
		GUILayout.Space(54);
		GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(48));
		GUILayout.Space(10);
		
		GUILayout.BeginVertical();
		GUILayout.Space(1);
		GUILayout.Label(heading, EditorStyles.boldLabel);
		GUILayout.Label(body);
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
			
		bool returnValue = false;
		if (Event.current.type == EventType.mouseDown && rect.Contains(Event.current.mousePosition)){
			returnValue = true;
		}
		
		GUILayout.Space(space);

		return returnValue;
	}
}
