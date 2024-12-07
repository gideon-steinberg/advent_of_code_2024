void Main()
{
	var input = @"";
	var lines = input.Split(Environment.NewLine);
	Solve(lines, false);
	Solve(lines, true);
}

public void Solve(string[] lines, bool part2)
{
	double sum = 0;
	foreach (var line in lines)
	{
		var parts = line.Split(": ");
		var total = double.Parse(parts[0]);
		
		var numbers = parts[1].Split(" ").Select(double.Parse).ToList();
		var results = new HashSet<double>();
		results.Add(numbers[0]);
		foreach (var number in numbers.Skip(1))
		{
			var newResults = new HashSet<double>();
			foreach (var result in results)
			{
				newResults.Add(result + number);
				newResults.Add(result * number);
				if (part2)
				{
					newResults.Add(double.Parse(result.ToString() + number.ToString()));
				}
			}
			results = newResults;
		}
		if (results.Contains(total))
		{
			sum += total;
		}
	}
	Console.WriteLine(sum);
}