using Godot;
using System;

public partial class Explosion : GpuParticles3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		GetNode<Timer>("Timer").Timeout += () => {
			QueueFree();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)

	{
		
	}
	
}
