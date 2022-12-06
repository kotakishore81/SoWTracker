using SoW.Tracker.WebAPI.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Utility.ExceptionHandling
{
    /// <summary>
    /// Class for raise validation exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SoWApiValidationException : SoWApiException
    {
        #region Private Members

        /// <summary>
        /// Field for validation messages
        /// </summary>
        [NonSerialized]
        private Collection<string> validationMessages = new Collection<string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        public SoWApiValidationException(BusinessEnums.ErrorCode errorCode)
            : base(errorCode)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        public SoWApiValidationException(BusinessEnums.ErrorCode errorCode, string message)
            : base(errorCode, message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SoWApiValidationException(BusinessEnums.ErrorCode errorCode, Exception innerException)
            : base(errorCode, SoWApiException.GetInnerExceptionMessage(innerException), innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public SoWApiValidationException(BusinessEnums.ErrorCode errorCode, string message, Exception innerException)
            : base(errorCode, message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="IDCApiValidationException" /> class.
        /// </summary>
        /// <param name="info">Serialized object data about the exception being thrown.</param>
        /// <param name="context">Contextual information about the source or destination.</param>
        protected SoWApiValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of validation issues encountered
        /// </summary>
        public ICollection<string> ValidationMessages
        {
            get { return this.validationMessages; }
        }

        #endregion
    }
}
