using System;
using Sandbox;
using Editor;
using XtremeFootball.Items;

namespace XtremeFootball.Announcers;

[Category( "Xtreme Football" )]
[Icon( "mic" )]
public abstract partial class BaseAnnouncer : Entity
{
	public static BaseAnnouncer Current { get; private set; }

	public abstract string PhraseSound { get; }

	public BaseAnnouncer()
	{
		Transmit = TransmitType.Always;

		if ( Current != null && Current.IsValid )
			Current.Delete();

		Current = this;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if ( Current == this )
			Current = null;
	}

	public void Say( string phrase )
	{
		PlaySound( string.Format( PhraseSound, phrase ) );

		Event.Run( "announcer.say", this, phrase );
	}

	[GameEvent.Ball.Reset]
	protected void OnBallReset( Ball _ )
	{
		Say( "BallReset" );
	}

	[ConCmd.Admin( "xf_announcer_say" )]
	protected static void SayCommand( string phrase )
	{
		if ( Current != null && Current.IsValid )
			Current.Say( phrase );
	}
}
