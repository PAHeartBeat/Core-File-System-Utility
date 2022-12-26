// --------------------------------------------------------------------------------------
// <copyright file="SearchOption.cs" company="iPAHeartBeat">
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

namespace iPAHeartBeat.Core.IO.Data;

/// <summary>
/// Search option used by File Class to search data.
/// </summary>
[Serializable]
public enum SearchOption {
	/// <summary>
	/// Compare whole value should as equal or same.
	/// </summary>
	Equal = 0,

	/// <summary>
	/// Compare whole value should not be the equal or same.
	/// </summary>
	NotEqual = 1,

	/// <summary>
	/// Compare value as text with a few starting character should be same.
	/// This uses <see cref="StringComparison.OrdinalIgnoreCase"/> to compare the data.
	/// </summary>
	StartWith = 2,

	/// <summary>
	/// Compare value as text with a few last character should be same.
	/// This uses <see cref="StringComparison.OrdinalIgnoreCase"/> to compare the data.
	/// </summary>
	EndWith = 3,

	/// <summary>
	/// Compare value as text with a few character should be part of the data field.
	/// </summary>
	Contains = 4,

	/// <summary>
	/// Compare value as text with a few character should not be part of the data field.
	/// </summary>
	NotContains = 5,

	/// <summary>
	/// Compare value as text with a few starting character should not be same.
	/// This uses <see cref="StringComparison.OrdinalIgnoreCase"/> to compare the data.
	/// </summary>
	NotStartWith = 6,

	/// <summary>
	/// Compare value as text with a few last character should not be same.
	/// This uses <see cref="StringComparison.OrdinalIgnoreCase"/> to compare the data.
	/// </summary>
	NotEndWith = 7,

	/// <summary>
	/// Compare value as Numeric data where filed value should grater/higher than provided value.
	/// This uses <see cref="double"/> to compare the data.
	/// </summary>
	Greater = 8,

	/// <summary>
	/// Compare value as Numeric data where filed value should lower/smaller than provided value.
	/// This uses <see cref="double"/> to compare the data.
	/// </summary>
	Less = 9,

	/// <summary>
	/// Compare value as Numeric data where filed value should grater/higher or equal than provided value.
	/// This uses <see cref="double"/> to compare the data.
	/// </summary>
	GreaterOrEqual = 10,

	/// <summary>
	/// Compare value as Numeric data where filed value should lower/smaller or equal than provided value.
	/// This uses <see cref="double"/> to compare the data.
	/// </summary>
	LessOrEqual = 11,
}
