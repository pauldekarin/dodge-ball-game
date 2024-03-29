using Godot;
using System;


public partial class Rocket : CsgCombiner3D
{
	
	Vector3 axis = Vector3.Left;
	private Vector3 speed = new Vector3(0,.1f,0);
	private float ratio = .01f;
	public override void _Ready()
	{
		
		base._Ready();
		
	}
	public override void _PhysicsProcess(double delta)
	{
		
		base._PhysicsProcess(delta);
		CharacterBody3D target = (CharacterBody3D)GetNode("/root").GetChild(0).GetNode("Player");
		Vector3 target_pos = target.Position;
		Vector3 direction = (target_pos - Position).Normalized();
		float x = Mathf.Lerp(speed.Normalized().X, direction.X, 1f);
		float y = Mathf.Lerp(speed.Normalized().Y, direction.Y, 1f);
		float z = Mathf.Lerp(speed.Normalized().Z, direction.Z, 1f);
		Vector3 step = new Vector3(x,y,z);
		step *= ratio;
		Position += step;
		
	}
}
