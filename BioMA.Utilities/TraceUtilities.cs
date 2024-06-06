
namespace JRC.IPSC.MARS.Utilities
{
    public static class TraceUtilities
    {
        static private System.Diagnostics.TraceSource Source = new System.Diagnostics.TraceSource(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

        /// <summary>
        ///     Writes a trace event message to the trace listeners in the 
        ///     System.Diagnostics.TraceSource.Listeners
        ///     collection using the specified event type, event identifier, and message.
        /// </summary>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values
        ///  that specifies the event type of the trace data.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">The trace message to write.</param>
        [System.Diagnostics.Conditional("TRACE")]
        static public void TraceEvent(System.Diagnostics.TraceEventType eventType, int id, string message)
        {
            Source.TraceEvent(eventType, id, message); Source.Flush();

        }

        /// <summary>
        /// Add a listener to the trace
        /// </summary>
        /// <param name="listener"></param>
        static public void AddListener(System.Diagnostics.TraceListener listener)
        {
            Source.Listeners.Add(listener);

        }
    }
}

