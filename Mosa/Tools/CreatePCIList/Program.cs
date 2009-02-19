﻿/*
* (c) 2008 MOSA - The Managed Operating System Alliance
*
* Licensed under the terms of the New BSD License.
*
* Authors:
*  Phil Garcia (tgiphil) <phil@thinkedge.com>
*/

using System;
using System.IO;

namespace Mosa.Tools.CreatePCIList
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length < 2) {
				Console.WriteLine("ERROR: Missing arguments");
				Console.WriteLine("CreatePCIList <source> <destination>");
				return -1;
			}

			try {
				using (TextWriter textWriter = new StreamWriter(args[1], false)) {

					textWriter.WriteLine("/*");
					textWriter.WriteLine("* (c) 2008 MOSA - The Managed Operating System Alliance");
					textWriter.WriteLine("*");
					textWriter.WriteLine("* Licensed under the terms of the New BSD License.");
					textWriter.WriteLine("*");
					textWriter.WriteLine("* Authors:");
					textWriter.WriteLine("*  Phil Garcia (tgiphil) <phil@thinkedge.com>");
					textWriter.WriteLine("*/");
					textWriter.WriteLine("");
					textWriter.WriteLine("namespace Mosa.DeviceDrivers.PCI");
					textWriter.WriteLine("{");
					textWriter.WriteLine("\tpublic static class PCIList");
					textWriter.WriteLine("\t{");

					textWriter.WriteLine("\t\tpublic static string Lookup(ushort vendorID)");
					textWriter.WriteLine("\t\t{");
					textWriter.WriteLine("\t\t\tswitch (vendorID) {");

					using (TextReader textReader = new StreamReader(args[0])) {
						string line = null;
						string lastVendorID = null;

						while ((line = textReader.ReadLine()) != null) {
							string[] lines = line.Split('\t');

							if ((lines[0] == "v") && (lastVendorID != lines[1]) && (!string.IsNullOrEmpty(lines[2])))
								textWriter.WriteLine("\t\t\t\tcase 0x" + lines[1].ToUpper() + ": return \"" + lines[2] + "\";");

							lastVendorID = lines[1];

						}

						textReader.Close();
					}

					textWriter.WriteLine("\t\t\t\tdefault: return string.Empty;");
					textWriter.WriteLine("\t\t\t}");
					textWriter.WriteLine("\t\t}");
					textWriter.WriteLine("");

					textWriter.WriteLine("\t\tpublic static string Lookup(ushort vendorID, ushort deviceID)");
					textWriter.WriteLine("\t\t{");
					textWriter.WriteLine("\t\t\tswitch ((uint)(((uint)vendorID << 16) | (uint)deviceID)) {");

					using (TextReader textReader = new StreamReader(args[0])) {
						string line = null;
						string lastDeviceID = null;

						while ((line = textReader.ReadLine()) != null) {
							string[] lines = line.Split('\t');

							if ((lines[0] == "d") && (lastDeviceID != lines[1]) && (!string.IsNullOrEmpty(lines[2])))
								textWriter.WriteLine("\t\t\t\tcase 0x" + lines[1].ToUpper() + ": return \"" + lines[2].Replace('\"', '\'') + "\";");

							lastDeviceID = lines[1];

						}

						textReader.Close();
					}

					textWriter.WriteLine("\t\t\t\tdefault: return string.Empty;");
					textWriter.WriteLine("\t\t\t}");
					textWriter.WriteLine("\t\t}");

					textWriter.WriteLine("\t\tpublic static string Lookup(ushort vendorID, ushort deviceID, ushort subSystem, ushort subVendor)");
					textWriter.WriteLine("\t\t{");
					textWriter.WriteLine("\t\t\tswitch ((((ulong)vendorID << 48) | ((ulong)deviceID << 32) | ((ulong)subSystem << 16) | subVendor)) {");
					textWriter.WriteLine("#if !MONO");

					using (TextReader textReader = new StreamReader(args[0])) {
						string line = null;
						string lastDeviceID = null;

						while ((line = textReader.ReadLine()) != null) {
							string[] lines = line.Split('\t');

							if ((lines[0] == "s") && (lastDeviceID != lines[1]) && (!string.IsNullOrEmpty(lines[2])))
								textWriter.WriteLine("\t\t\t\tcase 0x" + lines[1].ToUpper() + ": return \"" + lines[2].Replace('\"', '\'') + "\";");

							lastDeviceID = lines[1];

						}

						textReader.Close();
					}

					textWriter.WriteLine("#endif");
					textWriter.WriteLine("\t\t\t\tdefault: return string.Empty;");
					textWriter.WriteLine("\t\t\t}");
					textWriter.WriteLine("\t\t}");


					textWriter.WriteLine("\t}");
					textWriter.WriteLine("}");
					textWriter.Close();
				}
			}
			catch (Exception e) {
				Console.WriteLine("Error: " + e.ToString());
				return -1;
			}

			return 0;
		}


	}
}
