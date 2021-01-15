using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMapper _mapper;

        public ExceptionMiddleware(RequestDelegate next, IMapper mapper)
        {
            _next = next;
            _mapper = mapper;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(JsonConvert.SerializeObject(_mapper.Map<ApiExceptionResponse>(ex)));
                }
                else{
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ex));
                }
            }

        }
    }
}