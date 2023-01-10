using System.IO;
using System.Linq;
using System.Text;

namespace ArcticFox_Demo_Histogram;

public class StoreValues : VerilogAutomation
{
    protected override Dependencies Dependencies => Dependencies.None;

	protected override void ApplyAutomation()
	{
		int amountToSend = Items[0] | 100;
		Random random = new Random();

		int min = 0;
		int max = 127;

		int[] histogram = new int[max - min + 1];

		for(int i = 0; i < amountToSend; i++)
		{
			int value = RandomNormalDistribution(50, 20, min, max);
			histogram[value - min] += 1;

			CodeAfterAutomation += @$"
StoreValue({value});"; 

		}


		double maxCounts = histogram.Max();
		StringBuilder histogramVisualization = new StringBuilder();
		bool stop = false;
		for(double i = maxCounts; !stop; i -= maxCounts/100)
		{
			for(int j = 0; j < max - min + 1; j++)
			{
				if(histogram[j] >= i)
					histogramVisualization.Append('*');
				else
					histogramVisualization.Append(' ');
			}

			stop = i < 0;

			histogramVisualization.Append(Environment.NewLine);
		}

		File.WriteAllText("HistogramVisualization.txt", histogramVisualization.ToString());
	}

	private int RandomNormalDistribution(double mean, double standardDeviation, int minCutoff, int maxCutoff)
	{
		int result = int.MinValue;
		while(result < minCutoff || result > maxCutoff)
		{
			https://stackoverflow.com/questions/218060/random-gaussian-variables
			Random rand = new Random(); //reuse this if you are generating many
			double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
			double u2 = 1.0-rand.NextDouble();
			double randomStandardNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
						Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
			result =(int)(mean + standardDeviation * randomStandardNormal);
		}

		return result;
	}
}