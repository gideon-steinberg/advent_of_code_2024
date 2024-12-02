void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var part1 = 0;
	var part2 = 0;
	foreach (var line in lines)
	{
		var parts = line.Split(" ").Select(i => Int32.Parse(i)).ToList();
		if (IsLineSafe(parts))
		{
			part1++;
			part2++;
		}
		else
		{
			var safe = false;
			for (var i = 0; i < parts.Count; i++)
			{
				var clone = new List<int>(parts);
				clone.RemoveAt(i);
				if (IsLineSafe(clone))
				{
					safe = true;
				}
			}
			if (safe)
			{
				part2++;
			}
		}
	}
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}

public static bool IsSafe(int num1, int num2, int increasingOrDecreasing)
{
	var diff = num1 - num2;
	if (Math.Sign(diff) != increasingOrDecreasing)
	{
		return false;
	}
	if (diff == 0 || Math.Abs(diff) > 3)
	{
		return false;
	}
	return true;
}

public static bool IsLineSafe(List<int> parts)
{
	var current = parts[0];
	var increasingOrDecreasing = Math.Sign(current - parts[1]);
	for (var i = 1; i < parts.Count; i++)
	{
		var isSafe = IsSafe(current, parts[i], increasingOrDecreasing);
		
		current = parts[i];
		
		if (!isSafe)
		{
			return false;
		}
	}
	return true;
}