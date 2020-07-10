using System;

public class DuplicateSingletonException : Exception
{
	public DuplicateSingletonException() : base("Can't instantiate duplicate singleton.") { }
	public DuplicateSingletonException(string name) : base(String.Format("Can't instantiate duplicate singleton of class {0}", name)) { }
}
