using DevEdu.Core.Common;
using DevEdu.Core.Exceptions;
using System;

namespace DevEdu.Tests.Data
{
    public abstract class BaseData
    {
        private const string _messageAuthorization = "Authorization exception";
        private const string _messageValidation = "Validation exception";
        private const string _messageEntity = "Entity not found exception";
        private const int _authorizationCode = 2000;
        private const int _validationCode = 1001;
        private const int _entityCode = 400;

        protected const string _dateFormat = "MM/dd/yyyy hh:mm:ss.fff";
        protected const string _validDateFormat = "dd/MM/yyyy";
        protected static Random _random = new Random();

        public static ExceptionResponse GetEntityNotFoundExceptionResponse(string entity, int id)
        {
            return new ExceptionResponse
            {
                Code = _entityCode,
                Message = _messageEntity,
                Description = (string.Format(ServiceMessages.EntityNotFoundMessage, entity, id))
            };
        }

        public static ExceptionResponse GetAuthorizationExceptionResponce(string description)
        {
            return new ExceptionResponse
            {
                Code = _authorizationCode,
                Message = _messageAuthorization,
                Description = description
            };
        }
    }
}