#
#	Copyright © 2011 Andrew Morgan <ziltro@ziltro.com>
#
#	This program is free software: you can redistribute it and/or modify
#	it under the terms of the GNU Lesser General Public License as published by
#	the Free Software Foundation, either version 3 of the License, or
#	(at your option) any later version.
#
#	This program is distributed in the hope that it will be useful,
#	but WITHOUT ANY WARRANTY; without even the implied warranty of
#	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#	GNU Lesser General Public License for more details.
#
#	You should have received a copy of the GNU Lesser General Public License
#	along with this program.  If not, see <http://www.gnu.org/licenses/>.
#

all: panini.dll

test:
	mcs -t:library -r:/usr/lib/cli/nunit.framework-2.4/nunit.framework.dll panini.cs paninitest.cs
	nunit-console panini.dll

%.dll: %.cs
	mcs -t:library $<

%.exe: %.cs
	mcs -r:panini.dll $<

clean:
	rm -f panini.dll

