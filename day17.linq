void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var A = double.Parse(lines[0].Replace("Register A: ", ""));
	var B = double.Parse(lines[1].Replace("Register B: ", ""));
	var C = double.Parse(lines[2].Replace("Register C: ", ""));
	
	var opCodes = lines[4].Replace("Program: ", "").Split(",").Select(int.Parse).ToList();
	
	var outputs = Run(A,B,C, opCodes, false);
	
	Console.WriteLine(string.Join(",", outputs));
	
	for (double i = 0; i < 2200000000000; i++)
	{
		if (i % 100000000 == 0)
		{
			Console.WriteLine(i);
		}
		outputs = Run(i, B, C, opCodes, true);
		if (outputs.Count() == opCodes.Count())
		{
			var isSame = true;
			for (var j = 0; j < outputs.Count(); j++)
			{
				if (outputs[j] != opCodes[j])
				{
					isSame = false;
				}
			}
			if (isSame)
			{
				Console.WriteLine(i);
				break;
			}
		}
	}
}

public List<int> Run(double A, double B, double C, List<int> opCodes, bool part2)
{
	var instuctionPointer = 0;
	var programLength = opCodes.Count();
	var outputs = new List<int>();
	while (instuctionPointer < programLength)
	{
		if (part2)
		{
			if (outputs.Count() > opCodes.Count())
			{
				return new List<int>();
			}
			var isSame = true;
			for (var j = 0; j < outputs.Count(); j++)
			{
				if (outputs[j] != opCodes[j])
				{
					isSame = false;
				}
			}
			
			if (!isSame)
			{
				return new List<int>();
			}
		}	
		double opcode = opCodes[instuctionPointer];
		double literal = opCodes[instuctionPointer+1];
		double combo = -1;
		if (literal >=0 && literal <= 3)
		{
			combo = literal;
		} 
		else if (literal == 4)
		{
			combo = A;
		}
		else if (literal == 5)
		{
			combo = B;
		}
		else if (literal == 6)
		{
			combo = C;
		}
		instuctionPointer+=2;
		
		switch (opcode)
		{
			case 0:
				var denominator = Math.Pow(2, combo);
				A = (int)(A / denominator);
				break;
			case 1:
				B = (int)B ^ (int)literal;
				break;
			case 2:
				B = combo % 8;
				break;
			case 3:
				if (A != 0)
				{
					instuctionPointer = (int)literal * 2;
				}
				break;
			case 4:
				B = (int)B ^ (int)C;
				break;
			case 5:
				outputs.Add((int)combo % 8);
				break;
			case 6:
				denominator = Math.Pow(2, combo);
				B = (int)(A / denominator);
				break;
			case 7:
				denominator = Math.Pow(2, combo);
				C = (int)(A / denominator);
				break;
			default:
				Console.WriteLine("what?");
				break;
		}
	}
	return outputs;
}
