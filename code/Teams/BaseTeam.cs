using System.Collections.Generic;
using Sandbox;
using XtremeFootball.SpawnPoints;

namespace XtremeFootball.Teams;

[Category("Xtreme Football")]
[Icon("groups")]
public abstract partial class BaseTeam : Entity
{
	private static readonly List<BaseTeam> all = new();
	public static new IReadOnlyList<BaseTeam> All => all.AsReadOnly();

	public IReadOnlyList<Player> Players
	{
		get
		{
			List<Player> players = new(Player.All);

			foreach (var player in players)
				if (player.Team != this)
					players.Remove(player);

			return players.AsReadOnly();
		}
	}

	public IReadOnlyList<BaseSpawnPoint> SpawnPoints
	{
		get
		{
			List<BaseSpawnPoint> spawnPoints = new(BaseSpawnPoint.All);

			foreach (var spawnPoint in spawnPoints)
				if (spawnPoint.Team != this)
					spawnPoints.Remove(spawnPoint);

			return spawnPoints;
		}
	}

	public abstract new string Name { get; }
	public abstract ColorHsv Color { get; }

	[Net]
	public uint Goals { get; set; }

	public BaseTeam()
	{
		Transmit = TransmitType.Always;

		all.Add(this);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		all.Remove(this);
	}
}
