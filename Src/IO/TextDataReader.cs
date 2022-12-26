// --------------------------------------------------------------------------------------
// <copyright file="TextDataReader.cs" company="iPAHeartBeat">
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

using System;
using System.Collections.Generic;

namespace iPAHeartBeat.Core.IO.Data;

/// <summary>
/// Helper or Utility code to read text file as tabular format using nested collection.
/// </summary>
public static class TextDataReader {
	/// <summary>
	/// Will read test file and return each line of text as data record to process later.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="path">File location where needs to be check.</param>
	/// <param name="fileName">name of the file which needs to be check.</param>
	/// <returns>List of string from the file.</returns>
	public static List<string> ReadRecordsFromFile(string path, string fileName)
		=> ReadRecordsFromFile(path, fileName, '\n');

	/// <summary>
	/// Will read test file and return each line of text as data record to process later.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="path">File location where needs to be check.</param>
	/// <param name="fileName">name of the file which needs to be check.</param>
	/// <param name="recordDelimiter">a single char which will be use to identify data part as next record.</param>
	/// <returns>List of string from the file.</returns>
	public static List<string> ReadRecordsFromFile(string path, string fileName, char recordDelimiter) {
		var tempData = File.ReadFile(path, fileName);
		return GetRecordsFromText(tempData, recordDelimiter);
	}

	/// <summary>
	/// Will read test file as data store. for a record identification it will use Unix new line character while for getting a field from record it will use comma "," as identifier.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="path">File location where needs to be check.</param>
	/// <param name="fileName">name of the file which needs to be check.</param>
	/// <returns>Nested List of string from the file as parent list will be records and nested list of string will be fields data of the record.</returns>
	public static List<List<string>> GetDataFromFile(string path, string fileName)
		=> GetDataFromFile(path, fileName, '\n', ',');

	/// <summary>
	/// Will read test file as data store. for a record identification it will use Unix new line character.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="path">File location where needs to be check.</param>
	/// <param name="fileName">name of the file which needs to be check.</param>
	/// <param name="fieldDelimiter">a single char which will be use to identify data part as next field.</param>
	/// <returns>Nested List of string from the file as parent list will be records and nested list of string will be fields data of the record.</returns>
	public static List<List<string>> GetDataFromFile(string path, string fileName, char fieldDelimiter)
		=> GetDataFromFile(path, fileName, '\n', fieldDelimiter);

	/// <summary>
	/// Will read test file as data store.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="path">File location where needs to be check.</param>
	/// <param name="fileName">name of the file which needs to be check.</param>
	/// <param name="recordDelimiter">a single char which will be use to identify data part as next record.</param>
	/// <param name="fieldDelimiter">a single char which will be use to identify data part as next field.</param>
	/// <returns>Nested List of string from the file as parent list will be records and nested list of string will be fields data of the record.</returns>
	public static List<List<string>> GetDataFromFile(string path, string fileName, char recordDelimiter, char fieldDelimiter) {
		string tempData;
		tempData = File.ReadFile(path, fileName);
		tempData = tempData.TrimEnd(recordDelimiter);

		return GetDataFromText(tempData, recordDelimiter, fieldDelimiter);
	}

	/// <summary>
	/// Will process provide raw text return each line of text as data record to process later.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="rawDataText">Raw text data to process.</param>
	/// <returns>List of string from the file.</returns>
	public static List<string> GetRecordsFromText(string rawDataText)
		=> GetRecordsFromText(rawDataText, '\n');

	/// <summary>
	/// Will process provide raw text return each line of text as data record to process later.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="rawDataText">Raw text data to process.</param>
	/// <param name="recordDelimiter">a single char which will be use to identify data part as next record.</param>
	/// <returns>List of string from the file.</returns>
	public static List<string> GetRecordsFromText(string rawDataText, char recordDelimiter) {
		string[] tempRecord;
		var retData = new List<string>();
		rawDataText = rawDataText.TrimEnd(recordDelimiter);
		tempRecord = rawDataText.Split(recordDelimiter);
		foreach (var st in tempRecord) {
			if (st.Trim().StartsWith("#", StringComparison.OrdinalIgnoreCase)) {
				continue;
			}

			retData.Add(st);
		}

		return retData;
	}

