void Main()
{
	var input = @"";

	var lines = input.Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines[0].Count();
	
	var points = new Dictionary<char, List<Point>>();
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (lines[i][j] != '.' && lines[i][j] != '#')
			{
				if (!points.ContainsKey(lines[i][j]))
				{
					points[lines[i][j]] = new List<Point>();
				}
				
				points[lines[i][j]].Add(new Point{x = i, y = j});
			}
		}
	}
	
	var part1 = Solve(points, height, width, false);
	Console.WriteLine(part1);
	
	var part2 = Solve(points, height, width, true);
	Console.WriteLine(part2);
}

public int Solve(Dictionary<char, List<Point>> points, int height, int width, bool part2)
{
	var antiNodes = new HashSet<Point>();
	
	foreach (var key in points.Keys)
	{
		var vals = points[key];
		for (var i = 0; i < vals.Count(); i++)
		{
			for (var j = i + 1; j < vals.Count(); j++)
			{
				var diffX = vals[i].x - vals[j].x;
				var diffY = vals[i].y - vals[j].y;
				if (part2)
				{
					for (var a = 1; a < 100; a++)
					{
						antiNodes.Add(new Point{x = vals[i].x + (diffX * a), y = vals[i].y + (diffY  * a)});
						antiNodes.Add(new Point{x = vals[j].x - (diffX * a), y = vals[j].y - (diffY * a)});
					}
				} 
				else 
				{
					antiNodes.Add(new Point{x = vals[i].x + diffX, y = vals[i].y + diffY});
					antiNodes.Add(new Point{x = vals[j].x - diffX, y = vals[j].y - diffY});
				}
			}
		}
	}
	
	if (part2)
	{
		foreach (var key in points.Keys)
		{
			if (points[key].Count > 1)
			{
				foreach (var point in points[key])
				{
					antiNodes.Add(point); 
				}
			}
		}
	}
	
	var sum = antiNodes.Where( p => p.x >= 0 && p.x < height && p.y >= 0 && p.y < width).Count();
	return sum;
}

public class Point
{
	public int x;
	public int y;
	
	public override bool Equals(Object other)
    {
		if (other is Point)
		{
			var otherPoint = (Point)other;
			return otherPoint.x == this.x && otherPoint.y == this.y;
		}
		return false;
    }
	public override int GetHashCode()
	{
		return x + 1000000 * y; // TODO: make this less terrible
	}
}