void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine).ToList();
	
	var rows = lines[0].Count();
	var cols = lines.Count();
	
	var needsACheat = new bool[rows,cols];
	var optimalPath = new int[rows,cols];
	var startRow = -1;
	var startCol = -1;
	var endRow = -1;
	var endCol = -1;
	
	var threshold = 100;
	
	for (var i = 0; i < cols; i++)
	{
		for (var j = 0; j < rows; j++)
		{
			needsACheat[i,j] = lines[i][j] == '#';
			optimalPath[i,j] = 1000000;
			if (lines[i][j] == 'S')
			{
				startCol = i;
				startRow = j;
			}
			if (lines[i][j] == 'E')
			{
				endCol = i;
				endRow = j;
			}
		}
	}

	var bfsQueue = new List<Point>();
	var bfsSeen = new HashSet<Point>();
	bfsQueue.Add(new Point(startCol, startRow));
	var distance = 0;
	while (bfsQueue.Count() > 0)
	{
		var newBfsQueue = new List<Point>();
		
		foreach (var point in bfsQueue)
		{
			if (needsACheat[point.X, point.Y])
			{
				continue;
			}
			if (bfsSeen.Contains(point))
			{
				continue;
			}
			bfsSeen.Add(point);
			optimalPath[point.X, point.Y] = distance;
			newBfsQueue.Add(new Point(point.X + 1, point.Y));
			newBfsQueue.Add(new Point(point.X - 1, point.Y));
			newBfsQueue.Add(new Point(point.X, point.Y + 1));
			newBfsQueue.Add(new Point(point.X, point.Y - 1));
		}
		
		bfsQueue = newBfsQueue;
		distance++;
	}
	
	var queue = new HashSet<Point>();
	queue.Add(new Point(startCol, startRow));
	
	int noCheatingEnd = optimalPath[endCol, endRow];
	var part1 = 0;
	var seen = new HashSet<Point>();
	
	while (queue.Count() > 0)
	{
		var newQueue = new HashSet<Point>();
		
		foreach (var point in queue)
		{
			var col = point.X;
			var row = point.Y;
			
			if (needsACheat[col, row])
			{
				continue;
			}
			if (col == endCol && row == endRow)
			{
				continue;
			}
			if (seen.Contains(point))
			{
				continue;
			}
			seen.Add(point);
			
			try 
			{
				if (needsACheat[col + 1, row])
				{
					var amountSaved = optimalPath[col + 2, row] - optimalPath[col, row];
					if (amountSaved < noCheatingEnd && amountSaved > threshold)
					{
						part1++;
					}
				}
				else 
				{
					newQueue.Add(new Point(col + 1, row));
				}
			} catch {}
			try
			{
				if (needsACheat[col - 1, row])
				{
					var amountSaved = optimalPath[col - 2, row] - optimalPath[col, row];
					if (amountSaved < noCheatingEnd && amountSaved > threshold)
					{
						part1++;
					}
				}
				else 
				{
					newQueue.Add(new Point(col - 1, row));
				}
			} catch {}
			try
			{
				if (needsACheat[col, row + 1])
				{
					var amountSaved = optimalPath[col, row + 2] - optimalPath[col, row];
					if (amountSaved < noCheatingEnd && amountSaved > threshold)
					{
						part1++;
					}
				}
				else 
				{
					newQueue.Add(new Point(col, row + 1));
				}
			} catch {}
			
			try
			{
				if (needsACheat[col, row - 1])
				{
					var amountSaved = optimalPath[col, row - 2] - optimalPath[col, row];
					if (amountSaved < noCheatingEnd && amountSaved > threshold)
					{
						part1++;
					}
				}
				else 
				{
					newQueue.Add(new Point(col, row - 1));
				}
			} catch {}
		}
		queue = newQueue;
	}
	Console.WriteLine(part1);
}