	/// <summary>
	/// Will process provide raw text and return Nested string list as data. for a record identification it will use Unix new line character while for getting a field from record it will use comma "," as identifier.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="rawDataText">Raw text data to process.</param>
	/// <returns>Nested List of string from the file as parent list will be records and nested list of string will be fields data of the record.</returns>
	public static List<List<string>> GetDataFromText(string rawDataText)
		=> GetDataFromText(rawDataText, '\n', ',');

	/// <summary>
	/// Will process provide raw text and return Nested string list as data. for a record identification it will use Unix new line character.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="dataText">Raw text data to process.</param>
	/// <param name="fieldDelimiter">a single char which will be use to identify data part as next record.</param>
	/// <returns>Nested List of string from the file as parent list will be records and nested list of string will be fields data of the record.</returns>
	public static List<List<string>> GetDataFromText(string dataText, char fieldDelimiter)
		=> GetDataFromText(dataText, '\n', fieldDelimiter);

	/// <summary>
	/// Will process provide raw text and return Nested string list as data.
	/// <br/>NOTE: Text processor will also ignore comment lines which are start with hash-sign.
	/// </summary>
	/// <param name="rawDataText">Raw text data to process.</param>
	/// <param name="recordDelimiter">a single char which will be use to identify data part as next record.</param>
	/// <param name="fieldDelimiter">a single char which will be use to identify data part as next field in the record.</param>
	/// <returns>Nested List of string from the file as parent list will be records and nested list of string will be fields data of the record.</returns>
	public static List<List<string>> GetDataFromText(string rawDataText, char recordDelimiter, char fieldDelimiter) {
		var retData = new List<List<string>>();
		List<string> recordList;
		List<string> fieldList;
		recordList = GetRecordsFromText(rawDataText, recordDelimiter);
		foreach (var st in recordList) {
			fieldList = new List<string>();
			fieldList.AddRange(st.Split(fieldDelimiter));
			retData.Add(fieldList);
		}

		return retData;
	}

	/// <summary>
	/// Helper Method to Search Records from the Nested collection of string data based on  a Single filed.
	/// <br/> NOTE: this System is un-optimized and  uses lots-of string operation, data-type check, temp data type conversation which leads to High CPU uses.
	/// </summary>
	/// <param name="data">Nested collection of the string data set.</param>
	/// <param name="searchValue">The Value which needs to search in data collection.</param>
	/// <returns>The nested collection of string data which matched with options.</returns>
	public static List<List<string>> SearchRecordFromData(List<List<string>> data, string searchValue)
		=> SearchRecordFromData(data, searchValue, 0, SearchOption.Equal);

	/// <summary>
	/// Helper Method to Search Records from the Nested collection of string data based on  a Single filed.
	/// <br/> NOTE: this System is un-optimized and  uses lots-of string operation, data-type check, temp data type conversation which leads to High CPU uses.
	/// </summary>
	/// <param name="data">Nested collection of the string data set.</param>
	/// <param name="searchValue">The Value which needs to search in data collection.</param>
	/// <param name="fieldNo">The zero-based filed number of data in which you need to check value in sub collection.</param>
	/// <returns>The nested collection of string data which matched with options.</returns>
	public static List<List<string>> SearchRecordFromData(List<List<string>> data, string searchValue, int fieldNo)
		=> SearchRecordFromData(data, searchValue, fieldNo, SearchOption.Equal);

	/// <summary>
	/// Helper Method to Search Records from the Nested collection of string data based on  a Single filed.
	/// <br/> NOTE: this System is un-optimized and  uses lots-of string operation, data-type check, temp data type conversation which leads to High CPU uses.
	/// </summary>
	/// <param name="data">Nested collection of the string data set.</param>
	/// <param name="searchValue">The Value which needs to search in data collection.</param>
	/// <param name="searchCriteria">The compare option to use with data in Field and Search Value provided.</param>
	/// <returns>The nested collection of string data which matched with options.</returns>
	public static List<List<string>> SearchRecordFromData(List<List<string>> data, string searchValue, SearchOption searchCriteria)
		=> SearchRecordFromData(data, searchValue, 0, searchCriteria);

