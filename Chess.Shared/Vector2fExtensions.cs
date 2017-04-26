using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Shared {
	public static class Vector2fExtensions {
		public static Vector2f Abs(this Vector2f vector) {
			return new Vector2f(Math.Abs(vector.X), Math.Abs(vector.Y));
		}

		public static Vector2f Clone(this Vector2f vector) {
			return new Vector2f(vector.X, vector.Y);
		}

		public static bool Equals(this Vector2f vector, float value) {
			return vector.X == value && vector.Y == value;
		}

		public static bool IsLess(this Vector2f a, Vector2f b) {
			return a.X < b.X || a.Y < b.Y;
		}

		public static bool IsLess(this Vector2f vector, float value) {
			return vector.X < value || vector.Y < value;
		}

		public static bool IsMore(this Vector2f a, Vector2f b) {
			return !a.IsLess(b) && a != b;
		}

		public static bool IsMore(this Vector2f vector, float value) {
			return !vector.IsLess(value) && !vector.Equals(value);
		}

		public static Vector2f Floor(this Vector2f vector) {
			return new Vector2f((float)Math.Floor(vector.X), (float)Math.Floor(vector.Y));
		}

		public static Vector2f Round(this Vector2f vector) {
			return new Vector2f((float)Math.Round(vector.X), (float)Math.Round(vector.Y));
		}
	}
}
