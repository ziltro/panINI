/*
	Copyright © 2011 Andrew Morgan <ziltro@ziltro.com>

	This program is free software: you can redistribute it and/or modify
	it under the terms of the GNU Lesser General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU Lesser General Public License for more details.

	You should have received a copy of the GNU Lesser General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using panINI;

class panINITest
{
	public static void Main (string[] args)
	{
		INIFile testini = new INIFile("test.ini");
		
		Console.WriteLine("String: " + testini.GetString("abc", "str"));
		Console.WriteLine("Int: " + testini.GetInt("abc", "int"));
		Console.WriteLine("Float: " + testini.GetFloat("abc", "float"));
		Console.WriteLine("Hello: " + testini.GetString("def", "hello"));
		Console.WriteLine("Data paths:");
		Console.WriteLine(testini.GetPath("Section1", "data1"));
		Console.WriteLine(testini.GetPath("Section1", "data2"));
		Console.WriteLine("Lines with comments:");
		Console.WriteLine(testini.GetString("Comments", "Space"));
		Console.WriteLine(testini.GetString("Comments", "Spaces"));
		Console.WriteLine(testini.GetString("Comments", "Tab"));
		Console.WriteLine(testini.GetString("Comments", "Tabs"));
		Console.WriteLine("End of file: " + testini.GetString("End", "LastLine"));
	}
}

