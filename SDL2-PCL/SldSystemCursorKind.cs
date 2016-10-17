using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2 {
	public enum SldSystemCursorKind  {
		Arrow,		// Arrow
		IBeam,		// I-beam
		Wait,		// Wait
		Crosshair,	// Crosshair
		WaitArray,	// Small wait cursor (or Wait if not available)
		SizeNWSW,	// Double arrow pointing northwest and southeast
		SizeNESW,	// Double arrow pointing northeast and southwest
		SizeWE,		// Double arrow pointing west and east
		SizeNS,		// Double arrow pointing north and south
		SizeAll,	// Four pointed arrow pointing north, south, east, and west
		No,			// Slashed circle or crossbones
		Hand,		// Hand
	}
}
