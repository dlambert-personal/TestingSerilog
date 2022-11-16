using FileHelpers;
using System;

namespace FAIMlib.tags
{
    /// <summary>
    /// RECEIPT TIMESTAMP
    /// Indicated the date and time when the message
    /// was first queued to the Fedwire Funds Service.
    /// </summary>
    [FixedLengthRecord()]
    public class tag1110
    {
        /// <summary>
        /// MMDD, based on calendar date.
        /// </summary>
        [FieldFixedLength(4)]
        public string ReceiptDate;

        /// <summary>
        /// HHMM, based on a 24-hour clock (Eastern time).
        /// </summary>
        [FieldFixedLength(4)]
        public string ReceiptTime;

        /// <summary>
        /// Uniquely identifies the Fedwire Funds Service site
        /// that processed the message(i.e., FT01, FT02 or FT03).
        /// </summary>
        [FieldFixedLength(4)]
        public string ReceiptApplicationId;
    }
}
