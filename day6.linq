void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines[0].Count();
	var map = new bool[height, width];
	var guardLocationX = -1;
	var guardLocationY = -1;
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (lines[i][j] == '#')
			{
				map[i,j] = true;
			}
			if (lines[i][j] == '^')
			{
				guardLocationX = i;
				guardLocationY = j;
			}
		}
	}
	
	var part1 = 0;
	
	var visited = Traverse(height, width, guardLocationX, guardLocationY, map).visited;
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (visited[i,j,0] || visited[i,j,1] || visited[i,j,2] || visited[i,j,3])
			{
				part1++;
			}
		}
	}
	Console.WriteLine(part1);
	
	var part2 = 0;
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			var newMap = (bool[,])map.Clone();
			newMap[i,j] = true;
			var result = Traverse(height, width, guardLocationX, guardLocationY, newMap);
			if (!result.crashed)
			{
				part2++;
			}
		}
	}	
	
	Console.WriteLine(part2);
	
}

public class Result
{
	public bool[,,] visited;
	public bool crashed;
}


public Result Traverse(int height, int width, int guardLocationX, int guardLocationY, bool[,] map)
{
	var visited = new bool[height,width,4];
	
	var direction = 0;
	var directions = new List<List<int>>
	{
		new List<int>{-1, 0},
		new List<int>{ 0, 1},
		new List<int>{1, 0},
		new List<int>{0, -1},
	};
	
	var crashed = false;
	
	try 
	{
		while (!visited[guardLocationX,guardLocationY,direction])
		{
			visited[guardLocationX,guardLocationY,direction] = true;
			if (map[guardLocationX + directions[direction][0],guardLocationY + directions[direction][1]])
			{
				direction++;
				direction %=4;
			}
			else 
			{
				guardLocationX += directions[direction][0];
				guardLocationY += directions[direction][1];
			}
		}
	} 
	catch (IndexOutOfRangeException)
	{
		crashed = true;
	}
	return new Result {visited = visited, crashed = crashed};
}