using System;
using Sandbox;
using Editor;

namespace XtremeFootball.Items;

[ClassName( "item_ball" ), Icon( "sports_football" ), HammerEntity]
public partial class Ball : BaseItem
{
	public static Ball Current { get; private set; }

	[Net] public Vector3 ResetPosition { get; protected set; }
	[Net] public Rotation ResetRotation { get; protected set; }

	private Sound flySound;
	private float flySpeed;

	public Ball()
	{
		Transmit = TransmitType.Always;

		if ( Game.IsServer )
		{
			ResetPosition = Position;
			ResetRotation = Rotation;
		}

		if ( Game.IsClient )
			flySound = PlaySound( "ball_fly" );

		Current?.Delete();

		Current = this;
	}

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/roller.vmdl" );

		LocalScale = .75f;
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

	[Sandbox.GameEvent.Tick.Client]
	protected void UpdateSingSound()
	{
		flySpeed = MathX.Approach( flySpeed, Velocity.Length, Time.Delta * 3500 );

		if ( Owner is null )
		{
			flySound.SetVolume( MathF.Pow( MathX.Clamp( flySpeed / 1000, .05f, .9f ), .5f ) );
			flySound.SetPitch( .75f + MathX.Clamp( flySpeed / 1500, 0, 1 ) );
		}
		else
			flySound.SetVolume( 0 );
	}

	[ConCmd.Admin( "xf_ball_reset" )]
	protected static void ResetCommand()
	{
		Current?.Reset();
	}
}
