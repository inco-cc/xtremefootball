using System;
using System.Collections.Generic;
using Sandbox;
using Editor;

namespace XtremeFootball;

[Category( "Xtreme Football" )]
[Icon( "propane_tank" )]
public abstract partial class Item : Prop
{
	private static readonly List<Item> all = new();
	public static new IReadOnlyList<Item> All => all.AsReadOnly();

	public virtual uint ExpireDelay { get; } = 20;
	[Net] public float ExpireTime { get; protected set; }
	public bool IsExpired => Time.Now >= ExpireTime;

	public Item()
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

	[GameEvent.Tick.Server]
	protected void ExpireTick()
	{
		if ( IsExpired )
		{
			Event.Run( "item.expire", this );

			Expire();
		}	
	}
}
