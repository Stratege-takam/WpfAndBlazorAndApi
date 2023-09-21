namespace Elia.Core.Utils;
using Newtonsoft.Json;


    /// <summary>
    /// A generic result of a method call.
    /// </summary>
    public class BaseResult
    {
        #region Static (Shortcuts)

        /// <summary>
        /// Success.
        /// </summary>
        public static BaseResult Success => new BaseResult();

        /// <summary>
        /// Success with warnings.
        /// </summary>
        public static BaseResult SuccessWithWarnings => new BaseResult(BaseResultStatus.SuccessWithWarnings);

        /// <summary>
        /// Failure.
        /// </summary>
        public static BaseResult Failure => new BaseResult(BaseResultStatus.Failure);

        /// <summary>
        /// An unexpected failure has occured.
        /// </summary>
        public static BaseResult Unexpected => new BaseResult(BaseResultStatus.Unexpected);

        /// <summary>
        /// Unauthorized.
        /// </summary>
        public static BaseResult Unauthorized => new BaseResult(BaseResultStatus.Unauthorized);

        /// <summary>
        /// Already.
        /// </summary>
        public static BaseResult Already => new BaseResult(BaseResultStatus.Already);

        /// <summary>
        /// Not found.
        /// </summary>
        public static BaseResult NotFound => new BaseResult(BaseResultStatus.NotFound);

        /// <summary>
        /// Bad parameters are given.
        /// </summary>
        public static BaseResult BadParameters => new BaseResult(BaseResultStatus.BadParams);

        /// <summary>
        /// The method call was canceled.
        /// </summary>
        public static BaseResult Cancelled => new BaseResult(BaseResultStatus.Cancel);

        /// <summary>
        /// The method call has timed out.
        /// </summary>
        public static BaseResult Timeout => new BaseResult(BaseResultStatus.Timeout);

        /// <summary>
        /// The operation has no connection to execute.
        /// </summary>
        public static BaseResult NoConnection => new BaseResult(BaseResultStatus.NoConnection);

        /// <summary>
        /// The operation is not implemented
        /// </summary>
        public static BaseResult NotImplemented => new BaseResult(BaseResultStatus.NotImplement);

        #endregion Static (Shortcuts)

        #region Constructors

        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="status">The result status of the method call.</param>
        public BaseResult(BaseResultStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="exception">The exception if unexpected error.</param>
        public BaseResult(Exception exception)
        {
            Status = BaseResultStatus.Unexpected;
            Exception = exception;
        }

        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="result">The another generic result to take the status/exception.</param>
        public BaseResult(BaseResult result)
        {
            Status = result.Status;
            Reason = result.Reason;
            Exception = result.Exception;
        }

        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="status">The result status of the method call.</param>
        /// <param name="exception">The exception if unexpected error.</param>
        [JsonConstructor]
        public BaseResult(BaseResultStatus status = BaseResultStatus.Success, Exception exception = null)
        {
            Status = status;
            Exception = exception;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The result status of the method call.
        /// </summary>
        public BaseResultStatus Status { get; protected set; }

        /// <summary>
        /// The reason in text why a not succeeded result has occured.
        /// </summary>
        public string Reason { get; protected set; }

        /// <summary>
        /// The exception if unexpected error.
        /// </summary>
        public Exception Exception { get; protected set; }

        #endregion Properties

        #region Properties (Computed)

        /// <summary>
        /// Whether the status is a success.
        /// </summary>
        [JsonIgnore]
        public bool IsSuccess => Status == BaseResultStatus.Success;

        /// <summary>
        /// Whether the status is not a success.
        /// </summary>
        [JsonIgnore]
        public bool IsNotSuccess => Status != BaseResultStatus.Success;

        /// <summary>
        /// Whether the status is a success with warnings.
        /// </summary>
        [JsonIgnore]
        public bool IsSuccessWithWarnings => Status == BaseResultStatus.SuccessWithWarnings;

        /// <summary>
        /// Whether the status is a failure.
        /// </summary>
        [JsonIgnore]
        public bool IsFailure => Status == BaseResultStatus.Failure;

        /// <summary>
        /// Whether the status is an unexpected error.
        /// </summary>
        [JsonIgnore]
        public bool IsUnexpected => Status == BaseResultStatus.Unexpected;

        /// <summary>
        /// Whether the status is an unauthorized call.
        /// </summary>
        [JsonIgnore]
        public bool IsUnauthorized => Status == BaseResultStatus.Unauthorized;

        /// <summary>
        /// Whether the call was already done.
        /// </summary>
        [JsonIgnore]
        public bool IsAlready => Status == BaseResultStatus.Already;

        /// <summary>
        /// Whether the call was not already done.
        /// </summary>
        [JsonIgnore]
        public bool IsNotAlready => Status != BaseResultStatus.Already;

        /// <summary>
        /// Whether the call logic was not found.
        /// </summary>
        [JsonIgnore]
        public bool IsNotFound => Status == BaseResultStatus.NotFound;


        /// <summary>
        /// Whether the call has bad parameters.
        /// </summary>
        [JsonIgnore]
        public bool IsBadParameters => Status == BaseResultStatus.BadParams;

        /// <summary>
        /// Whether the status is a user cancellation.
        /// </summary>
        [JsonIgnore]
        public bool IsCancelled => Status == BaseResultStatus.Cancel;

        /// <summary>
        /// Whether the status is a time out.
        /// </summary>
        [JsonIgnore]
        public bool IsTimeout => Status == BaseResultStatus.Timeout;

        /// <summary>
        /// Whether the operation has no connection to execute.
        /// </summary>
        [JsonIgnore]
        public bool IsNoConnection => Status == BaseResultStatus.NoConnection;

        /// <summary>
        /// Whether the operation is not implemeted.
        /// </summary>
        [JsonIgnore]
        public bool IsNotImplemented => Status == BaseResultStatus.NotImplement;

        #endregion Properties (Computed)


        #region Methods (Public Helpers)

        /// <summary>
        /// Sets the status of the result.
        /// </summary>
        /// <param name="status">The status to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public BaseResult WithStatus(BaseResultStatus status)
        {
            Status = status;
            return this;
        }

        /// <summary>
        /// Sets the reason of the result.
        /// </summary>
        /// <param name="reason">The reason to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public BaseResult WithReason(string reason)
        {
            Reason = reason;
            return this;
        }

        /// <summary>
        /// Sets the exception of the result.
        /// </summary>
        /// <param name="exception">The exception to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public BaseResult WithException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        /// <summary>
        /// When the result is a success then continues with another action that returns a success in order to chain a bunch of actions.
        /// </summary>
        /// <param name="resultFunctionToContinueWith">A function that returns a result to continue with</param>
        /// <returns>This result if not success or the result of the function to continue with if success.</returns>
        public BaseResult WhenSuccessContinueWith(Func<BaseResult, BaseResult> resultFunctionToContinueWith)
        {
            if (IsNotSuccess)
                return this;

            return resultFunctionToContinueWith(this);
        }

        /// <summary>
        /// When the result is a success then continues with another action that returns a success in order to chain a bunch of actions.
        /// </summary>
        /// <param name="resultFunctionToContinueWith">A function that returns a result to continue with</param>
        /// <returns>This result if not success or the result of the function to continue with if success.</returns>
        public async Task<BaseResult> WhenSuccessContinueWith(Func<BaseResult, Task<BaseResult>> resultFunctionToContinueWith)
        {
            if (IsNotSuccess)
                return this;

            return await resultFunctionToContinueWith(this);
        }

        #endregion Methods (Public Helpers)

        #region Methods (Override)

        /// <summary>
        /// String representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return Status.ToString();
        }

        #endregion Methods (Override)
    }

    /// <inheritdoc />
    /// <summary>
    /// A generic result of a method call with data.
    /// </summary>
    public class BaseResult<T> : BaseResult
    {
        #region Static (Shortcuts)

        /// <summary>
        /// Success.
        /// </summary>
        public new static BaseResult<T> Success => new BaseResult<T>();

        /// <summary>
        /// Success with warnings.
        /// </summary>
        public new static BaseResult<T> SuccessWithWarnings => new BaseResult<T>(BaseResultStatus.SuccessWithWarnings);

        /// <summary>
        /// Failure.
        /// </summary>
        public new static BaseResult<T> Failure => new BaseResult<T>(BaseResultStatus.Failure);

        /// <summary>
        /// An unexpected failure has occured.
        /// </summary>
        public new static BaseResult<T> Unexpected => new BaseResult<T>(BaseResultStatus.Unexpected);

        /// <summary>
        /// Unauthorized.
        /// </summary>
        public new static BaseResult<T> Unauthorized => new BaseResult<T>(BaseResultStatus.Unauthorized);

        /// <summary>
        /// Already.
        /// </summary>
        public new static BaseResult<T> Already => new BaseResult<T>(BaseResultStatus.Already);

        /// <summary>
        /// Not found.
        /// </summary>
        public new static BaseResult<T> NotFound => new BaseResult<T>(BaseResultStatus.NotFound);

       
        /// <summary>
        /// Bad parameters are given.
        /// </summary>
        public new static BaseResult<T> BadParameters => new BaseResult<T>(BaseResultStatus.BadParams);

        /// <summary>
        /// The method call was canceled.
        /// </summary>
        public new static BaseResult<T> Cancelled => new BaseResult<T>(BaseResultStatus.Cancel);

        /// <summary>
        /// The method call has timed out.
        /// </summary>
        public new static BaseResult<T> Timeout => new BaseResult<T>(BaseResultStatus.Timeout);

        /// <summary>
        /// The operation has no connection to execute.
        /// </summary>
        public new static BaseResult<T> NoConnection => new BaseResult<T>(BaseResultStatus.NoConnection);

        /// <summary>
        /// The operation is not implemented
        /// </summary>
        public new static BaseResult<T> NotImplemented => new BaseResult<T>(BaseResultStatus.NotImplement);

        #endregion Static (Shortcuts)

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Creates a Result instance.
        /// Simply checks if there is some data to flag as success or failure.
        /// </summary>
        /// <param name="data">The data returned from the method call if any.</param>
        public BaseResult(T data)
            : base(BaseResultStatus.Success)
        {
            Data = data;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="exception">The exception if unexpected error.</param>
        public BaseResult(Exception exception)
            : base(exception)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="result">The another generic result to take the status/exception.</param>
        public BaseResult(BaseResult result)
            : base(result)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="status">The result status of the method call.</param>
        /// <param name="exception">The exception if unexpected error.</param>
        [JsonConstructor]
        public BaseResult(BaseResultStatus status, Exception exception)
            : base(status, exception)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="status">The result status of the method call.</param>
        /// <param name="data">The data returned from the method call if any.</param>
        /// <param name="exception">The exception if unexpected error.</param>
        [JsonConstructor]
        public BaseResult(BaseResultStatus status = BaseResultStatus.Success, T data = default, Exception exception = null)
            : base(status, exception)
        {
            Data = data;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">The data returned from the method call if any.</param>
        [JsonConstructor]
        public BaseResult( BaseResult result, T data = default) : base(result)
        {
            Data = data;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The data returned from the method call if any.
        /// </summary>
        public T Data { get; protected set; }

        #endregion Properties

        #region Properties (Computed)

        /// <summary>
        /// Whether the result has some data.
        /// </summary>
        [JsonIgnore]
        public bool HasData => Data != null;

        #endregion Properties (Computed)

        #region Methods (Public Helpers)

        /// <summary>
        /// Sets the status of the result.
        /// </summary>
        /// <param name="status">The status to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public new BaseResult<T> WithStatus(BaseResultStatus status)
        {
            Status = status;
            return this;
        }

        /// <summary>
        /// Sets the reason of the result.
        /// </summary>
        /// <param name="reason">The reason to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public new BaseResult<T> WithReason(string reason)
        {
            Reason = reason;
            return this;
        }

        /// <summary>
        /// Sets the exception of the result.
        /// </summary>
        /// <param name="exception">The exception to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public new BaseResult<T> WithException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        /// <summary>
        /// Sets the data of the result.
        /// </summary>
        /// <param name="data">The data to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public BaseResult<T> WithData(T data)
        {
            Data = data;
            return this;
        }

        /// <summary>
        /// When the result is a success then continues with another action that returns a success in order to chain a bunch of actions.
        /// </summary>
        /// <param name="resultFunctionToContinueWith">A function that returns a result to continue with</param>
        /// <returns>This result if not success or the result of the function to continue with if success.</returns>
        public BaseResult WhenSuccessContinueWith(Func<BaseResult<T>, BaseResult> resultFunctionToContinueWith)
        {
            if (IsNotSuccess)
                return this;

            return resultFunctionToContinueWith(this);
        }

        #endregion Methods (Public Helpers)

        #region Methods (Override)

        /// <inheritdoc />
        /// <summary>
        /// String representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return $"{Status} - {Data?.GetType().Name}:{Data}";
        }

        #endregion Methods (Override)
    }

    /// <inheritdoc />
    /// <summary>
    /// A generic result of a method call with 2 distinct data.
    /// </summary>
    public class BaseResult<T1, T2> : BaseResult
    {
        #region Static (Shortcuts)

        /// <summary>
        /// Success.
        /// </summary>
        public new static BaseResult<T1, T2> Success => new BaseResult<T1, T2>();

        /// <summary>
        /// Success with warnings.
        /// </summary>
        public new static BaseResult<T1, T2> SuccessWithWarnings => new BaseResult<T1, T2>(BaseResultStatus.SuccessWithWarnings);

        /// <summary>
        /// Failure.
        /// </summary>
        public new static BaseResult<T1, T2> Failure => new BaseResult<T1, T2>(BaseResultStatus.Failure);

        /// <summary>
        /// An unexpected failure has occured.
        /// </summary>
        public new static BaseResult<T1, T2> Unexpected => new BaseResult<T1, T2>(BaseResultStatus.Unexpected);

        /// <summary>
        /// Unauthorized.
        /// </summary>
        public new static BaseResult<T1, T2> Unauthorized => new BaseResult<T1, T2>(BaseResultStatus.Unauthorized);

        /// <summary>
        /// Already.
        /// </summary>
        public new static BaseResult<T1, T2> Already => new BaseResult<T1, T2>(BaseResultStatus.Already);

        /// <summary>
        /// Not found.
        /// </summary>
        public new static BaseResult<T1, T2> NotFound => new BaseResult<T1, T2>(BaseResultStatus.NotFound);
        
        /// <summary>
        /// Bad parameters are given.
        /// </summary>
        public new static BaseResult<T1, T2> BadParameters => new BaseResult<T1, T2>(BaseResultStatus.BadParams);

        /// <summary>
        /// The method call was canceled.
        /// </summary>
        public new static BaseResult<T1, T2> Cancelled => new BaseResult<T1, T2>(BaseResultStatus.Cancel);

        /// <summary>
        /// The method call has timed out.
        /// </summary>
        public new static BaseResult<T1, T2> Timeout => new BaseResult<T1, T2>(BaseResultStatus.Timeout);

        /// <summary>
        /// The operation has no connection to execute.
        /// </summary>
        public new static BaseResult<T1, T2> NoConnection => new BaseResult<T1, T2>(BaseResultStatus.NoConnection);

        /// <summary>
        /// The operation is not implemented
        /// </summary>
        public new static BaseResult<T1, T2> NotImplemented => new BaseResult<T1, T2>(BaseResultStatus.NotImplement);

        #endregion Static (Shortcuts)

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Creates a Result instance.
        /// Simply checks if there is some data to flag as success or failure.
        /// </summary>
        /// <param name="data1">The first data returned from the method call if any.</param>
        /// <param name="data2">The second data returned from the method call if any.</param>
        public BaseResult(T1 data1, T2 data2)
            : base(BaseResultStatus.Success)
        {
            Data1 = data1;
            Data2 = data2;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="exception">The exception if unexpected error.</param>
        public BaseResult(Exception exception)
            : base(exception)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="result">The another generic result to take the status/exception.</param>
        public BaseResult(BaseResult result)
            : base(result)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="BaseResult"/> instance.
        /// </summary>
        /// <param name="status">The result status of the method call.</param>
        /// <param name="data1">The first data returned from the method call if any.</param>
        /// <param name="data2">The second data returned from the method call if any.</param>
        /// <param name="exception">The exception if unexpected error.</param>
        [JsonConstructor]
        public BaseResult(BaseResultStatus status = BaseResultStatus.Success, T1 data1 = default, T2 data2 = default, Exception exception = null)
            : base(status, exception)
        {
            Data1 = data1;
            Data2 = data2;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The first data returned from the method call if any.
        /// </summary>
        public T1 Data1 { get; protected set; }

        /// <summary>
        /// The second data returned from the method call if any.
        /// </summary>
        public T2 Data2 { get; protected set; }

        #endregion Properties

        #region Properties (Computed)

        /// <summary>
        /// Whether the result has a first data.
        /// </summary>
        [JsonIgnore]
        public bool HasData1 => Data1 != null;

        /// <summary>
        /// Whether the result has a second data.
        /// </summary>
        [JsonIgnore]
        public bool HasData2 => Data2 != null;

        #endregion Properties (Computed)

        #region Methods (Public Helpers)

        /// <summary>
        /// Sets the status of the result.
        /// </summary>
        /// <param name="status">The status to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public new BaseResult<T1, T2> WithStatus(BaseResultStatus status)
        {
            Status = status;
            return this;
        }

        /// <summary>
        /// Sets the reason of the result.
        /// </summary>
        /// <param name="reason">The reason to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public new BaseResult<T1, T2> WithReason(string reason)
        {
            Reason = reason;
            return this;
        }

        /// <summary>
        /// Sets the exception of the result.
        /// </summary>
        /// <param name="exception">The exception to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public new BaseResult<T1, T2> WithException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        /// <summary>
        /// Sets the first data of the result.
        /// </summary>
        /// <param name="data1">The first data to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public BaseResult<T1, T2> WithData1(T1 data1)
        {
            Data1 = data1;
            return this;
        }

        /// <summary>
        /// Sets the second data of the result.
        /// </summary>
        /// <param name="data2">The second data to set in the result.</param>
        /// <returns>The same result instance in order to chain the property set.</returns>
        public BaseResult<T1, T2> WithData2(T2 data2)
        {
            Data2 = data2;
            return this;
        }

        /// <summary>
        /// When the result is a success then continues with another action that returns a success in order to chain a bunch of actions.
        /// </summary>
        /// <param name="resultFunctionToContinueWith">A function that returns a result to continue with</param>
        /// <returns>This result if not success or the result of the function to continue with if success.</returns>
        public BaseResult WhenSuccessContinueWith(Func<BaseResult<T1, T2>, BaseResult> resultFunctionToContinueWith)
        {
            if (IsNotSuccess)
                return this;

            return resultFunctionToContinueWith(this);
        }

        #endregion Methods (Public Helpers)

        #region Methods (Override)

        /// <inheritdoc />
        /// <summary>
        /// String representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return $"{Status} - {Data1.GetType().Name}:{Data1} - {Data2.GetType().Name}:{Data2}";
        }

        #endregion Methods (Override)
    }