using UnityEngine;

namespace NotificationSys{
// Standard notification class. For specific needs subclass
	public class Explosion_note:Notification
	{
	    public Vector3 Position;
		public float Force,Radius;
		
	    public Explosion_note(Vector3 position,float force,float radius):base(NotificationType.Explode)
	    {
			Position=position;
			Force=force;
			Radius=radius;
	    }
		
		public void addForce(Rigidbody rbody){
			
			rbody.AddExplosionForce(Force,Position,Radius);
		}
	}
	
	public class DisengageParent_note:Notification
	{
	    public Vector3 Velocity;
		
	    public DisengageParent_note(Vector3 velocity):base(NotificationType.DisengageParent)
	    {
			Velocity=velocity;
	    }
	}
}