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
	/// Max exclusive.
	/// </summary>
	public static int Wrap(int number, int min, int max)
	{
		var b=number%(max-min);
		if (b>=0)
			return min+b;
		return max+b;
	}
	
	/// <summary>
	/// Adds and wraps the specified number according to min and max.
	/// Max exclusive.
	/// </summary>
	public static int Add(int number, int min, int max)
	{
		return Add(number,1,min,max);
	}
	
	/// <summary>
	/// Adds and wraps the specified number according to min and max.
	/// Max exclusive.
	/// </summary>
	public static int Add(int number,int amount, int min, int max)
	{
		return Wrap (number+amount,min,max);
	}
}
