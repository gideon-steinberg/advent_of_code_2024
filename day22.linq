void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	
	double part1 = 0;
	var values = new List<string>();
	var changes = new List<List<int>>();
	
	foreach (var line in lines)
	{
		var valuesForLine = "";
		var changesForLine = new List<int>();
		var secret = int.Parse(line);
		for (var i = 0; i < 2000; i++)
		{
			var current = (secret % 10);
			secret = RunPrng(secret);
			valuesForLine += (secret % 10) + "";
			changesForLine.Add(((secret % 10) - current));
		}
		part1 += secret;
		values.Add(valuesForLine);
		changes.Add(changesForLine);
	}
	Console.WriteLine(part1);
	
	double maxBannanas = -1;
	for (var a = -9; a <= 9; a++)
	{
		Console.WriteLine(a);
		for (var b = -9; b <= 9; b++)
		{
			for (var c = -9; c <= 9; c++)
			{
				for (var d = -9; d <= 9; d++)
				{
					double localMax = 0;
					for (var i = 0; i < changes.Count(); i++)
					{
						var change = changes[i];
						var index = -1;
						for (var j = 3; j < change.Count(); j++)
						{
							if (change[j-3] == a && change[j-2] == b && change[j-1] == c && change[j] == d)
							{
								index = j;
								break;
							}
						}
						if (index != -1)
						{
							localMax += double.Parse(values[i][index] + "");
						}
					}
					if (localMax != 0)
					{
						maxBannanas = Math.Max(localMax, maxBannanas);
					}
				}
			}
		}
	}
	
	Console.WriteLine(maxBannanas);
	
}

int toMod = 16777216;

public int RunPrng(int secret)
{
	var temp = secret * 64;
	secret = temp ^ secret;
	while (secret <= toMod) secret += toMod;
	secret = secret % toMod;
			
	temp = (int)Math.Floor(secret / 32.0);
	secret = temp ^ secret;
	while (secret <= toMod) secret += toMod;
	secret = secret % toMod;
	
	temp = secret * 2048;
	secret = temp ^ secret;
	while (secret <= toMod) secret += toMod;
	secret = secret % toMod;
	return secret;
}