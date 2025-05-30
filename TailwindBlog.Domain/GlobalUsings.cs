// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GlobalUsings.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  TailwindBlog.Domain
// =======================================================

global using System;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Web;

global using Bogus;

global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;

global using TailwindBlog.Domain.Abstractions;
global using TailwindBlog.Domain.Entities;
global using TailwindBlog.Domain.Models;

global using FluentValidation;

global using TailwindBlog.Domain.Enums;
global using TailwindBlog.Domain.Helpers;
global using TailwindBlog.Domain.Interfaces;
global using TailwindBlog.Domain.Validators;

global using ValidationException = FluentValidation.ValidationException;