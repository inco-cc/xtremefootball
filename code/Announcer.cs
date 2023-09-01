using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

[Category( "Xtreme Football" )]
[Icon( "mic" )]
public abstract partial class Announcer : Entity
{
	public static Announcer Current { get; private set; }

	public abstract string PhraseSound { get; }

	public Announcer()
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

#if DEBUG
	[ConCmd.Admin( "xf_announcer_say" )]
#endif
	public static void Say( string phrase )
	{
		Current.PlaySound( string.Format( Current.PhraseSound, phrase ) );

		Event.Run( "announcer.say", phrase );
	}

	[Event( "ball.reset" )]
	protected static void OnBallReset()
	{
		Say( "BallReset" );
	}
}
