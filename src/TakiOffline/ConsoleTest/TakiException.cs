using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public abstract class TakiException : Exception {
		public TakiException() { }
		public TakiException(string msg) : base(msg) { }
		public TakiException(string msg, Exception inner) : base(msg, inner) { }

		public class InvalidTakiMoveException : TakiException {
			public InvalidTakiMoveException() { }
			public InvalidTakiMoveException(string msg) : base(msg) { }
			public InvalidTakiMoveException(string msg, Exception inner) : base(msg, inner) { }
		}
	}
}
