<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	var input = @"";
	
	
	var split = input.Split(Environment.NewLine + Environment.NewLine);
	
	var lines = split[0].Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines[0].Count() * 2;
	
	var grid = new char[height,width];
	
	var robotY = -1;
	var robotX = -1;
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width/2; j++)
		{
			if (lines[i][j] == '#' || lines[i][j] == '.')
			{
				grid[i,2*j] = lines[i][j];
				grid[i,2*j+1] = lines[i][j];
			}
			if (lines[i][j] == '@')
			{
				grid[i,2*j] = lines[i][j];
				grid[i,2*j+1] = '.';
			}
			if (lines[i][j] == 'O')
			{
				grid[i,2*j] = '[';
				grid[i,2*j+1] = ']';
			}
			if (lines[i][j] == '@')
			{
				robotY = i;
				robotX = j * 2;
			}
		}
	}
	var count = 0;
	
	foreach (var c in split[1])
	{
		//Console.WriteLine(count);
		count++;
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
		
		if (directionx == 0)
		{
			// up and down
			var canMove = true;
			var toCheck = new Queue<Point>();
			toCheck.Enqueue(new Point {X=robotX, Y=robotY});
			var toMove = new List<Point>();
			while (toCheck.Count() > 0)
			{
				var point = toCheck.Dequeue();
				
				if (grid[point.Y, point.X] == '#')
				{
					canMove = false;
				}
				
				if (grid[point.Y, point.X] == '@')
				{
					toCheck.Enqueue(new Point{X = point.X, Y = point.Y + directiony});
					toMove.Add(point);
				}
				else if (grid[point.Y, point.X] == '[')
				{
					toCheck.Enqueue(new Point{X = point.X, Y = point.Y + directiony});
					toCheck.Enqueue(new Point{X = point.X + 1, Y = point.Y + directiony});
					toMove.Add(point);
				}
				else if (grid[point.Y, point.X] == ']')
				{
					toCheck.Enqueue(new Point{X = point.X, Y = point.Y + directiony});
					toCheck.Enqueue(new Point{X = point.X - 1, Y = point.Y + directiony});
					toMove.Add(point);
				}
			}
			
			if (canMove)
			{
				toMove.Reverse();
				foreach (var point in toMove)
				{				
					if (grid[point.Y, point.X] == '@')
					{
						grid[point.Y + directiony, point.X] = grid[point.Y, point.X];
						grid[point.Y, point.X] = '.';
					}
					else if (grid[point.Y, point.X] == '[')
					{
						grid[point.Y + directiony, point.X] = grid[point.Y, point.X];
						grid[point.Y + directiony, point.X + 1] = grid[point.Y, point.X + 1];
						grid[point.Y, point.X] = '.';
						grid[point.Y, point.X + 1] = '.';
					}
					else if (grid[point.Y, point.X] == ']')
					{
						grid[point.Y + directiony, point.X] = grid[point.Y, point.X];
						grid[point.Y + directiony, point.X - 1] = grid[point.Y, point.X - 1];
						grid[point.Y, point.X] = '.';
						grid[point.Y, point.X - 1] = '.';
					}
				}
				grid[robotY, robotX] = '.';
				robotY += directiony;
			}
		}
		else if (directiony != -2)
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
					grid[currentY,currentX] = grid[currentY - directiony, currentX - directionx];
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
			if (grid[i,j] == '[')
			{
				sum += 100 * i + j;
			}
		}
	}
	
	Console.WriteLine(sum);
}