void Main()
{
	var input = @"";
	var lines = input.Split(Environment.NewLine);
	
	var height = lines.Count();
	var width = lines[0].Count();
	
	var map = new int[height,width];
	
	var zeros = new List<Point>();
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			map[i,j] = int.Parse(lines[i][j] + "");
			if (map[i,j] == 0)
			{
				zeros.Add(new Point{x=i,y=j});
			}
		}
	}
	
	var part1 = 0;
	var part2 = 0;
	
	foreach (var point in zeros)
	{
		var points = new List<Point>();
		points.Add(point);
		
		var ninesReached = new HashSet<Point>();
		
		while (points.Count() > 0)
		{
			var newPoints = new List<Point>();
			
			foreach (var current in points)
			{
				try
				{
					if(map[current.x, current.y] == 9)
					{
						part2++;
						ninesReached.Add(current);
						continue;
					}
				}
				catch{continue;}
				
				try 
				{
					if (map[current.x, current.y] == (map[current.x +1, current.y] - 1))
					{
						newPoints.Add(new Point{x = current.x +1, y= current.y});
					}
				}
				catch {}
				try
				{
					if (map[current.x, current.y] == (map[current.x -1, current.y] - 1))
					{
						newPoints.Add(new Point{x = current.x-1, y= current.y});
					}
				}
				catch {}
				try
				{
					if (map[current.x, current.y] == (map[current.x, current.y+1] - 1))
					{
						newPoints.Add(new Point{x = current.x, y= current.y+1});
					}
				}
				catch {}
				try
				{
					if (map[current.x, current.y] == (map[current.x, current.y-1] - 1))
					{
						newPoints.Add(new Point{x = current.x, y= current.y-1});
					}
				}
				catch {}
			}
			
			points = newPoints;
		}
		
		part1 += ninesReached.Count();
	}
	
	Console.WriteLine(part1);
	Console.WriteLine(part2);
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
