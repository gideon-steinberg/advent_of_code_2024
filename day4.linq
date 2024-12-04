void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines.First().Count();
	
	var map = new char[height,width];
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			map[i,j] = lines[i][j];
		}
	}
	
	var part1 = 0;
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (i > 2 && j > 2)
			{
				if (map[i,j] == 'X' && map[i-1,j-1] == 'M' && map[i-2,j-2] == 'A' && map[i-3,j-3] == 'S')
				{
					part1++;
				}
			}
			if (i > 2)
			{
				if (map[i,j] == 'X' && map[i-1,j] == 'M' && map[i-2,j] == 'A' && map[i-3,j] == 'S')
				{
					part1++;
				}
			}
			if (j > 2)
			{
				if (map[i,j] == 'X' && map[i,j-1] == 'M' && map[i,j-2] == 'A' && map[i,j-3] == 'S')
				{
					part1++;
				}
			}
			if (i < height - 3 && j < width - 3)
			{
				if (map[i,j] == 'X' && map[i+1,j+1] == 'M' && map[i+2,j+2] == 'A' && map[i+3,j+3] == 'S')
				{
					part1++;
				}
			}
			if (i < height - 3)
			{
				if (map[i,j] == 'X' && map[i+1,j] == 'M' && map[i+2,j] == 'A' && map[i+3,j] == 'S')
				{
					part1++;
				}
			}
			if (j < width - 3)
			{
				if (map[i,j] == 'X' && map[i,j+1] == 'M' && map[i,j+2] == 'A' && map[i,j+3] == 'S')
				{
					part1++;
				}
			}
			if (i > 2 && j < width - 3)
			{
				if (map[i,j] == 'X' && map[i-1,j+1] == 'M' && map[i-2,j+2] == 'A' && map[i-3,j+3] == 'S')
				{
					part1++;
				}
			}
			if (j > 2 && i < height - 3)
			{
				if (map[i,j] == 'X' && map[i+1,j-1] == 'M' && map[i+2,j-2] == 'A' && map[i+3,j-3] == 'S')
				{
					part1++;
				}
			}
		}
	}
	
	Console.WriteLine(part1);
	
	var part2 = 0;
	
	for (var i = 1; i < height -1; i++)
	{
		for (var j = 1; j < width -1; j++)
		{
			if (map[i,j] == 'A')
			{
				var count = 0;
				if ((map[i-1,j-1] == 'M' && map[i+1,j+1] == 'S') || (map[i-1,j-1] == 'S' && map[i+1,j+1] == 'M'))
				{
					count++;
				}
				if ((map[i+1,j-1] == 'M' && map[i-1,j+1] == 'S') || (map[i+1,j-1] == 'S' && map[i-1,j+1] == 'M'))
				{
					count++;
				}
				
				if (count == 2)
				{
					part2++;
				}
			}
		}
	}
	Console.WriteLine(part2);
}