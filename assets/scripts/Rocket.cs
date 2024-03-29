using Godot;
using System;

public partial class Rocket : CsgCombiner3D
{
	private Vector3 speed = new Vector3(.1f,.1f,.1f);
	private float ratio = .5f;
	public override void _Ready()
	{
		base._Ready();
		
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		CharacterBody3D target = (CharacterBody3D)GetNode("/root").GetChild(0).GetNode("Player");
		Vector3 target_pos = target.Position;
		Rotation = Rotation.AngleTo(target_pos);
	}
}
