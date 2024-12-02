void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	var leftNums = new List<int>();
	var rightNums = new List<int>();
	var rightCount = new Dictionary<int,int>();
	
	foreach(var line in lines)
	{
		var parts = line.Split(" ");
		var left = Int32.Parse(parts[0]);
		var right = Int32.Parse(parts[parts.Count() -1]);
		leftNums.Add(left);
		rightNums.Add(right);
		
		if (!rightCount.ContainsKey(right))
		{
			rightCount[right] = 0;
		}
		rightCount[right]++;
	}

	leftNums = leftNums.OrderBy(a => a).ToList();
	rightNums = rightNums.OrderBy(a => a).ToList();
	
	var part1 = 0;
	var part2 = 0;
	
	for(var i = 0; i < leftNums.Count(); i++)
	{
		part1 += Math.Abs(leftNums[i] - rightNums[i]);
		if (rightCount.ContainsKey(leftNums[i]))
		{
			part2 += leftNums[i] * rightCount[leftNums[i]];
		}
	}
	Console.WriteLine(part1);
	Console.WriteLine(part2);
}