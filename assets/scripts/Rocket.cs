using Godot;
using System;

public partial class Rocket : CsgCylinder3D
{
	public override void _Ready()
	{
		base._Ready();
		Console.Write("Help");
		Position = new Vector3(1,1,1);
	}
}
