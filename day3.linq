void Main()
{
	var input = @""; 
	
	var mulRegex = new Regex("mul\\((\\d+,\\d+)\\)");
	var doRegex = new Regex("do\\(\\)");
	var dontRegex = new Regex("don't\\(\\)");	
	
	var mulMatches = mulRegex.Matches(input);
	var doMatches = doRegex.Matches(input);
	var dontMatches = dontRegex.Matches(input);
	
	var doIndexes = new List<int>();
	var dontIndexes = new List<int>();
	
	for (var i = 0; i < doMatches.Count; i++)
	{
		foreach (dynamic group in doMatches[i].Groups)
		{
			doIndexes.Add(group.Index);
		}
	}
	
	for (var i = 0; i < dontMatches.Count; i++)
	{
		foreach (dynamic group in dontMatches[i].Groups)
		{
			dontIndexes.Add(group.Index);
		}
	}
	
	double part1 = 0;
	double part2 = 0;
	
	for (var i = 0; i < mulMatches.Count; i++)
	{
		foreach (dynamic group in mulMatches[i].Groups)
		{
			string val = group.Value;
			if (!val.StartsWith("mul"))
			{
				var index = (int)group.Index;
				
				var smallestDoIndex = -1;
				var smallestDontIndex = -2;
				
				foreach (var doIndex in doIndexes)
				{
					if (index > doIndex)
					{
						smallestDoIndex = doIndex;
					}
				}
				
				foreach (var dontIndex in dontIndexes)
				{
					if (index > dontIndex)
					{
						smallestDontIndex = dontIndex;
					}
				}
				
				var parts = val.Split(",");
				if (smallestDoIndex > smallestDontIndex)
				{				
					part2 += Int32.Parse(parts[0]) * Int32.Parse(parts[1]);
				}
				part1 += Int32.Parse(parts[0]) * Int32.Parse(parts[1]);
			}
		}
	}
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}
