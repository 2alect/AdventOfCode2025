namespace AdventOfCode2025.Collections.Data;

public class DisjoinSet<TKey> where TKey : notnull
{
	private Dictionary<TKey, TKey> parent = new();
	private Dictionary<TKey, int> rank = new();

	public void Add(TKey x)
	{
		if (!parent.ContainsKey(x))
		{
			parent[x] = x;
			rank[x] = 0;
		}
	}

	public TKey Find(TKey x)
	{
		if (!parent.ContainsKey(x))
			Add(x);

		if (!EqualityComparer<TKey>.Default.Equals(parent[x], x))
		{
			parent[x] = Find(parent[x]);
		}

		return parent[x];
	}

	public bool Union(TKey x, TKey y)
	{
		TKey rootX = Find(x);
		TKey rootY = Find(y);

		if (EqualityComparer<TKey>.Default.Equals(rootX, rootY))
			return false;

		int rankX = rank[rootX];
		int rankY = rank[rootY];

		if (rankX < rankY)
		{
			parent[rootX] = rootY;
		}
		else if (rankX > rankY)
		{
			parent[rootY] = rootX;
		}
		else
		{
			parent[rootY] = rootX;
			rank[rootX]++;
		}

		return true;
	}

	public bool Connected(TKey x, TKey y)
	{
		return EqualityComparer<TKey>.Default.Equals(Find(x), Find(y));
	}

	public Dictionary<TKey, List<TKey>> GetAllGroups()
	{
		Dictionary<TKey, List<TKey>> groups = new();

		foreach (TKey x in parent.Keys)
		{
			var root = Find(x);
			if (!groups.ContainsKey(root))
				groups[root] = new();

			groups[root].Add(x);
		}

		return groups;
	}
}
