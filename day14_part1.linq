void Main()
{
	var input = @"";

	var width = 101;
	var height = 103;

	var lines = input.Split(Environment.NewLine);
	
	var q1 = 0;
	var q2 = 0;
	var q3 = 0;
	var q4 = 0;
	

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
		
		var x = px + (vx * 100);
		var y = py + (vy * 100);
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
		
		grid[x,y]++;
		
		if (x < width/2 && y < height/2)
		{
			q1++;
		}
		else if (x > width/2 && y < height/2)
		{
			q2++;
		}
		else if (x < width/2 && y > height/2)
		{
			q3++;
		}
		else if (x > width/2 && y > height/2)
		{
			q4++;
		}
	}
	
	//Console.WriteLine($"{q1},{q2},{q3},{q4}");
	Console.WriteLine(q1 * q2 * q3 *q4);
}
