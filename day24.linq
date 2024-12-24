void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	
	var variables = new Dictionary<string, bool>();
	var gates = new List<Gate>();
	
	foreach (var line in lines)
	{
		if (line.Contains(":"))
		{
			var parts = line.Split(": ");
			var state = parts[1] == "1" ? true : false;
			variables.Add(parts[0], state);
		}
		else if (line.Contains("->"))
		{
			var parts = line.Split(" ");
			var gate = new Gate
			{
				input1 = parts[0],
				input2 = parts[2],
				op = parts[1],
				output = parts[4]
			};
			if (gate.input2.StartsWith("x"))
			{
				gate.input2 = parts[0];
				gate.input1 = parts[2];
			}
			gates.Add(gate);
		}
	}
	
	var gatesToProcess = new List<Gate>(gates);
	while (gatesToProcess.Count() > 0)
	{
		var newGatesToProcess = new List<Gate>();
		foreach (var gate in gatesToProcess)
		{
			if (variables.ContainsKey(gate.input1) && variables.ContainsKey(gate.input2))
			{
				if (gate.op == "OR")
				{
					variables[gate.output] = variables[gate.input1] || variables[gate.input2];
				}
				else if (gate.op == "AND")
				{
					variables[gate.output] = variables[gate.input1] && variables[gate.input2];
				}
				else if (gate.op == "XOR")
				{
					variables[gate.output] = (variables[gate.input1] && !variables[gate.input2]) || (!variables[gate.input1] && variables[gate.input2]);
				}
				else 
				{
					Console.WriteLine("huh");
				}
			}
			else
			{
				newGatesToProcess.Add(gate);
			}
		}
		gatesToProcess = newGatesToProcess;
	}
	var part1AsBinary = "";
	var zvariables = variables.Where(v => v.Key.StartsWith("z")).OrderBy(kv => int.Parse(kv.Key.Substring(1)));
	
	foreach (var variable in zvariables)
	{
		part1AsBinary += variable.Value ? "1" : "0";
	}
	
	// Actually convert it to decimal :( #AdventOfReading
	double part1 = 0;
	for (var i = 0; i < part1AsBinary.Count(); i++)
	{
		if (part1AsBinary[i] == '1')
		{
			part1 += Math.Pow(2, i);
		}
	}
	Console.WriteLine(part1);
	
	var orderedGates = gates.OrderBy(g => g.input1).ThenBy(g => g.input2).ThenBy(g => g.op);
	foreach (var gate in orderedGates)
	{
		if (gate.input1.StartsWith("x") || gate.input2.StartsWith("x"))
		{
			var number = int.Parse(gate.input1.Substring(1));
			foreach (var newGate in orderedGates)
			{
				if (newGate.input1 == gate.output || newGate.input2 == gate.output)
				{
					foreach (var newGate2 in orderedGates)
					{
						if (newGate2.input1 == newGate.output || newGate2.input2 == newGate.output)
						{
							Console.WriteLine($"{gate.input1} {gate.op} {gate.input2} => {gate.output} : {newGate.input1} {newGate.op} {newGate.input2} => {newGate.output} : {newGate2.input1} {newGate2.op} {newGate2.input2} => {newGate2.output}");
						}
					}
				}
			}
		}
	}
	/*
	
	Look at the rules above and compare each set with these rules:
	
	x0 AND t0 => _ OR _ => c
	x1 AND y1 => a
	a OR b => c
	e AND d => b
	c XOR e => z
	x1 XOR y1 => d
	x2 XOR y2 => e

	Find the four descrepencies manually and then figure out what is wrong.
	Yea....
	
	*/
}

public class Gate
{
	public string input1;
	public string input2;
	public string op;
	public string output;
}