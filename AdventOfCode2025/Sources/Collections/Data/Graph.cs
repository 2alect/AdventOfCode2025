using System.Numerics;

namespace AdventOfCode2025.Collections.Data;

public abstract class Graph<T>
	where T : INumber<T>
{
	public List<List<T>> Links { get; private set; }

	public Graph(List<List<T>> links)
	{
		Links = links;
	}
}

public sealed class GraphI : Graph<int>
{
	public GraphI(List<List<int>> links) : base(links) { }
}
