using Godot;
using System;
using System.Linq;


public partial class Rocket : StaticBody3D
{
	GpuParticles3D Explosion, Smoke;
	CharacterBody3D Target;
	MeshInstance3D Mesh;
	Vector3 axis = Vector3.Left;
	private Vector3 Clamp = new Vector3(.01f,.01f,.01f);
	private float Speed = 1f;
	public Vector3 Force;
	private Vector3 Velocity;
	private float Damp = .1f;
	double time = 0;
	public static Vector3 Lerp(Vector3 First, Vector3 Second, float amount){
		float x = Mathf.Lerp(First.X, Second.X, amount);
		float y = Mathf.Lerp(First.Y, Second.Y, amount);
		float z = Mathf.Lerp(First.Z, Second.Z, amount);

		return new Vector3(x,y,z);
	}
	public override void _Ready()
	{
		
		base._Ready();
		Target = (CharacterBody3D)GetTree().Root.GetNode("Scene").GetNode("Player");
		Mesh = (MeshInstance3D)GetTree().Root.GetNode("Scene").GetNode("Rocket").GetNode("Mesh");
		Explosion = (GpuParticles3D)GetParent().GetNode<GpuParticles3D>("Explosion");

        GD.Print(Explosion);
		Force = new Vector3(0,.1f,0);
		Velocity = Force;
		
	}
	public void _Spawn(){
		GD.Randomize();

		Position = ((new Vector3(
			GD.Randf(),
			GD.Randf(),
			GD.Randf()
		))  - Vector3.One/2) * 25 +  Vector3.Up * 15;
		Force = (new Vector3(
			GD.Randf(),
			GD.Randf(),
			GD.Randf()
		)) - Vector3.One / 2 ;
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Mesh.RotateObjectLocal(Vector3.Up, Mathf.DegToRad(5) % 360);
		Vector3 Direction = (Target.Position.DirectionTo(Position)*Speed + Force);
		Quaternion Quat = Transform.LookingAt(Position + Direction).Basis.GetRotationQuaternion();
		Quat = Quaternion.Slerp(Quat, 0.05f);
		Quaternion = Quat;
		Vector3 Rot = Quat.GetEuler();
		float roll = Mathf.DegToRad(Rot.X*30) ;
		float pitch = Mathf.DegToRad(Rot.Y*45) ;
		float yaw = Mathf.DegToRad(Rot.Z*60) ;
		Transform3D transform = new Transform3D();
		transform.Basis = new Basis(Quaternion.FromEuler(new Vector3(roll, pitch ,yaw)));
		Velocity = transform.Basis.Z*Speed;
		KinematicCollision3D Collision = MoveAndCollide(Velocity);
		if(Collision != null){
			if(Collision.GetCollider().Equals(Target)){
				GpuParticles3D explosion = (GpuParticles3D)Explosion.Duplicate();
				Smoke = explosion.GetNode<GpuParticles3D>("Smoke");
                OmniLight3D Light = explosion.GetNode<OmniLight3D>("OmniLight3D");

                explosion.Position = Transform.Origin;
				explosion.Emitting = true;				
				explosion.GetNode<Timer>("Timer").Autostart = true;
				Light.LightEnergy = 6.805f;

                Smoke.Position = Transform.Origin;
                Smoke.Emitting = true;

                GetParent().AddChild(explosion);
				_Spawn();
			}
			else{
				Force = -Collision.GetNormal()*Speed;
			}
		}else{
			Force = Lerp(Force, Vector3.Zero, Damp);
		}
	}
}

