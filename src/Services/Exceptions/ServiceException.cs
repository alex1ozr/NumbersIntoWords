﻿namespace AErmilov.NumbersIntoWords.Services.Exceptions
{
    /// <summary>
    /// An exception related to <see cref="Services"/>>
    /// </summary>
    public abstract class ServiceException : Exception
    {
        /// <inheritdoc />
        public virtual string ErrorCode => string.Empty;

        /// <inheritdoc />
        public virtual string ShortDescription => string.Empty;

        /// <inheritdoc />
        protected ServiceException()
        {
        }

        /// <inheritdoc />
        protected ServiceException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        protected ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
