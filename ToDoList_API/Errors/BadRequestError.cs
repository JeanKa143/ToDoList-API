﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ToDoList_API.Errors
{
    public class BadRequestError : ApiError
    {
        public BadRequestError(string message)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message)
        {
        }

        public BadRequestError(ModelStateDictionary errors, string? message = null)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), errors, message)
        {
        }

        public BadRequestError(Dictionary<string, IEnumerable<string>> errors, string? message = null)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), errors, message)
        {
        }
    }
}