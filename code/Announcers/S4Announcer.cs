using System;
using Sandbox;
using Editor;

namespace XtremeFootball.Announcers;

[ClassName( "announcer_s4" )]
[HammerEntity]
public partial class S4Announcer : BaseAnnouncer
{
	public override string PhraseSound { get; } = "s4_{0}";

	[Sandbox.GameEvent.Entity.PostSpawn]
	protected static void AutoSpawn()
	{
		if ( Current == null || !Current.IsValid )
			_ = new S4Announcer();
	}
}
