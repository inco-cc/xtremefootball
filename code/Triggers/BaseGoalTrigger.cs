using System;
using Sandbox;
using Editor;
using XtremeFootball.Teams;

namespace XtremeFootball.Triggers;

[Category( "Xtreme Football" )]
[Icon( "flag" )]
public abstract partial class BaseGoalTrigger : TriggerMultiple
{
	public abstract BaseTeam Team { get; }
}
