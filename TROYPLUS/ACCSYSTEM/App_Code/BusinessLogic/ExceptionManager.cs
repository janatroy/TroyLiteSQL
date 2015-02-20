using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

/// <summary>
/// Summary description for ExceptionManager
/// </summary>
public static class TroyLiteExceptionManager
{
    #region Fields

    /// <summary>
    ///   Exception manager instance to log the exception.
    /// </summary>
    private static readonly ExceptionManager ExceptionManager;

    #endregion

    #region Constructor

    /// <summary>
    ///   Initializes the ExceptionHandler
    /// </summary>
    static TroyLiteExceptionManager()
    {
        //  Create LogWriter.
        Logger.SetLogWriter(new LogWriterFactory().Create(), false);

        // Initialize and set the ExceptionManager for the ExceptionPolicy.
        IConfigurationSource config = ConfigurationSourceFactory.Create();
        ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
        ExceptionManager = factory.CreateManager();
        ExceptionPolicy.SetExceptionManager(ExceptionManager, false);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Logs the exception details into the target file source as specified in the
    ///   exception and logging policy configuration.
    /// </summary>
    /// <param name="exception">Exception to log</param>
    public static Exception HandleException(Exception exception)
    {
        Exception exceptionToReThrow;
        ExceptionManager.HandleException(exception,
                                         "TroyLite Exception Policy",
                                         out exceptionToReThrow);
        return exception;
    }

    /// <summary>
    ///   Logs the exception details into the target file source as specified in the
    ///   exception and logging policy configuration.
    /// </summary>
    /// <param name="exception">Exception to log</param>
    /// <param name="rethrowException">Rethrow the logged exception.</param>
    public static Exception HandleException(Exception exception, bool rethrowException)
    {
        Exception exceptionToReThrow;
        ExceptionManager.HandleException(exception,
                                         "Troylite Exception Policy",
                                         out exceptionToReThrow);

        if (exceptionToReThrow == null)
        {
            exceptionToReThrow = exception;
        }
        if (rethrowException)
        {
            throw exceptionToReThrow;
        }
        return exceptionToReThrow;
    }



    /// <summary>
    /// Get Detailed exception messages
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <param name="errorMessages">Error Messages</param>
    private static void GetExceptionMessages(Exception ex, IList<string> errorMessages)
    {

        errorMessages.Add(ex.Message);
        if (ex.InnerException != null)
        {
            GetExceptionMessages(ex.InnerException, errorMessages);
        }

    }

    #endregion
}