	/// <summary>
	/// Helper Method to Search Records from the Nested collection of string data based on  a Single filed.
	/// <br/> NOTE: this System is un-optimized and  uses lots-of string operation, data-type check, temp data type conversation which leads to High CPU uses.
	/// </summary>
	/// <param name="data">Nested collection of the string data set.</param>
	/// <param name="searchValue">The Value which needs to search in data collection.</param>
	/// <param name="fieldNo">The zero-based filed number of data in which you need to check value in sub collection.</param>
	/// <param name="searchCriteria">The compare option to use with data in Field and Search Value provided.</param>
	/// <returns>The nested collection of string data which matched with options.</returns>
	public static List<List<string>> SearchRecordFromData(List<List<string>> data, string searchValue, int fieldNo, SearchOption searchCriteria) {
		var retData = new List<List<string>>();

		foreach (var listData in data) {
			if (listData.Count <= fieldNo) {
				continue;
			}

			switch (searchCriteria) {
				case SearchOption.Greater:
					if (CheckTypeForNumbers(listData[fieldNo]) && CheckTypeForNumbers(searchValue) && double.Parse(listData[fieldNo]) > double.Parse(searchValue)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.GreaterOrEqual:
					if (CheckTypeForNumbers(listData[fieldNo]) && CheckTypeForNumbers(searchValue) && double.Parse(listData[fieldNo]) >= double.Parse(searchValue)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.Less:
					if (CheckTypeForNumbers(listData[fieldNo]) && CheckTypeForNumbers(searchValue) && double.Parse(listData[fieldNo]) < double.Parse(searchValue)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.LessOrEqual:
					if (CheckTypeForNumbers(listData[fieldNo]) && CheckTypeForNumbers(searchValue) && double.Parse(listData[fieldNo]) <= double.Parse(searchValue)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.Contains:
					if (listData[fieldNo] is not null && listData[fieldNo].Contains(searchValue)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.NotContains:
					if (listData[fieldNo] is not null && !listData[fieldNo].Contains(searchValue)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.NotEndWith:
					if (listData[fieldNo] is not null && !listData[fieldNo].EndsWith(searchValue, StringComparison.OrdinalIgnoreCase)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.EndWith:
					if (listData[fieldNo] is not null && listData[fieldNo].EndsWith(searchValue, StringComparison.OrdinalIgnoreCase)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.NotStartWith:
					if (listData[fieldNo] is not null && !listData[fieldNo].StartsWith(searchValue, StringComparison.OrdinalIgnoreCase)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.StartWith:
					if (listData[fieldNo] is not null && listData[fieldNo].StartsWith(searchValue, StringComparison.OrdinalIgnoreCase)) {
						retData.Add(listData);
					}

					break;

				case SearchOption.NotEqual:
					if (listData[fieldNo] != searchValue) {
						retData.Add(listData);
					}

					break;

				case SearchOption.Equal:
					if (listData[fieldNo] == searchValue) {
						retData.Add(listData);
					}

					break;
			}
		}

		return retData;
	}

	/// <summary>
	/// Internal data type checker to use Data query option in raw Text.
	/// </summary>
	/// <param name="data">Data which need to indemnify it data type as number.</param>
	/// <returns>true if data type is any kind of number otherwise false.</returns>/
	private static bool CheckTypeForNumbers(object data) {
		var result = false;
		if (Equals(data.GetType(), typeof(byte)) ||
			Equals(data.GetType(), typeof(float)) ||
			Equals(data.GetType(), typeof(int)) ||
			Equals(data.GetType(), typeof(uint)) ||
			Equals(data.GetType(), typeof(long)) ||
			Equals(data.GetType(), typeof(ulong)) ||
			Equals(data.GetType(), typeof(double)) ||
			Equals(data.GetType(), typeof(decimal)) ||
			Equals(data.GetType(), typeof(short))) {
			result = true;
		}

		return result;
	}
}
