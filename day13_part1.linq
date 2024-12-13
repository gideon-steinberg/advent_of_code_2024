void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	
	int sum = 0;
	
	for (var i = 0; i < lines.Count()+1; i+=4)
	{
		var ax = int.Parse(lines[i].Split(" ")[2].Replace("X+", "").Replace(",", ""));
		var ay = int.Parse(lines[i].Split(" ")[3].Replace("Y+", ""));
		
		var bx = int.Parse(lines[i+1].Split(" ")[2].Replace("X+", "").Replace(",", ""));
		var by = int.Parse(lines[i+1].Split(" ")[3].Replace("Y+", ""));
		
		var prizex = int.Parse(lines[i+2].Split(" ")[1].Replace("X=", "").Replace(",", ""));
		var prizey = int.Parse(lines[i+2].Split(" ")[2].Replace("Y=", ""));
		
		//Console.WriteLine($"{ax},{ay} : {bx},{by} : {prizex},{prizey}");
		
		var minCost = prizex;
		
		var maxa = prizex / ax * 10;
		var maxb = prizex / bx * 10;
		
		var found = false;
		
		for (var a = 0; a < maxa; a++)
		{
			for (var b = 0; b < maxb; b++)
			{
				var x = a * ax + b * bx;
				var y = a * ay + b * by;
				
				if (x == prizex && y == prizey)
				{
					var cost = a * 3 + b;
					minCost = Math.Min(cost, minCost);
					found = true;
				}
			}
		}
		
		if (found)
		{
			sum+= minCost;
		}
	}
	
	Console.WriteLine(sum);
	
}

// You can define other methods, fields, classes and namespaces here