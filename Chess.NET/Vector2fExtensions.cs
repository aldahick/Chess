using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess {
	public static class Vector2fExtensions {
		public static Vector2f Abs(this Vector2f vector) {
			return new Vector2f(Math.Abs(vector.X), Math.Abs(vector.Y));
		}

		public static Vector2f Floor(this Vector2f vector) {
			return new Vector2f((int)Math.Floor(vector.X), (int)Math.Floor(vector.Y));
		}
	}
}
