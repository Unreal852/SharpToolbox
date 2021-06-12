using System;

namespace SharpToolbox.Misc
{
    /// <summary>
    /// Provide a progress tracker for bytes operations ( Downloading, Uploading, etc).
    /// </summary>
    public class ProgressTracker
    {
        public ProgressTracker() : this(TimeSpan.FromSeconds(1.0))
        {
        }

        public ProgressTracker(TimeSpan interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// Update Interval
        /// </summary>
        public TimeSpan Interval { get; }

        /// <summary>
        /// Current Progress Percentage
        /// </summary>
        public double ProgressPercentage { get; private set; }

        /// <summary>
        /// Bytes per Second
        /// </summary>
        public double BytesPerSecond { get; private set; }

        /// <summary>
        /// Time left
        /// </summary>
        public TimeSpan TimeLeft { get; private set; }

        /// <summary>
        /// Total Bytes
        /// </summary>
        public long TotalBytes { get; set; }

        /// <summary>
        /// Last Bytes
        /// </summary>
        public long LastBytes { get; set; }

        /// <summary>
        /// Last Update
        /// </summary>
        public DateTime LastUpdate { get; private set; }

        /// <summary>
        /// Extra data
        /// </summary>
        public object ExtraData { get; set; }

        /// <summary>
        /// Triggered when the progress is updated
        /// </summary>
        public event EventHandler<ProgressTracker> ProgressChanged;

        /// <summary>
        /// Reset values
        /// </summary>
        public void Reset()
        {
            LastBytes = 0;
            ExtraData = null;
        }

        /// <summary>
        /// Sets the current progress.
        /// Call this every times that you need to update your progress.
        /// </summary>
        /// <param name="bytes">New Bytes</param>
        /// <param name="totalBytes">Total Bytes</param>
        public void SetProgress(long bytes, long totalBytes)
        {
            if (LastBytes <= 0)
            {
                TotalBytes = totalBytes;
                LastBytes = bytes;
                LastUpdate = DateTime.Now;
                return;
            }

            TimeSpan elapsed = DateTime.Now - LastUpdate;
            if (elapsed.TotalSeconds < Interval.TotalSeconds)
                return;
            long bytesDiff = bytes - LastBytes;
            if (bytesDiff <= 0)
                return;
            BytesPerSecond = bytesDiff / elapsed.TotalSeconds;
            TimeLeft = TimeSpan.FromSeconds((TotalBytes - bytes) / BytesPerSecond);
            ProgressPercentage = (LastBytes / (double) TotalBytes) * 100;
            LastBytes = bytes;
            LastUpdate = DateTime.Now;
            ProgressChanged?.Invoke(this, this);
        }
    }
}