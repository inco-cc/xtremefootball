using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

[ClassName( "announcer_s4" )]
[HammerEntity]
public partial class S4Announcer : Announcer
{
	public override string PhraseSound { get; } = "s4_{0}";

	[GameEvent.Entity.PostSpawn]
	private static void AutoCreate()
	{
		if ( Current == null )
			_ = new S4Announcer();
	}
}
