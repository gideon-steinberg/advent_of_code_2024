void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	
	foreach (var line in lines)
	{
		var parts = line.Split("-");
		nodes.Add(parts[0]);
		nodes.Add(parts[1]);
		
		if (!edges.ContainsKey(parts[0]))
		{
			edges[parts[0]] = new HashSet<string>();
		}
		if (!edges.ContainsKey(parts[1]))
		{
			edges[parts[1]] = new HashSet<string>();
		}
		
		edges[parts[0]].Add(parts[1]);
		edges[parts[1]].Add(parts[0]);
	}
	
	var triples = new HashSet<string>();
	foreach (var node in nodes)
	{
		foreach (var edge1 in edges[node])
		{
			foreach (var edge2 in edges[edge1])
			{
				if (edge1 == edge2)
				{
					continue;
				}
				if (edges[node].Contains(edge2) && edges[node].Contains(edge1) && edges[edge1].Contains(edge2))
				{
					var list = new List<string>{node, edge1, edge2};
					triples.Add(string.Join(",", list.OrderBy(a => a)));
				}
			}
		}
	}
	double part1 = 0;
	
	foreach (var triple in triples)
	{
		var parts = triple.Split(",");
		if (parts[0].StartsWith("t") || parts[1].StartsWith("t") || parts[2].StartsWith("t"))
		{
			part1++;
		}
	}
	Console.WriteLine(part1);
	foreach (var node in nodes)
	{
		Solve(node, new HashSet<string>(), new List<string>());
	}
	Console.WriteLine(string.Join(",", connectedNodes.OrderBy(a => a.Count()).Last()));
}

HashSet<string> nodes = new HashSet<string>();
Dictionary<string, HashSet<string>> edges = new Dictionary<string, HashSet<string>>();
List<string> connectedNodes = new List<string>();

HashSet<string> seen = new HashSet<string>();

public void Solve(string node, HashSet<string> currentNodes, List<string> currentNodesInOrder)
{
	foreach (var edge in edges[node])
	{
		if (currentNodes.Contains(edge))
		{
			continue;
		}
		var valid = true;
		
		foreach (var existingNode in currentNodes)
		{
			if (!edges[existingNode].Contains(edge))
			{
				valid = false;
			}
		}
		
		if (!valid)
		{
			continue;
		}
		
		var newNodes = new HashSet<string>(currentNodes);
		newNodes.Add(edge);
		var newNodesInOrder = new List<string>(currentNodesInOrder);
		newNodesInOrder.Add(edge);
		
		var key = string.Join(",", newNodesInOrder.OrderBy(a => a));
		
		if (seen.Contains(key))
		{
			continue;
		}
		seen.Add(key);
		
		connectedNodes.Add(key);
		
		Solve(edge, newNodes, newNodesInOrder);
	}
}