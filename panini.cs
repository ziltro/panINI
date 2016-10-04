/*
	Copyright © 2011, 2012, 2016 Andrew Morgan <ziltro@ziltro.com>

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
using System.Text;
using System.Collections.Generic;

namespace panINI
{
	public class INIFile
	{
		private string FileName;
		private Encoding FileEncoding;

		public INIFile(string FileName, Encoding FileEncoding)
		{
			this.FileName = FileName;
			this.FileEncoding = FileEncoding;
		}

		public INIFile(string FileName)
		{
			this.FileName = FileName;
			this.FileEncoding = Encoding.UTF8;
		}
		
		public string GetPath(string Section, string Key)
		{
			return GetPath(Section, Key, "");
		}
		
		public string GetPath(string Section, string Key, string Default)
		{
			return GetPath(Section, Key, Default, System.IO.Path.DirectorySeparatorChar);
		}
		
		public string GetPath(string Section, string Key, string Default, char PathSeparator)
		{
			string Path = this.GetString(Section, Key, Default);
			string PathSep = PathSeparator.ToString();
			if (!Path.EndsWith(PathSep))
				Path += PathSep;
			
			return Path;
		}
		
		public System.Single GetSingle(string Section, string Key)
		{
			return GetSingle(Section, Key, 0.0F);
		}
		
		public System.Single GetSingle(string Section, string Key, System.Single Default)
		{
			try
			{
				return System.Single.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Double GetDouble(string Section, string Key)
		{
			return GetDouble(Section, Key, 0.0);
		}
		
		public System.Double GetDouble(string Section, string Key, System.Double Default)
		{
			try
			{
				return System.Double.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Byte GetByte(string Section, string Key)
		{
			return GetByte(Section, Key, 0);
		}
		
		public System.Byte GetByte(string Section, string Key, System.Byte Default)
		{
			try
			{
				return System.Byte.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Char GetChar(string Section, string Key)
		{
			return GetChar(Section, Key, '\0');
		}
		
		public System.Char GetChar(string Section, string Key, System.Char Default)
		{
			try
			{
				return System.Char.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Int16 GetInt16(string Section, string Key)
		{
			return GetInt16(Section, Key, 0);
		}
		
		public System.Int16 GetInt16(string Section, string Key, System.Int16 Default)
		{
			try
			{
				return System.Int16.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Int32 GetInt32(string Section, string Key)
		{
			return GetInt32(Section, Key, 0);
		}
		
		public System.Int32 GetInt32(string Section, string Key, System.Int32 Default)
		{
			try
			{
				return System.Int32.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Int64 GetInt64(string Section, string Key)
		{
			return GetInt64(Section, Key, 0);
		}
		
		public System.Int64 GetInt64(string Section, string Key, System.Int64 Default)
		{
			try
			{
				return System.Int64.Parse(this.GetString(Section, Key, ""));
			}
			catch
			{
				return Default;
			}
		}
		
		public System.Decimal GetDecimal(string Section, string Key)
		{
			return GetDecimal(Section, Key, 0);
		}
		
		public System.Decimal GetDecimal(string Section, string Key, System.Decimal Default)
		{
			try
			{
				return System.Decimal.Parse(this.GetString(Section, Key, ""));
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
			string line;
			string Value;
			bool inSection = false;

			Value = Default;

			var file = new StreamReader(this.FileName, this.FileEncoding);

			while ((line = file.ReadLine()) != null)
			{
				line = line.TrimStart();
				
				if (line.Length == 0)
					continue;

				if (line.StartsWith(";"))
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
		
		public IList<string> GetSectionNames()
		{
			string line;
			List<string> Sections;
			
			Sections = new List<string>();

			var file = new StreamReader(this.FileName, this.FileEncoding);

			while ((line = file.ReadLine()) != null)
			{
				line = line.TrimStart();
				
				if (line.Length == 0)
					continue;

				if (line.StartsWith(";"))
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
			string[] TrueValues = new string[] {"1", "true", "yes", "oui"};
			string[] FalseValues = new string[] {"0", "false", "no", "non"};
			
			string Value = this.GetString(Section, Key, Default.ToString());

			if(Default)
			{
				return Array.IndexOf(FalseValues, Value.ToLower()) == -1;
			}

			return (Array.IndexOf(TrueValues, Value.ToLower()) >= 0);
		}
		
		public IList<string[]> GetSection(string Section)
		{
			string line;
			List<string[]> SectionsValues;
			string[] Value;
			bool inSection = false;
			int EqualsLocation;

			var file = new StreamReader(this.FileName, this.FileEncoding);
			SectionsValues = new List<string[]>();

			while ((line = file.ReadLine()) != null)
			{
				line = line.TrimStart();
				
				if (line.Length == 0)
					continue;

				if (line.StartsWith(";"))
					continue;

				if (line.StartsWith("["))
				{
					if (inSection)
						break;
						
					inSection = line.StartsWith("[" + Section + "]", true, null);
				}
		
				if (!inSection)
					continue;
				
				EqualsLocation = line.IndexOf("=");
				if (EqualsLocation > -1)
				{
					Value = new string[2];
					Value[0] = line.Substring(0, EqualsLocation);
					Value[1] = line.Substring(EqualsLocation + 1);
					SectionsValues.Add(Value);
				}
			}
			file.Close();
			return SectionsValues;
		}

		public void SetString(string Section, string Key, string Value)
		{
			string[] contents;
			StringBuilder outputINI = new StringBuilder();
			bool inSection = false;
			bool alreadyWritten = false;
			string line;

			contents = File.ReadAllLines(this.FileName, this.FileEncoding);

			foreach(string l in contents)
			{
				line = l.TrimStart();

				if (line.Length == 0)
				{
					outputINI.AppendLine(l);
					continue;
				}

				if (line.StartsWith(";"))
				{
					outputINI.AppendLine(l);
					continue;
				}

				if (line.StartsWith("["))
				{
					if (inSection && !alreadyWritten)
					{
						outputINI.Append(Key);
						outputINI.Append("=");
						outputINI.AppendLine(Value);
						alreadyWritten = true;
					}
					inSection = line.StartsWith("[" + Section + "]", true, null);
				}

				if (!inSection)
				{
					outputINI.AppendLine(l);
					continue;
				}

				if (line.StartsWith(Key + "=", true, null))
				{
					outputINI.Append(Key);
					outputINI.Append("=");
					outputINI.AppendLine(Value);
					alreadyWritten = true;
				}
				else
				{
					outputINI.AppendLine(l);
				}
			}

			if (!alreadyWritten)
			{
				outputINI.Append("[");
				outputINI.Append(Section);
				outputINI.AppendLine("]");
				outputINI.Append(Key);
				outputINI.Append("=");
				outputINI.AppendLine(Value);
				alreadyWritten = true;
			}

			File.WriteAllText(this.FileName, outputINI.ToString(), this.FileEncoding);
		}

		public void SetBoolean(string Section, string Key, bool Value)
		{
			if (Value)
			{
				this.SetString(Section, Key, "true");
			}
			else
			{
				this.SetString(Section, Key, "false");
			}
		}
	}
}

