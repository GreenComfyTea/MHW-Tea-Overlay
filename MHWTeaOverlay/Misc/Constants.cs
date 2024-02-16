using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public enum OutlineModes { Outside, Center, Inside }

public enum FillDirections { LeftToRight, RightToLeft, TopToBottom, BottomToTop }

public static class Constants
{
	public const string MOD_NAME = "Tea Overlay";

	public const float EPSILON = 0.000001f;

	public const string DEFAULT_LOCALIZATION = "en-us";

	public const string PLUGIN_PATH = @"nativePC\plugins\CSharp\MHWTeaOverlay\";
	public const string PLUGIN_DATA_PATH = $@"{PLUGIN_PATH}data\";
	public const string LOCALIZATIONS_PATH = $@"{PLUGIN_DATA_PATH}localizations\";
	public const string CONFIGS_PATH = $@"{PLUGIN_DATA_PATH}configs\";
	public const string FONTS_PATH =  $@"{PLUGIN_DATA_PATH}fonts\";

	public const string DEFAULT_CONFIG = "default";
	public const string DEFAULT_CONFIG_WITH_EXTENSION = $"{DEFAULT_CONFIG}.json";
	public const string DEFAULT_CONFIG_FILE_PATH_NAME = $"{PLUGIN_DATA_PATH}{DEFAULT_CONFIG_WITH_EXTENSION}";

	public const string SELECTED_CONFIG = "config";
	public const string SELECTED_CONFIG_WITH_EXTENSION = $"{SELECTED_CONFIG}.json";
	public const string SELECTED_CONFIG_FILE_PATH_NAME = $"{PLUGIN_DATA_PATH}{SELECTED_CONFIG_WITH_EXTENSION}";

	public const float DRAG_FLOAT_SPEED = 0.1f;
	public const float DRAG_FLOAT_MAX = 15360f;
	public const float DRAG_FLOAT_MIN = -DRAG_FLOAT_MAX;
	public const string DRAG_FLOAT_FORMAT = "0.0";
}