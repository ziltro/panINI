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
using System.IO;
using System.Collections.Generic;

namespace panINI
{
	public class INIFile
	{
		private string FileName;
		
		public INIFile(string FileName)
		{
			this.FileName = FileName;
		}
		
		public string GetPath(string Section, string Key)
		{
			return GetPath(Section, Key, "");
		}
		
		public string GetPath(string Section, string Key, string Default)
		{
			return GetPath(Section, Key, "", System.IO.Path.DirectorySeparatorChar);
		}
		
		public string GetPath(string Section, string Key, string Default, char PathSeparator)
		{
			string Path = this.GetString(Section, Key, Default);
			string PathSep = PathSeparator.ToString();
			if (!Path.EndsWith(PathSep))
				Path += PathSep;
			
			return Path;
		}
		
		public double GetFloat(string Section, string Key)
		{
			return GetFloat(Section, Key, 0.0);
		}
		
		public double GetFloat(string Section, string Key, double Default)
		{
			try
			{
				return double.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public int GetInt(string Section, string Key)
		{
			return GetInt(Section, Key, 0);
		}
		
		public int GetInt(string Section, string Key, int Default)
		{
			try
			{
				return int.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public string GetString(string Section, string Key)
		{
			return GetString(Section, Key, "");
		}
		
		public string GetString(string Section, string Key, string Default)
		{
			StreamReader file;
			string line;
			string Value;
			bool inSection = false;

			Value = Default;
			file = File.OpenText(this.FileName);

			while ((line = file.ReadLine()) != null)
			{
				line = line.Split(';')[0];
				line = line.Trim();
				
				if (line.Length == 0)
					continue;

				if (line.StartsWith("["))
					inSection = line.StartsWith("[" + Section + "]", true, null);
		
				if (!inSection)
					continue;
				
				if (line.StartsWith(Key + "=", true, null))
				{
					Value = line.Substring(Key.Length + 1);
					break;
				}
			}
			file.Close();
			return Value;
		}
		
		public List<string> GetSectionNames()
		{
			StreamReader file;
			string line;
			List<string> Sections;
			
			Sections = new List<string>();

			file = File.OpenText(this.FileName);
			while ((line = file.ReadLine()) != null)
			{
				line = line.Split(';')[0];
				line = line.Trim();
				
				if (line.Length == 0)
					continue;

				if (line.StartsWith("[") && line.EndsWith("]"))
				{
					Sections.Add(line.Substring(1, line.Length - 2));
				}
			}
			file.Close();
			return Sections;
		}
		
		public bool GetBoolean(string Section, string Key)
		{
			return GetBoolean(Section, Key, false);
		}
		
		public bool GetBoolean(string Section, string Key, bool Default)
		{
			// Values must be lowercase.
			string[] TrueValues = new string[] {"1", "yes", "true", "oui"};
			string[] FalseValues = new string[] {"0", "no", "false", "non"};
			
			string Value = this.GetString(Section, Key, Default.ToString());

			if(Default == true)
			{
				return !(Array.IndexOf(FalseValues, Value.ToLower()) > -1);
			}
			
			if(Default == false)
			{
				return (Array.IndexOf(TrueValues, Value.ToLower()) > -1);
			}
			
			return false;
		}
	}
}

