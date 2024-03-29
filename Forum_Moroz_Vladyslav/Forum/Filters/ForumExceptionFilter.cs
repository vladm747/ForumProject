﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Forum.Filters
{
    public class ForumExceptionFilter: Attribute, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var callStack = context.Exception.StackTrace;
            var exceptionMessage = context.Exception.Message;

            if (context.Exception is AggregateException aggregateException)
            {
                exceptionMessage = "Several exceptions might happen" +
                    string.Join(';', aggregateException.InnerExceptions.Select(e => e.Message));
            }

            context.Result = new ContentResult
            {
                Content = $"Calling {action} failed, because: {exceptionMessage}. Callstack: {callStack}.",
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
