using Godot;
using System;

public partial class lightExplosion : OmniLight3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = this.GetParent<GpuParticles3D>().GetNode<Timer>("Timer");
        timer.Timeout += () => {
			this.QueueFree();
        };
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		this.LightEnergy += 6.805f / 10;

    }
}
