using Godot;
using System;


public partial class Rocket : CsgCombiner3D
{
	
	Vector3 axis = Vector3.Left;
	private float speed = .3f;
	private Vector3 f = new Vector3(0,.7f,.7f)*5;
	private float ratio = .1f;
	public static Vector3 Lerp(Vector3 First, Vector3 Second, float amount){
		float x = Mathf.Lerp(First.X, Second.X, amount);
		float y = Mathf.Lerp(First.Y, Second.Y, amount);
		float z = Mathf.Lerp(First.Z, Second.Z, amount);

		return new Vector3(x,y,z);
	}
	public override void _Ready()
	{
		
		base._Ready();
		
		
		
	}
	public override void _PhysicsProcess(double delta)
	{
		
		base._PhysicsProcess(delta);
		
		CharacterBody3D Target = (CharacterBody3D)GetNode("/root").GetChild(0).GetNode("Player");
		Vector3 DirectionVector = (Target.GlobalTransform.Origin - Position).Normalized()*speed + f;
		Position += DirectionVector.Normalized()*speed;
		LookAt(Position + DirectionVector, Vector3.Left);


		f = Lerp(f, Vector3.Zero, .1f);
		// GD.Print(f);
	}
}

