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

namespace panINI
{
	public class INIFile
	{
		private string FileName;
		
		public INIFile(string sFileName)
		{
			this.FileName = sFileName;
		}
		
		public string GetPath(string sSection, string sKey)
		{
			return GetPath(sSection, sKey, "");
		}
		
		public string GetPath(string sSection, string sKey, string sDefault)
		{
			return GetPath(sSection, sKey, "", System.IO.Path.DirectorySeparatorChar);
		}
		
		public string GetPath(string sSection, string sKey, string sDefault, char cPathSeparator)
		{
			string sPath = this.GetString(sSection, sKey, sDefault);
			string sChar = cPathSeparator.ToString();
			if (!sPath.EndsWith(sChar))
				sPath += sChar;
			
			return sPath;
		}
		
		public double GetFloat(string sSection, string sKey)
		{
			return GetFloat(sSection, sKey, 0.0);
		}
		
		public double GetFloat(string sSection, string sKey, double fDefault)
		{
			try
			{
				return double.Parse(this.GetString(sSection, sKey, ""));
			}
			catch
			{
				return fDefault;
			}
		}
		
		public int GetInt(string sSection, string sKey)
		{
			return GetInt(sSection, sKey, 0);
		}
		
		public int GetInt(string sSection, string sKey, int iDefault)
		{
			try
			{
				return int.Parse(this.GetString(sSection, sKey, ""));
			}
			catch
			{
				return iDefault;
			}
		}
		
		public string GetString(string sSection, string sKey)
		{
			return GetString(sSection, sKey, "");
		}
		
		public string GetString(string sSection, string sKey, string sDefault)
		{
			StreamReader file;
			string line;
			string ret;
			bool inSection = false;
			string startChar;
			
			ret = sDefault;
			file = File.OpenText(this.FileName);

			while ((line = file.ReadLine()) != null)
			{
				line = line.Trim();
				
				if (line.Length == 0)
					continue;

				startChar = line.Substring(0, 1);
				if (startChar == ";")	// Ignore comment lines
					continue;

				if (startChar == "[")
					inSection = line.StartsWith("[" + sSection + "]", true, null);
		
				if (!inSection)
					continue;
				
				if (line.StartsWith(sKey + "=", true, null))
				{
					ret = line.Substring(sKey.Length + 1);
					break;
				}
			}
			file.Close();
			return ret;
		}
	} 
}

