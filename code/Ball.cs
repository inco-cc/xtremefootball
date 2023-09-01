using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

[ClassName( "item_ball" )]
[Icon( "sports_football" )]
[HammerEntity]
public partial class Ball : Item
{
	public static Ball Current { get; private set; }

	[Net] public Vector3 ResetPosition { get; protected set; }
	[Net] public Rotation ResetRotation { get; protected set; }

	private Sound flySound;
	private float flySpeed;

	public Ball()
	{
		if ( Sandbox.Game.IsServer )
		{
			ResetPosition = Position;
			ResetRotation = Rotation;
		}

		if ( Sandbox.Game.IsClient )
			flySound = PlaySound( "ball_fly" );
			

		if ( Current != null && Current.IsValid )
			Current.Delete();

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

#if DEBUG
	[ConCmd.Admin( "xf_ball_reset" )]
#endif
	public static void Reset()
	{
		Sound.FromWorld( "ball_explode", Current.Position );

		Current.Position = Current.ResetPosition;
		Current.Rotation = Current.ResetRotation;

		Current.Velocity = Vector3.Zero;
		Current.AngularVelocity = Angles.Zero;

		Current.ResetExpireTime();

		Event.Run( "ball.reset" );
	}

	[GameEvent.Entity.PostSpawn]
	protected static void AutoCreate()
	{
		if ( Current == null || !Current.IsValid )
			_ = new Ball();
	}

	[GameEvent.Tick.Client]
	protected void UpdateFlySound()
	{
		flySpeed = MathX.Approach( flySpeed, Velocity.Length, Time.Delta * 3500 );

		if ( Owner != null && Owner.IsValid )
			flySound.SetVolume( 0 );
		else
		{
			flySound.SetVolume( MathF.Pow( MathX.Clamp( flySpeed / 1000, .05f, .9f ), .5f ) );
			flySound.SetPitch( .75f + MathX.Clamp( flySpeed / 1500, 0, 1 ) );
		}
	}
}
