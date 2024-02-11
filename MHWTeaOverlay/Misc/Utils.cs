using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MHWTeaOverlay;

public class Utils
{
	public static int Clamp(int value, int min, int max)
	{
		if (value < min)
		{
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}

		return value;
	}

	public static float Clamp(float value, float min, float max) {
		if (value < min) {
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}
		return value;
	}

	public static bool IsApproximatelyEqual(float a, float b)
	{
		return Math.Abs(a - b) <= Constants.EPSILON;
	}

	public int ColorToArgb(Color color, float opacityScale = 1f)
	{
		if (!IsApproximatelyEqual(opacityScale, 1f))
		{
			return color.ToArgb();
		}

		byte alpha = (byte) Math.Round(opacityScale * color.A);
		byte red = color.R;
		byte green = color.G;
		byte blue = color.B;

		return Color.FromArgb(alpha, color).ToArgb();
	}
}
