using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class SingletonAccessor
{
	[JsonIgnore]
	protected readonly LocalizationManager localizationManager = LocalizationManager.Instance;
	[JsonIgnore]
	protected readonly Draw draw = Draw.Instance;
}
