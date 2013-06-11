using UnityEngine;
using System.Collections;

public class Subs{
	
	#region Random Subs
	
	/// <summary>
	/// Random float from 0f to 1f(ex).
	/// </summary>
	public static float RandomFloat(){
		return Random.Range(0f,1f);
	}
	/// <summary>
	/// Random int from 0 to 100(ex)
	/// </summary>
	public static float RandomPercent(){
		return Random.Range(0,100);
	}
	
	public static bool RandomBool(){
		if (RandomPercent()<50)
			return true;
		return false;
	}
	/// <summary>
	/// Random vector3. All values from -1f to 1f.
	/// </summary>

	public static Vector3 RandomVector3(){
		return new Vector3(Random.Range(-1f,1),Random.Range(-1f,1),Random.Range(-1f,1));
	}
	#endregion

	public static void SendMessageRecursive(Transform tform,string message){
		
		foreach(Transform t in tform){
			t.SendMessage(message,SendMessageOptions.DontRequireReceiver);
			SendMessageRecursive(t,message);
		}
	}

	public static Vector3 LengthDir(Transform transform,Vector3 dir)
	{
		return transform.position+transform.TransformDirection(dir);
	}
	
	/// <summary>
	/// Wraps the specified number according to min and max.
	/// Doesn't take multiple revolutions into account.
	/// </summary>
	public static int Wrap (int number, int min, int max)
	{
		if (number<min)
			return max+number;
		if (number>=max)
			return min+(number-max);
		return number;
	}
}
