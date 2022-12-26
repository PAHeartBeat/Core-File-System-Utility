// --------------------------------------------------------------------------------------
// <copyright file="File.cs" company="iPAHeartBeat">
// Copyright (c) iPAHeartBeat. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------

/*
Author:				Ankur Ranpariya {iPAHeartBeat}
EMail:				ankur30884@gmail.com
Copyright:			(c) 2017, Ankur Ranpariya {iPAHeartBeat}
Social:				@iPAHeartBeat,
GitHubL				https://www.github.com/PAHeartBeat

Original Source:	N/A
Last Modified:		Ankur Ranpariya
Contributed By:		N/A

All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the
following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.


The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
Software.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
rights to use, copy, modify, merge, publish, distribute, sub license, and/or sell copies of the Software, and to permit
persons to whom the Software is furnished to do so, subject to the following conditions:

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
*/

using iPAHeartBeat.Core.Abstraction;
using iPAHeartBeat.Core.Dependency;
using iPAHeartBeat.Core.Extensions;

namespace iPAHeartBeat.Core.IO;

/// <summary>
/// Utility Class to Read Write File in Unity with a few extra options.
/// </summary>
public static class File {
	/// <summary>
	/// Static instance of the logger to use different logger with different system or game modes.
	/// </summary>
	private static readonly IMasterLogger _log = DependencyResolver.Resolve<IMasterLogger>();

	/// <summary>
	/// Will check about the file is exists or not at particular path.
	/// </summary>
	/// <param name="path">File location where needs to be check.</param>
	/// <param name="fileName">name of the file which needs to be check.</param>
	/// <returns>true if file exists at the location other wiser false.</returns>
	public static bool FileExists(string path, string fileName) {
		var dataPath = path + fileName;
		return FileExists(dataPath);
	}

	/// <summary>
	/// Will check about the file is exists or not at particular path.
	/// </summary>
	/// <param name="fileNameWithPath">fully qualified File name with path/location where needs to be check.</param>
	/// <returns>true if file exists at the location other wiser false.</returns>
	public static bool FileExists(string fileNameWithPath)
		=> System.IO.File.Exists(fileNameWithPath);

	/// <summary>
	/// Will try to read file form provided location.
	/// </summary>
	/// <param name="path">File location where needs to be read.</param>
	/// <param name="fileName">name of the file which needs to be read.</param>
	/// <returns>string data if file exists at the location other return empty string.</returns>
	public static string ReadFile(string path, string fileName) {
		var dataPath = path + fileName;
		return ReadFile(dataPath);
	}

	/// <summary>
	/// Will try to read file form provided location.
	/// </summary>
	/// <param name="fileNameWithPath">fully qualified File name with path/location where needs to be read.</param>
	/// <returns>string data if file exists at the location other return empty string.</returns>
	public static string ReadFile(string fileNameWithPath) {
		System.IO.FileInfo theSourceFile;
		System.IO.StreamReader reader = null;
		var fileData = string.Empty;
		var dataPath = fileNameWithPath;
		try {
			theSourceFile = new System.IO.FileInfo(dataPath);
			if (theSourceFile.IsNotNull() && theSourceFile.Exists) {
				reader = theSourceFile.OpenText();
			}

			if (reader.IsNull()) {
				_ = _log?.LogDebug("FileHandling", 10, "File::ReadFile => " + dataPath + " was not found or not readable");
				fileData = null;
			} else {
				fileData = reader?.ReadToEnd();
			}
		} catch (System.Exception ex) {
			_ = _log?.LogError("FileHandling", 10, ex.Message);
		} finally {
			reader?.Close();
		}

		return fileData;
	}

	/// <summary>
	/// Will try to read file form provided location.
	/// </summary>
	/// <param name="path">File location where data needs to be write.</param>
	/// <param name="fileName">name of the file which be used to store the data.</param>
	/// <param name="data">string converted data of the file which needs to be write on disk.</param>
	/// <returns>true if file written successfully otherwise false.</returns>
	public static bool WriteFile(string path, string fileName, string data)
		=> WriteFile(path, fileName, data, FileWriteMode.CreateOverwrite);

	/// <summary>
	/// Will try to read file form provided location.
	/// </summary>
	/// <param name="path">File location where data needs to be write.</param>
	/// <param name="fileName">name of the file which be used to store the data.</param>
	/// <param name="data">string converted data of the file which needs to be write on disk.</param>
	/// <param name="mode">File write mode like it will be overwrite or appended in existing file.</param>
	/// <returns>true if file written successfully otherwise false.</returns>
	public static bool WriteFile(string path, string fileName, string data, FileWriteMode mode) {
		bool retValue;
		var dataPath = path;
		try {
			if (!System.IO.Directory.Exists(dataPath)) {
				_ = System.IO.Directory.CreateDirectory(dataPath);
			}

			dataPath += fileName;
			switch (mode) {
				case FileWriteMode.Open:
					retValue = false;
					break;

				case FileWriteMode.CreateOverwrite:
					System.IO.File.WriteAllText(dataPath, data);
					break;

				case FileWriteMode.Append:
					System.IO.File.AppendAllText(dataPath, data);
					break;
			}

			retValue = true;
		} catch (System.Exception ex) {
			_ = _log?.LogError("FileHandling", 10, "File Write Error\n" + ex.Message);
			retValue = false;
		}

		return retValue;
	}

	/// <summary>
	/// Will try to read file form provided location.
	/// </summary>
	/// <param name="path">File location where data needs to be write.</param>
	/// <param name="fileName">name of the file which be used to store the data.</param>
	/// <param name="data">array of byte as data which needs to write on disk.</param>
	/// <returns>true if file written successfully otherwise false.</returns>
	public static bool WriteFile(string path, string fileName, byte[] data) {
		bool retValue;
		var dataPath = path;
		try {
			if (!System.IO.Directory.Exists(dataPath)) {
				_ = System.IO.Directory.CreateDirectory(dataPath);
			}

			dataPath += fileName;
			System.IO.File.WriteAllBytes(dataPath, data);
			retValue = true;
		} catch (System.Exception ex) {
			_ = _log?.LogError("FileHandling", 10, "File Write Error\n" + ex.Message);
			retValue = false;
		}

		return retValue;
	}
}
