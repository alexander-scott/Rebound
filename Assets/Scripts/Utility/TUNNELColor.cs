using UnityEngine;
using System;
using System.Collections;

[Serializable]
public struct TUNNELColor 
{
    public float h,s,l,a;
    public TUNNELColor(float h, float s, float l, float a = 1){this.h = h; this.s = s; this.l = l; this.a = a;}
    public TUNNELColor(Color c){TUNNELColor temp = Utils.FromColor(c); h = temp.h; s = temp.s; l = temp.l; a = temp.a;}
	public static implicit operator TUNNELColor(Color src){return Utils.FromColor(src);}
	public static implicit operator Color(TUNNELColor src){return Utils.ToColor(src);}
}
