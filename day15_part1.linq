void Main()
{
	var input = @"";
	
	var split = input.Split(Environment.NewLine + Environment.NewLine);
	
	var lines = split[0].Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines[0].Count();
	
	var grid = new char[height,width];
	
	var robotY = -1;
	var robotX = -1;
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			grid[i,j] = lines[i][j];
			if (grid[i,j] == '@')
			{
				robotY = i;
				robotX = j;
			}
		}
	}
	
	foreach (var c in split[1])
	{
		var directiony = -2;
		var directionx = -2;
		if (c == '^')
		{
			directiony = -1;
			directionx = 0;
		}
		else if (c == '<')
		{
			directiony = 0;
			directionx = -1;
		}
		else if (c == '>')
		{
			directiony = 0;
			directionx = 1;
		}
		else if (c == 'v')
		{
			directiony = 1;
			directionx = 0;
		}
		
		if (directiony != -2)
		{
			var currentX = robotX;
			var currentY = robotY;
			while (!(grid[currentY, currentX] == '#' || grid[currentY, currentX] == '.'))
			{
				currentX += directionx;
				currentY += directiony;
			}
			
			if (grid[currentY, currentX] == '.')
			{
				while (!(currentY == robotY && currentX == robotX))
				{
					grid[currentY,currentX] = 'O';
					currentX -= directionx;
					currentY -= directiony;
				}
				grid[robotY, robotX] = '.';
				robotY += directiony;
				robotX += directionx;
				grid[robotY, robotX] = '@';
			}
		}
	}
	
	double sum = 0;
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (grid[i,j] == 'O')
			{
				sum += 100 * i + j;
			}
		}
	}
	
	Console.WriteLine(sum);
}

// You can define other methods, fields, classes and namespaces here