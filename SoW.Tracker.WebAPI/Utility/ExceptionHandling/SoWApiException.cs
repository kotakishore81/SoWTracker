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
    /// Class for raise API related exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SoWApiException : Exception
    {
        public BusinessEnums.ErrorCode ErrorCode { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiException" /> class.
        /// </summary>
        public SoWApiException(BusinessEnums.ErrorCode errorCode)
            : base()
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        public SoWApiException(BusinessEnums.ErrorCode errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiException" /> class.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SoWApiException(BusinessEnums.ErrorCode errorCode, Exception innerException)
            : base(GetInnerExceptionMessage(innerException), innerException)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public SoWApiException(BusinessEnums.ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiException" /> class.
        /// </summary>
        /// <param name="info">Serialized object data about the exception being thrown.</param>
        /// <param name="context">Contextual information about the source or destination.</param>
        protected SoWApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Verify inner exception is not null before retrieving message (used before passing to base constructor)
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <returns>The inner exceptions message</returns>
        protected static string GetInnerExceptionMessage(Exception innerException)
        {
            if (innerException == null)
            {
                throw new ArgumentNullException("innerException");
            }

            return innerException.Message;
        }

        #endregion
    }
}
