void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	
	var rules = new Dictionary<int, HashSet<int>>();
	var updates = new List<List<int>>();
	
	foreach (var line in lines)
	{
		if (line.Contains("|"))
		{
			var parts = line.Split("|");
			var first = Int32.Parse(parts[0]);
			var second = Int32.Parse(parts[1]);
			if (!rules.ContainsKey(first))
			{
				rules[first] = new HashSet<int>();
			}
			rules[first].Add(second);
		}
		else if (line.Contains(","))
		{
			var parts = line.Split(",");
			var update = new List<int>();
			foreach (var part in parts)
			{
				update.Add(Int32.Parse(part));
			}
			
			updates.Add(update);
		}
	}
	
	var comparator = new RuleComparator(rules);
	
	var part1 = 0;
	var part2 = 0;
	foreach(var update in updates)
	{
		var clone = new List<int>(update);
		update.Sort(comparator);
		var isValid = true;
		for (var i = 0; i < update.Count(); i++)
		{
			if (update[i] != clone[i])
			{
				isValid = false;
			}
		}
		
		if (isValid)
		{
			part1 += update[(int)Math.Ceiling((double)(update.Count /2))];
		}
		else 
		{
			part2 += update[(int)Math.Ceiling((double)(update.Count /2))];
		}
	}
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}

public class RuleComparator : Comparer<int>
{

	private Dictionary<int, HashSet<int>> _rules;
	
    public RuleComparator(Dictionary<int, HashSet<int>> rules)
	{
		_rules = rules;
	}
	
    public override int Compare(int x, int y)
    {
		if (x == y)
		{
			return 0;
		}
			
		if (_rules.ContainsKey(x))
		{
			if (_rules[x].Contains(y))
			{
				return -1;
			}
		}
		if (_rules.ContainsKey(y))
		{
			if (_rules[y].Contains(x))
			{
				return 1;
			}
		}
		
		return 0;// Don't do this: -Math.Sign(x-y);
    }
}