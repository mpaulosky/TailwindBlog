﻿// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     DatabaseSettings.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : TailwindBlog
// Project Name :  Domain
// =============================================

using Domain.Interfaces;

namespace Domain.Models;

/// <summary>
///   DatabaseSettings class
/// </summary>
public class DatabaseSettings : IDatabaseSettings
{

	public DatabaseSettings() { }

	public DatabaseSettings(string connectionStrings, string databaseName)
	{

		ConnectionStrings = connectionStrings;
		DatabaseName = databaseName;

	}

	public string ConnectionStrings { get; set; } = null!;

	public string DatabaseName { get; set; } = null!;

}