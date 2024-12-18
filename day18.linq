<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine).ToList();
	
	var rows = 71;
	var cols = 71;
	
	
	// for test input :)
	//var rows = 7;
	//var cols = 7;
	
	var part1 = IsReachable(lines, 1024, rows, cols);
	
	Console.WriteLine(part1);
	
	// We know 1024 is reachable, so there is no point starting from zero
	for (var i = 1024; i < lines.Count(); i++)
	{
		var reachable = IsReachable(lines, i, rows, cols);
		if (reachable == -1)
		{
			Console.WriteLine(i-1);
			Console.WriteLine(lines[i-1]);
			break;
		}
	}
	
}

public int IsReachable(List<string> lines, int steps, int rows, int cols)
{
	var grid = new bool[rows,cols];
	for (var i = 0; i < rows; i++)
	{
		for (var j = 0; j < cols; j++)
		{
			grid[i,j] = false;
		}
	}
	
	foreach (var line in lines.Take(steps))
	{
		var parts = line.Split(",");
		var col = int.Parse(parts[1]);
		var row = int.Parse(parts[0]);
		grid[row,col] = true;
	}

	var queue = new HashSet<Point>{new Point{X=0, Y = 0}};
	var seen = new HashSet<Point>();
	
	var count = -1;
	
	while (queue.Count() > 0)
	{
		count++;
		var newQueue = new HashSet<Point>();
		foreach (var point in queue)
		{
			if (point.X == rows -1 && point.Y == cols -1)
			{
				return count;
			}
			try {
				if (grid[point.X, point.Y])
				{
					continue;
				}
			}
			catch
			{
				continue;
			}
		
			seen.Add(point);
			if (!seen.Contains(new Point(point.X + 1, point.Y)))
			{
				newQueue.Add(new Point(point.X + 1, point.Y));
			}
			if (!seen.Contains(new Point(point.X - 1, point.Y)))
			{
				newQueue.Add(new Point(point.X - 1, point.Y));
			}
			if (!seen.Contains(new Point(point.X ,point.Y + 1)))
			{
				newQueue.Add(new Point(point.X, point.Y+1));
			}
			
			if (!seen.Contains(new Point(point.X, point.Y-1)))
			{
				newQueue.Add(new Point(point.X, point.Y-1));
			}
		}	
		queue = newQueue;
	}
	
	return -1;
}