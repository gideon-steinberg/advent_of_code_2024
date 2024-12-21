void Main()
{

	var numbers = new List<int>
	{
		// computed manually
	};
	
	// Path computed manually. Must start with an aditional A
	var layer1 = @"".Split(Environment.NewLine);
	
	var map = new Dictionary<string, string>
	{
		{"<^", ">^A"},
		{"<v", ">A"},
		{"<>", ">>A"},
		{"<<", "A"},
		{"<A", ">>^A"},
		{">^", "^<A"},
		{">v", "<A"},
		{">>", "A"},
		{"><", "<<A"},
		{">A", "^A"},
		{"^^", "A"},
		{"^v", "vA"},
		{"^>", "v>A"},
		{"^<", "v<A"},
		{"^A", ">A"},
		{"v^", "^A"},
		{"vv", "A"},
		{"v>", ">A"},
		{"v<", "<A"},
		{"vA", "^>A"},
		{"A^", "<A"},
		{"Av", "v<A"},
		{"A>", "vA"},
		{"A<", "v<<A"},
		{"AA", "A"},
	};
	
	var currentLayer = layer1.ToList();
	var cache = new Dictionary<string, string>();
	for (var a = 0; a < 2; a++)
	{
		var nextLayer = new List<string>();
		for (var i = 0; i < layer1.Count(); i++)
		{
			var current = currentLayer[i];
		
			var replacement = "";
			for (var j = 1; j < current.Count(); j++)
			{
				var toReplace = current[j-1] + "" + current[j];
				replacement += map[toReplace];
			}
			nextLayer.Add(replacement);
		}
		currentLayer = nextLayer;
	}
	double sum = 0;
	for (var i = 0; i < currentLayer.Count(); i++)
	{
		var current = currentLayer[i];
		sum += (double)current.Count() * (double)numbers[i];
	}
	Console.WriteLine(sum);
}