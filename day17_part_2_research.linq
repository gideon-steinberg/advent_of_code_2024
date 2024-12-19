<Query Kind="Program" />

void Main()
{
	var input = @"";
	
	var lines = input.Split(Environment.NewLine);
	
	var A = double.Parse(lines[0].Replace("Register A: ", ""));
	var B = double.Parse(lines[1].Replace("Register B: ", ""));
	var C = double.Parse(lines[2].Replace("Register C: ", ""));
	
	var opCodes = lines[4].Replace("Program: ", "").Split(",").Select(int.Parse).ToList();
	
	var count = opCodes.Count();
	var inputs = new int[count];
	for (var i = 0; i < count; i++)
	{
		inputs[i] = 0;
	}
	
	double counter = 0;
	var track = -1;
	
	var current = count-1;
	while (current >= 0)
	{
		track--;
		counter++;
		if (counter %100000000 == 0)
		{
			Console.WriteLine(counter);
		}
		inputs[current]++;
		if (inputs[current] == 8)
		{
			inputs[current] = 0;
			inputs[current - 1]++;
		}
		var testCase = string.Join("", inputs.Where(i => i != 0).Select(ConvertToOctal));
		var testCaseAsNumber = (int)BinaryStringToDouble(testCase);
		
		if (track > 0)
		{
			Console.WriteLine($"{testCaseAsNumber} in binary is {testCase}");
		}
		
		var outputs = Run(testCaseAsNumber, B, C, opCodes);
		if (outputs.Count() == 0)
		{
			//continue;
		}
		var isValid = true;
		for (var i = 0; i  < outputs.Count(); i++)
		{
			if (outputs[i] != opCodes[i + (count - outputs.Count())])
			{
				isValid = false;
			}
		}
		if (isValid)
		{
			Console.WriteLine($"{testCaseAsNumber} in binary is {testCase} isValid:{isValid} outputs: {string.Join(",", outputs)}");
			current--;
			track = 100;
		}
		else if (current < count-2)
		{
			//current = count-1;		
			/*inputs[current]++;
			while (current < count-1)
			{
				current++;
				inputs[current] = 0;
			}*/
		}
	}
	
}

public string ConvertToOctal(int i)
{
	var output =  Convert.ToString(i, 2);
	while (output.Count() < 3)
	{
		output = "0" + output;
	}
	return output;
}

// Found online - can't be bothered.
public static double BinaryStringToDouble(string s)
{
  if(string.IsNullOrEmpty(s))
    throw new ArgumentNullException("s");

  double d = 0;
  foreach(var c in s)
  {
    d *= 2;
    if(c == '1')
      d += 1;
    else if(c != '0')
      throw new FormatException();
  }

  return d;
}

public List<int> Run(double A, double B, double C, List<int> opCodes)
{
	var instuctionPointer = 0;
	var programLength = opCodes.Count();
	var outputs = new List<int>();
	while (instuctionPointer < programLength)
	{
		var opcode = opCodes[instuctionPointer];
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
