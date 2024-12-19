void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var towels = lines[0].Split(", ");
	
	var part1 = 0;
	double part2 = 0;
	foreach (var line in lines.Skip(2))
	{
		var possibilities = new Dictionary<string, double>{{line, 1}};
		var changed = true;
		
		while (changed)
		{
			changed = false;
			var newPossibilities = new Dictionary<string, double>();
			foreach (var possibility in possibilities.Keys)
			{
				if (possibility == "")
				{
					if (!newPossibilities.ContainsKey(possibility))
					{
						newPossibilities[possibility] = 0;
					}
					newPossibilities[possibility] += possibilities[possibility];
					continue;
				}
				foreach (var towel in towels)
				{
					if (possibility.StartsWith(towel))
					{
						var newTowel = possibility.Substring(towel.Count());
						
						if (!newPossibilities.ContainsKey(newTowel))
						{
							newPossibilities[newTowel] = possibilities[possibility];
						}
						else 
						{
							newPossibilities[newTowel] += possibilities[possibility];
						}
						changed = true;
					}
				}
			}
			possibilities = newPossibilities;
		}
		if (possibilities.ContainsKey(""))
		{
		
			part1++;
			part2 += possibilities[""];
		}
	}
	
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}