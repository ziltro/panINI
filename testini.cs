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
using System.Collections.Generic;

class panINITest
{
	public static void Main (string[] args)
	{
		IList<string> sec;
		IList<string[]> keyvals;

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
		Console.WriteLine("Spaces at end: '" + testini.GetString("Section2", "SpacesAtEnd") + "'");
		Console.WriteLine("Tabs at end: '" + testini.GetString("Section2", "TabsAtEnd") + "'");
		Console.WriteLine("Doesn't exist: " + testini.GetString("Doesnt", "Exist", "---×××---"));
		Console.WriteLine("Only key doesn't exist: " + testini.GetString("Section1", "Nope", "---×××---"));
		Console.WriteLine("End of file: " + testini.GetString("End", "LastLine"));

		Console.WriteLine("Booleans:");
		Console.WriteLine(testini.GetBoolean("Booleans", "1", true).ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "1", false).ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "1").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "2").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "3").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "4").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "5").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "6").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "7").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "8").ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "9", true).ToString());
		Console.WriteLine(testini.GetBoolean("Booleans", "9", false).ToString());

		sec = testini.GetSectionNames();
		Console.WriteLine("Sections:" + sec.Count.ToString());
		foreach(string section in sec)
		{
			Console.WriteLine("Section:" + section);
		}

		keyvals = testini.GetSection("abc");
		Console.WriteLine("Key/Value pairs:" + keyvals.Count.ToString());
		foreach(string[] keyval in keyvals)
		{
			Console.WriteLine("Key: " + keyval[0]);
			Console.WriteLine("Value: " + keyval[1]);
		}
	}
}

