﻿using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Common.Models
{
	public class Result
	{
		internal Result(bool succeeded, IEnumerable<string> errors)
		{
			Succeeded = succeeded;
			Errors = errors.ToArray();
			Data = null;
		}

		internal Result(bool succeeded, IEnumerable<string> errors, object data)
		{
			Succeeded = succeeded;
			Errors = errors.ToArray();
			Data = data;
		}

		public object Data { get; set; }

		public bool Succeeded { get; set; }

		public string[] Errors { get; set; }

		public static Result Success()
		{
			return new Result(true, new string[] { });
		}

		public static Result Success(object data)
		{
			return new Result(true, new string[] { }, data);
		}

		public static Result Failure(IEnumerable<string> errors)
		{
			return new Result(false, errors);
		}
	}
}
