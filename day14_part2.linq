void Main()
{
	var input = @"";

	var width = 101;
	var height = 103;

	var lines = input.Split(Environment.NewLine);
	
	for (var t = 0; t < 10000; t++)
	{
		var grid = new int[width,height];
		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				grid[i,j] = 0;
			}
		}
		foreach (var line in lines)
		{
			var parts = line.Split(" ");
			var px = int.Parse(parts[0].Replace("p=", "").Split(",")[0]);
			var py = int.Parse(parts[0].Split(",")[1]);
			var vx = int.Parse(parts[1].Split(",")[0].Replace("v=", ""));
			var vy = int.Parse(parts[1].Split(",")[1]);
		
			double x = px + (vx * t);
			double y = py + (vy * t);
			while (x <= 0)
			{
				x+=width;
			}
			while (y <= 0)
			{
				y+=height;
			}
			x %= width;
			y %= height;
		
			grid[(int)x,(int)y]++;
		}
		var numGood = 0;
		for (var a = 1; a < 100; a++)
		{
			if (grid[54,a] > 0)
			{
				numGood++;
			}
		}
		if (numGood > 30)
		{
			Console.WriteLine(t);
			for (var j = 0; j < height; j++)
			{
				for (var i = 0; i < width; i++)	
				{
					if (grid[i,j] > 0)
					{
						Console.Write("X");
					}
					else
					{
						Console.Write(" ");
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine("======================");
		}
	}
}
