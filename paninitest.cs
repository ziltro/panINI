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

namespace panINI
{
	using NUnit.Framework;
	using System.Collections.Generic;
	
	[TestFixture]
	public class panINITest
	{
		[Test]
		public void Read()
		{
			INIFile ini = new panINI.INIFile("test.ini");
			Assert.AreEqual(123, ini.GetInt16("abc", "int"));
			Assert.AreEqual(123, ini.GetInt32("abc", "int"));
			Assert.AreEqual(123, ini.GetInt64("abc", "int"));
			Assert.AreEqual(123.456f, ini.GetSingle("abc", "float"));
			Assert.AreEqual(123.456d, ini.GetDouble("abc", "float"));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "1", true));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "1", false));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "1"));
			Assert.AreEqual(false, ini.GetBoolean("Booleans", "2"));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "3"));
			Assert.AreEqual(false, ini.GetBoolean("Booleans", "4"));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "5"));
			Assert.AreEqual(false, ini.GetBoolean("Booleans", "6"));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "7"));
			Assert.AreEqual(false, ini.GetBoolean("Booleans", "8"));
			Assert.AreEqual(false, ini.GetBoolean("Booleans", "9"));
			Assert.AreEqual(true, ini.GetBoolean("Booleans", "9", true));
			Assert.AreEqual(false, ini.GetBoolean("Booleans", "9", false));
			Assert.AreEqual("Hello World", ini.GetString("abc", "str"));
			Assert.AreEqual("world", ini.GetString("def", "Hello"));
			Assert.AreEqual(ini.GetString("def", "Hello"), ini.GetString("def", "hello"));
			Assert.AreEqual("abc", ini.GetString("Comments", "Space"));
			Assert.AreEqual("abc", ini.GetString("Comments", "Spaces"));
			Assert.AreEqual("abc", ini.GetString("Comments", "Tab"));
			Assert.AreEqual("abc", ini.GetString("Comments", "Tabs"));
			Assert.AreEqual("abc", ini.GetString("Section2", "SpacesAtEnd"));
			Assert.AreEqual("def", ini.GetString("Section2", "TabsAtEnd"));
			Assert.AreEqual("---×××---", ini.GetString("Doesnt", "Exist", "---×××---"));
			Assert.AreEqual("---×××---", ini.GetString("Section1", "Nope", "---×××---"));
		}
		[Test]
		public void ArrayTests()
		{
			IList<string> sec;
			IList<string[]> keyvals;
			INIFile ini = new panINI.INIFile("test.ini");

			sec = ini.GetSectionNames();
			keyvals = ini.GetSection("abc");

			Assert.AreEqual(7, sec.Count);
			Assert.AreEqual(3, keyvals.Count);
		}
	}
}

