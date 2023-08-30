using Microsoft.AspNetCore.Http;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;

namespace MISA.WebFresher042023.Demo.Middlewares
{
    public class ExceptionMiddlewares
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        public ExceptionMiddlewares(RequestDelegate next)
        {
            _next = next;
        }

        #region Methods

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HanldeExceptionAsync(context, ex);
            }
        }

        public async Task HanldeExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            if (ex is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                var UserMsg = new List<string>()
                {
                    ex?.Message??ResourceVN.UserMsg_NotFound
                };

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrCode = ErrorCode.NotFound,
                        DevMsg = ex?.Message,
                        UserMsg = UserMsg,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = ex?.HelpLink
                    }.ToString() ?? ""
                    );
            }
            else if (ex is ValidateException validateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrCode = ErrorCode.InValidData,
                        DevMsg = ex?.Message,
                        UserMsg = validateException.UserMsg,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = ex?.HelpLink,
                        ErrorsMore = validateException.ErrorsMore
                    }.ToString() ?? ""
                    );
            }
            else if (ex is DupCodeException dupCodeException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrCode = dupCodeException?.ErrorCode ?? ErrorCode.DuplicateCode,
                        DevMsg = ex?.Message,
                        UserMsg = dupCodeException.UserMsg,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = ex?.HelpLink,
                        ErrorsMore = dupCodeException.ErrorsMore
                    }.ToString() ?? ""
                    );
            }
            else if (ex is BadRequestException badRequest)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var UserMsg = new List<string>()
                {

                };
                if (badRequest?.UserMsg?.Count > 0)
                {
                    UserMsg = badRequest.UserMsg;
                }
                else
                {
                    UserMsg = new List<string>()
                    {
                        ex?.Message??ResourceVN.UserMsg_Exception
                    };
                }
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrCode = ErrorCode.BadRequest,
                        DevMsg = ex?.Message,
                        UserMsg = UserMsg,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = ex?.HelpLink,
                        ErrorsMore = badRequest.ErrorsMore
                    }.ToString() ?? ""
                    );
            }
            else
            {
                // Lỗi server hoặc lỗi khác
                context.Response.StatusCode = StatusCodes.Status502BadGateway;
                var UserMsg = new List<string>()
                {
                    ex?.Message??ResourceVN.UserMsg_Exception
                };

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrCode = ErrorCode.InteralException,
                        DevMsg = ex?.Message,
                        UserMsg = UserMsg,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = ex?.HelpLink
                    }.ToString() ?? ""
                    );
            }

        }

        #endregion
    }
}
