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
			return new Vector2f((float)Math.Floor(vector.X), (float)Math.Floor(vector.Y));
		}

		public static Vector2f Round(this Vector2f vector) {
			return new Vector2f((float)Math.Round(vector.X), (float)Math.Round(vector.Y));
		}
	}
}
