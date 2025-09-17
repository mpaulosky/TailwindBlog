// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Services.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Shared
// =======================================================

namespace Shared;

/// <summary>
///   Contains constants for service names and configuration values.
/// </summary>
public static class Services
{


	public const string POSTGRES_SERVER = "postgres-server";
	public const string USER_DB = "userDb";

	public const string ARTICLES_DB = "articlesDb";

	public const string WEBSITE = "Web";

	public const string CACHE = "RedisCache";

	public const string OUTPUT_CACHE = "output-cache";

	public const string CATEGORY_CACHE_NAME = "CategoryData";

	public const string ARTICLE_CACHE_NAME = "ArticleData";

	public const string ADMIN_POLICY = "AdminOnly";

	public const string DEFAULT_CORS_POLICY = "DefaultPolicy";

}