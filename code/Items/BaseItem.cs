using System;
using System.Collections.Generic;
using Sandbox;
using Editor;

namespace XtremeFootball.Items;

[Category( "Xtreme Football" ), Icon( "propane_tank" )]
public abstract partial class BaseItem : Prop
{
	private static readonly List<BaseItem> all = new();
	public static new IReadOnlyList<BaseItem> All => all.AsReadOnly();

	[Net] public float ExpireTime { get; protected set; }
	public float TimeUntilExpire => ExpireTime - Time.Now;
	public virtual uint ExpireDelay { get; } = 20;
	public bool IsExpired => ExpireTime > -1 && Time.Now >= ExpireTime;

	public BaseItem()
	{
		all.Add( this );
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		all.Remove( this );
	}

	protected virtual void Expire()
	{
		Delete();
	}

	public void ResetExpireTime()
	{
		ExpireTime = Time.Now + ExpireDelay;
	}

	public void DisableExpireTime()
	{
		ExpireTime = -1;
	}

	[Sandbox.GameEvent.Tick.Server]
	protected void AutoExpire()
	{
		if ( IsExpired )
		{
			Event.Run( "item.expire", this );

			Expire();
		}
	}
}
