using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeaOverlay;

public static class Utils
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
}
