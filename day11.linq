void Main()
{
	var input = @"";
	var stones = input.Split(" ").ToDictionary(i => double.Parse(i), i => (double)1);
	
	for (var i = 0; i < 75; i++)
	{
		if (i == 25)
		{
			Console.WriteLine(stones.Sum( s => s.Value));
		}
		var newStones = new Dictionary<double, double>();
		
		foreach(var stone in stones)
		{
			if (stone.Key == 0)
			{
				AddOrUpdate(newStones, 1, stone.Value);
			}
			else
			{
				var stoneAsString = stone.Key.ToString();
				if (stoneAsString.Count() % 2 == 0)
				{
					var halfLength = stoneAsString.Count()/2;
					var halfway = Math.Pow(10, halfLength);
					AddOrUpdate(newStones, Math.Floor(stone.Key / (halfway)), stone.Value);
					AddOrUpdate(newStones, stone.Key % (halfway), stone.Value);
				}
				else
				{
					AddOrUpdate(newStones, stone.Key * 2024, stone.Value);
				}
			}			
		}
		
		stones = newStones;
	}
	Console.WriteLine(stones.Sum( s => s.Value));
}

public void AddOrUpdate(Dictionary<double, double> stones, double key, double value)
{
	if (!stones.ContainsKey(key))
	{
		stones[key] = 0;
	}
	stones[key]+=value;
}