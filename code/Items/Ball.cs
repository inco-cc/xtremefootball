using System;
using Sandbox;
using Editor;
using XtremeFootball.Panels;

namespace XtremeFootball.Items;

[ClassName( "item_ball" ), Icon( "sports_football" ), HammerEntity]
public partial class Ball : BaseItem
{
	public static Ball Current { get; private set; }

	[Net] public Vector3 ResetPosition { get; protected set; }
	[Net] public Rotation ResetRotation { get; protected set; }

	private BallTimer Timer { get; set; }

	private Sound FlySound { get; set; }
	private float FlySpeed { get; set; }

	public Ball()
	{
		Transmit = TransmitType.Always;

		if ( Game.IsServer )
		{
			ResetPosition = Position;
			ResetRotation = Rotation;
		}

		if ( Game.IsClient )
			FlySound = PlaySound( "ball_fly" );

		Current?.Delete();

		Current = this;
	}

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/roller.vmdl" );

		LocalScale = .75f;
	}

	public override void ClientSpawn()
	{
		base.ClientSpawn();

		Timer = new( this );
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if ( Current == this )
			Current = null;
	}

	protected override void OnPhysicsCollision( CollisionEventData eventData )
	{
		base.OnPhysicsCollision( eventData );

		if ( eventData.Speed > 30 )
			PlaySound( "ball_click" );

		var normal = eventData.Velocity.Normal;

		Velocity = (eventData.Speed * .75f * (2 * eventData.Normal * eventData.Normal.Dot( normal * -1 ) + normal));
	}

	protected override void Expire()
	{
		Reset();
	}

	public void Reset()
	{
		Sound.FromWorld( "ball_explode", Position );

		Position = ResetPosition;
		Rotation = ResetRotation;

		Velocity = Vector3.Zero;
		AngularVelocity = Angles.Zero;

		ResetExpireTime();

		Event.Run( "ball.reset", this );
	}

	[Sandbox.GameEvent.Entity.PostSpawn]
	protected static void AutoSpawn()
	{
		_ = Current ?? new();
	}

	[Sandbox.GameEvent.Client.Frame]
	protected void UpdateSingSound()
	{
		FlySpeed = MathX.Approach( FlySpeed, Velocity.Length, Time.Delta * 3500 );

		if ( Owner is null )
		{
			FlySound.SetVolume( MathF.Pow( MathX.Clamp( FlySpeed / 1000, .05f, .9f ), .5f ) );
			FlySound.SetPitch( .75f + MathX.Clamp( FlySpeed / 1500, 0, 1 ) );
		}
		else
			FlySound.SetVolume( 0 );
	}

	[Sandbox.GameEvent.Client.Frame]
	protected void UpdateTimer()
	{
		Timer.Position = Position + Vector3.Up * 16;
		Timer.Rotation = Camera.Rotation.RotateAroundAxis( Vector3.Up, 180 );

		var distance = Camera.Position.Distance( Timer.Position );

		Timer.WorldScale = MathF.Max( distance / 128, 1 );
	}

	[ConCmd.Admin( "xf_ball_reset" )]
	protected static void ResetCommand()
	{
		Current?.Reset();
	}
}
