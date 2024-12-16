<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var rows = lines.Count();
	var cols = lines[0].Count();
	
	var grid = new char[rows,cols];
	var scores = new int[rows,cols];
	var facing = new Facing[rows,cols];
	var valid = new bool[rows,cols];
	var possible = new bool[rows,cols];
	
	var startRow = -1;
	var startCol = -1;
	
	for (var i = 0; i < rows; i++)
	{
		for (var j = 0; j < cols; j++)
		{
			grid[i,j] = lines[i][j];
			facing[i,j] = Facing.EastWest;
			scores[i,j] = 100000000;
			valid[i,j] = false;
			possible[i,j] = false;
			if (grid[i,j] == 'S')
			{
				startRow = i;
				startCol = j;
				scores[i,j] = 0;
			}
		}
	}
	
	var queue = new HashSet<Point>();
	var seen = new HashSet<Point>();
	
	queue.Add(new Point{X = startRow, Y = startCol});
	
	var score = -1;
	
	while (queue.Count() > 0)
	{
		var point = queue.OrderBy(p => scores[p.X, p.Y]).First();
		queue.Remove(point);
		if (grid[point.X, point.Y] == '#')
		{
			scores[point.X, point.Y] = 100000000;
			continue;
		}
		if (seen.Contains(point))
		{
			continue;
		}
		seen.Add(point);
		
		if (grid[point.X, point.Y] == 'E')
		{
			score = scores[point.X, point.Y];
			break;
		}
		
		if (facing[point.X, point.Y] != Facing.EastWest)
		{
			if (scores[point.X + 1, point.Y] > scores[point.X, point.Y] + 1)
			{
				scores[point.X + 1, point.Y] = scores[point.X, point.Y] + 1;
				facing[point.X + 1, point.Y] = Facing.NorthSouth;
			}
			if (scores[point.X - 1, point.Y] > scores[point.X, point.Y] + 1)
			{
				scores[point.X - 1, point.Y] = scores[point.X, point.Y] + 1;
				facing[point.X - 1, point.Y] = Facing.NorthSouth;
			}
		}
		else 
		{
			if (scores[point.X + 1, point.Y] > scores[point.X, point.Y] + 1001)
			{
				scores[point.X + 1, point.Y] = scores[point.X, point.Y] + 1001;
				facing[point.X + 1, point.Y] = Facing.NorthSouth;
			}
			if (scores[point.X - 1, point.Y] > scores[point.X, point.Y] + 1001)
			{
				scores[point.X - 1, point.Y] = scores[point.X, point.Y] + 1001;
				facing[point.X - 1, point.Y] = Facing.NorthSouth;
			}
		}
		
		if (facing[point.X, point.Y] != Facing.NorthSouth)
		{
			if (scores[point.X, point.Y+1] > scores[point.X, point.Y] + 1)
			{
				scores[point.X, point.Y+1] = scores[point.X, point.Y] + 1;
				facing[point.X, point.Y+1] = Facing.EastWest;
			}
			if (scores[point.X, point.Y-1] > scores[point.X, point.Y] + 1)
			{
				scores[point.X, point.Y-1] = scores[point.X, point.Y] + 1;
				facing[point.X, point.Y-1] = Facing.EastWest;
			}
		}
		else 
		{
			if (scores[point.X, point.Y+1] > scores[point.X, point.Y] + 1001)
			{
				scores[point.X, point.Y+1] = scores[point.X, point.Y] + 1001;
				facing[point.X, point.Y+1] = Facing.EastWest;
			}
			if (scores[point.X, point.Y-1] > scores[point.X, point.Y] + 1001)
			{
				scores[point.X, point.Y-1] = scores[point.X, point.Y] + 1001;
				facing[point.X, point.Y-1] = Facing.EastWest;
			}
		}

		queue.Add(new Point{X = point.X + 1, Y = point.Y});
		queue.Add(new Point{X = point.X - 1, Y = point.Y});
		queue.Add(new Point{X = point.X, Y = point.Y + 1});
		queue.Add(new Point{X = point.X, Y = point.Y - 1});
	}
	Console.WriteLine($"Part 1: {score}");
	
	// My search sucks. Do this because... why not
	for (var i = 0; i < rows; i++)
	{
		for (var j = 0; j < cols; j++)
		{
			if (grid[i,j] == '#')
			{
				score = 100000000;
			}
		}
	}
	
	var endQueue = new Queue<Point>();
	endQueue.Enqueue(new Point{X = startRow, Y = startCol});
	seen = new HashSet<Point>();
	
	while (endQueue.Count() > 0)
	{
		var point = endQueue.Dequeue();
		if (grid[point.X, point.Y] == '#')
		{
			continue;
		}
		
		if (seen.Contains(point))
		{
			continue;
		}
		seen.Add(point);
		
		endQueue.Enqueue(new Point{X = point.X + 1, Y = point.Y});
		endQueue.Enqueue(new Point{X = point.X - 1, Y = point.Y});
		endQueue.Enqueue(new Point{X = point.X, Y = point.Y + 1});
		endQueue.Enqueue(new Point{X = point.X, Y = point.Y - 1});
		
		var mainScore = scores[point.X, point.Y];
		var upScore = scores[point.X, point.Y-1];
		var downScore = scores[point.X, point.Y+1];
		var leftScore = scores[point.X-1, point.Y];
		var rightScore = scores[point.X+1, point.Y];
		
		if (mainScore == upScore + 1 || mainScore == upScore + 1001)
		{
			if (mainScore < downScore || mainScore < leftScore || mainScore < rightScore)
			{
				possible[point.X, point.Y] = true;
			}
		}
		if (mainScore == downScore + 1 || mainScore == downScore + 1001)
		{
			if (mainScore < upScore || mainScore < leftScore || mainScore < rightScore)
			{
				possible[point.X, point.Y] = true;
			}
		}
		if (mainScore == leftScore + 1 || mainScore == leftScore + 1001)
		{
			if (mainScore < downScore || mainScore < upScore || mainScore < rightScore)
			{
				possible[point.X, point.Y] = true;
			}
		}
		if (mainScore == rightScore + 1 || mainScore == rightScore + 1001)
		{
			if (mainScore < downScore || mainScore < leftScore || mainScore < upScore)
			{
				possible[point.X, point.Y] = true;
			}
		}	
	}
	
	endQueue = new Queue<Point>();
	endQueue.Enqueue(new Point{X = startRow, Y = startCol});
	seen = new HashSet<Point>();
	
	while (endQueue.Count() > 0)
	{
		var point = endQueue.Dequeue();
		if (grid[point.X, point.Y] == '#')
		{
			continue;
		}
		if (seen.Contains(point))
		{
			continue;
		}
		seen.Add(point);
		
		endQueue.Enqueue(new Point{X = point.X + 1, Y = point.Y});
		endQueue.Enqueue(new Point{X = point.X - 1, Y = point.Y});
		endQueue.Enqueue(new Point{X = point.X, Y = point.Y + 1});
		endQueue.Enqueue(new Point{X = point.X, Y = point.Y - 1});
		
		var isValid = false;
		var searchQueue = new HashSet<Point>();
		searchQueue.Add(point);
		while (searchQueue.Count > 0)
		{
			var searchPoint = searchQueue.First();
			searchQueue.Remove(searchPoint);
			
			if (grid[searchPoint.X, searchPoint.Y] == '#')
			{
				continue;
			}
			
			if (grid[searchPoint.X, searchPoint.Y] == 'E')
			{
				isValid = true;
				break;
			}
			
			if (scores[searchPoint.X+1, searchPoint.Y] == scores[searchPoint.X, searchPoint.Y] + 1 ||
			    scores[searchPoint.X+1, searchPoint.Y] == scores[searchPoint.X, searchPoint.Y] + 1001)
			{
				searchQueue.Add(new Point{X = searchPoint.X + 1, Y = searchPoint.Y});
			}
			
			if (scores[searchPoint.X-1, searchPoint.Y] == scores[searchPoint.X, searchPoint.Y] + 1 ||
			    scores[searchPoint.X-1, searchPoint.Y] == scores[searchPoint.X, searchPoint.Y] + 1001)
			{
				searchQueue.Add(new Point{X = searchPoint.X - 1, Y = searchPoint.Y});
			}
			
			if (scores[searchPoint.X, searchPoint.Y+1] == scores[searchPoint.X, searchPoint.Y] + 1 || 
				scores[searchPoint.X, searchPoint.Y+1] == scores[searchPoint.X, searchPoint.Y] + 1001)
			{
				searchQueue.Add(new Point{X = searchPoint.X, Y = searchPoint.Y+1});
			}
			
			if (scores[searchPoint.X, searchPoint.Y-1] == scores[searchPoint.X, searchPoint.Y] + 1 ||
			    scores[searchPoint.X, searchPoint.Y-1] == scores[searchPoint.X, searchPoint.Y] + 1001)
			{
				searchQueue.Add(new Point{X = searchPoint.X, Y = searchPoint.Y-1});
			}
		}
		if (isValid)
		{
			valid[point.X,point.Y] = true;
		}
	}
	
	// If you need it :(
	//scores.Dump();
	
	var optimalPathAmount = 0;
	for (var i = 0; i < rows; i++)
	{
		for (var j = 0; j < cols; j++)
		{
			if (valid[i,j])
			{
				optimalPathAmount++;
				Console.Write("1");
			}
			else if (possible[i,j])
			{
				Console.Write("2");
			}
			else 
			{
				Console.Write(" ");
			}
		}
		Console.WriteLine();
	}
	Console.WriteLine($"Part2: {optimalPathAmount} + manual checking");
}

public enum Facing
{
	None,
	NorthSouth,
	EastWest
}