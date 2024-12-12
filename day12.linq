<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines[0].Count();
	var regions = new Dictionary<int, HashSet<Point>>();
	
	var grid = new char[height, width];
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			grid[i,j] = lines[i][j];
		}
	}
	
	var regionNumber = 0;
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (grid[i,j] != '.')
			{
				var points = new List<Point>();
				
				var queue = new Queue<Point>();
				queue.Enqueue(new Point(i,j));
				
				regions[regionNumber] = new HashSet<Point>();
				
				var seen = new HashSet<Point>();
				
				var charToMatch = grid[i,j];
				
				while (queue.Count > 0)
				{
					var point = queue.Dequeue();
					if (seen.Contains(point))
					{
						continue;
					}
					
					seen.Add(point);
					regions[regionNumber].Add(point);
					grid[point.X, point.Y] = '.';
					
					try {
						if (grid[point.X + 1, point.Y] == charToMatch)
						{
							queue.Enqueue(new Point(point.X + 1, point.Y));
						}
					} catch {}
					
					try {
						if (grid[point.X - 1, point.Y] == charToMatch)
						{
							queue.Enqueue(new Point(point.X - 1, point.Y));
						}
					} catch {}
					
					try {
						if (grid[point.X ,point.Y + 1] == charToMatch)
						{
							queue.Enqueue(new Point(point.X, point.Y+1));
						}
					} catch {}
					
					try {
						if (grid[point.X, point.Y-1] == charToMatch)
						{
							queue.Enqueue(new Point(point.X, point.Y-1));
						}
					} catch {}
				}
				
				regionNumber++;
			}
		}
	}
	
	double part1 = 0;
	double part2 = 0;
	
	foreach(var region in regions)
	{
		var points = region.Value.ToList();
		var edgeCount = points.Count * 4;
		var edges = new HashSet<Point>();
		
		foreach (var point in points)
		{
			edges.Add(new Point(point.X * 10, point.Y * 10 + 5));
			edges.Add(new Point(point.X * 10, point.Y * 10 - 5));
			edges.Add(new Point(point.X * 10 + 5, point.Y * 10));
			edges.Add(new Point(point.X * 10 - 5, point.Y * 10));
		}
		
		for (var i = 0; i < points.Count(); i++)
		{
			var first = points[i];
			
			for (var j = i; j < points.Count(); j++)
			{
				var second = points[j];
				
				if (first.X == second.X && first.Y == second.Y + 1)
				{
					edgeCount-=2;
					edges.Remove(new Point(first.X * 10, first.Y * 10 - 5));
				}
				if (first.X == second.X && first.Y == second.Y - 1)
				{
					edgeCount-=2;
					edges.Remove(new Point(first.X * 10, first.Y * 10 + 5));
				}
				if (first.X == second.X + 1 && first.Y == second.Y)
				{
					edgeCount-=2;
					edges.Remove(new Point(first.X * 10 - 5, first.Y * 10));
				}
				if (first.X == second.X - 1 && first.Y == second.Y)
				{
					edgeCount-=2;
					edges.Remove(new Point(first.X * 10 + 5, first.Y * 10));
				}
			}
		}
		
		part1 += edgeCount * points.Count();
		
		double sides = 0;
		var seen = new List<Point>();
		foreach (var edge in edges.OrderBy(p => p.X).ThenBy(p => p.Y))
		{
			var newSide = true;
			foreach (var point in seen)
			{
				if ((edge.X + 10) %10 == 5)
				{
					if (
					   edge.X     == point.X      && edge.Y     == point.Y + 10
					|| edge.X     == point.X      && edge.Y +10 == point.Y
					)
					{
						var isCross = false;
						foreach (var np1 in edges)
						{
							if (np1 == edge) continue;
							foreach (var np2 in edges)
							{
								if (np2 == point) continue;
								if (np2 == edge) continue;
								if (!(np1.Y == np2.Y && (np1.X == np2.X + 10 || np1.X + 10 == np2.X))) continue;
								if ((edge.X < np1.X && edge.X > np2.X) &&
								(np1.Y < edge.Y && np1.Y > point.Y))
								{
										isCross = true;
								}
							}
						}
						if (!isCross)
						{
							newSide = false;
						}
					}
				} else if ((edge.Y + 10) %10 == 5)
				{
				
					if(
					   edge.X     == point.X +10  && edge.Y     == point.Y
					|| edge.X +10 == point.X      && edge.Y     == point.Y
					)
					{
						var isCross = false;
						foreach (var np1 in edges)
						{
							if (np1 == edge) continue;
							foreach (var np2 in edges)
							{
								if (np2 == point) continue;
								if (np2 == edge) continue;
								if (!(np1.X == np2.X && (np1.Y == np2.Y + 10 || np1.Y + 10 == np2.Y))) continue;
								if ((edge.Y < np1.Y && edge.Y > np2.Y) &&
								(np1.X < edge.X && np1.X > point.X))
								{
										isCross = true;
								}
							}
						}
						if (!isCross)
						{
							newSide = false;
						}
					}
				}
				else
				{
					throw new Exception("This shouldn't happen");
				}
			}
			if (newSide)
			{
				sides++;
			}
			seen.Add(edge);
		}
		
		part2 += sides * points.Count();
	}
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}