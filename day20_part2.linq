<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

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
	
	var threshold = 98;
	
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
	
	int noCheatingEnd = optimalPath[endCol, endRow];	
	
	var part1 = 0;
	var part2 = 0;
	
	for (var col = 0; col < cols; col++)
	{
		for (var row = 0; row < rows; row++)
		{			
			if (needsACheat[col, row])
			{
				continue;
			}
			if (col == endCol && row == endRow)
			{
				continue;
			}
			for (var i = -21; i < 21; i++)
			{
				for (var j = -21; j <= 21 - i; j++)
				{
					if (Math.Abs(i) + Math.Abs(j) > 20 )
					{
						continue;
					}
					try 
					{
					    if (needsACheat[col + i, row + j])
						{
							continue;
						}
						var amountSaved = optimalPath[col + i, row + j] - optimalPath[col, row];
						if (amountSaved < noCheatingEnd && amountSaved > (threshold + Math.Abs(i) + Math.Abs(j)))
						{
							if ((j == 0 && (i == 2 || i == -2)) || (i == 0 && (j == 2 || j == -2)))
							{
								part1++;
							}
							part2++;
						}
					} catch {}
				}
			}
		}
	}
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}