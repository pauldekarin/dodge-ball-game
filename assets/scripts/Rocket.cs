using Godot;
using System;


public partial class Rocket : StaticBody3D
{
	CharacterBody3D Target;
	Vector3 axis = Vector3.Left;
	private float Speed = .3f;
	public Vector3 Force = new Vector3(0,.7f,.7f)*5;
	private float Damp = .1f;
	public static Vector3 Lerp(Vector3 First, Vector3 Second, float amount){
		float x = Mathf.Lerp(First.X, Second.X, amount);
		float y = Mathf.Lerp(First.Y, Second.Y, amount);
		float z = Mathf.Lerp(First.Z, Second.Z, amount);

		return new Vector3(x,y,z);
	}
	public override void _Ready()
	{
		
		base._Ready();
		_Spawn();
		Target = (CharacterBody3D)GetTree().Root.GetNode("Scene").GetNode("Player");
		
	}
	public void _Spawn(){
		GD.Randomize();

		Position = ((new Vector3(
			GD.Randf(),
			GD.Randf(),
			GD.Randf()
		))  - Vector3.One/2) * 25;
		Force = (new Vector3(
			GD.Randf(),
			GD.Randf(),
			GD.Randf()
		)) - Vector3.One / 2;
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector3 Direction = Position.DirectionTo(Target.Position);
		Vector3 DirectionVector = (Target.GlobalTransform.Origin - Position).Normalized()*Speed + Force;
		KinematicCollision3D Collision =  MoveAndCollide(DirectionVector.Normalized()*Speed);
		if(Collision != null && Collision.GetCollider().Equals(Target)){
			_Spawn();
		}else{
			LookAt(DirectionVector + Position);
			Force = Lerp(Force, Vector3.Zero, Damp);
		}
	}
}

