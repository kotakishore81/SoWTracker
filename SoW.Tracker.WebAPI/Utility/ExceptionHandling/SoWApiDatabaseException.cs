using SoW.Tracker.WebAPI.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Utility.ExceptionHandling
{
    /// <summary>
    /// Class for raise database related exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SoWApiDatabaseException : SoWApiException
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        public SoWApiDatabaseException(BusinessEnums.ErrorCode errorCode)
            : base(errorCode)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        public SoWApiDatabaseException(BusinessEnums.ErrorCode errorCode, string message)
            : base(errorCode, message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SoWApiDatabaseException(BusinessEnums.ErrorCode errorCode, Exception innerException)
            : base(errorCode, SoWApiException.GetInnerExceptionMessage(innerException), innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public SoWApiDatabaseException(BusinessEnums.ErrorCode errorCode, string message, Exception innerException)
            : base(errorCode, message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="info">Serialized object data about the exception being thrown.</param>
        /// <param name="context">Contextual information about the source or destination.</param>
        protected SoWApiDatabaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
