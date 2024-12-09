void Main()
{
	string input = "";
	
	var ints = input.Select(c => int.Parse(c  + "")).ToList();
	var disk = new List<int>();	
	var highestNumber = -1;
	var amounts = new Dictionary<int,int>();
	
	for (var i = 0; i < ints.Count(); i++)
	{
		if ( i % 2 == 0)
		{
			var label = (int)Math.Ceiling((double)i / 2);
			for (var j = 0; j < ints[i]; j++)
			{
				disk.Add(label);
			}
			highestNumber = label;
			amounts.Add(label, ints[i]);
		}
		else 
		{
			for (var j = 0; j < ints[i]; j++)
			{
				disk.Add(-1);
			}
		}
	}
	
	var diskClone = new List<int>(disk);
	
	while (true)
	{
		if (disk.Last() != -1)
		{
			var firstIndexOfBadSpace = disk.IndexOf(-1);
			disk[firstIndexOfBadSpace] = disk.Last();
		}
		disk.RemoveAt(disk.Count() - 1);
		if (disk.IndexOf(-1) == -1)
		{
			break;
		}
	}
	
	double checksum_part1 = 0;
	
	for (var i = 0; i < disk.Count(); i++)
	{
		checksum_part1 += i * disk[i];
	}
	Console.WriteLine(checksum_part1);
	
	disk = diskClone;
	
	for (var i = highestNumber; i >0; i--)
	{
		var amountNeeded = amounts[i];
		var currentIndex = disk.IndexOf(i);
		
		var currentRun = 0;
		
		for (var j = 0; j < disk.Count() - amountNeeded;j++)
		{
			if (disk[j] == -1)
			{
				currentRun++;
				if (currentRun == amountNeeded)
				{
					if (j < currentIndex)
					{
						for (var k = 0; k < amountNeeded; k++)
						{
							disk[j-k] = i;
							disk[currentIndex + k] = -1;
						}
					}
					j = disk.Count();
				}
			}
			else
			{
				currentRun = 0;
			}
		}
	}
	
	double checksum_part2 = 0;
	
	for (var i = 0; i < disk.Count(); i++)
	{
		if (disk[i] >= 0)
		{
			checksum_part2 += i * disk[i];
		}
	}
	Console.WriteLine(checksum_part2);
}