using FileHelpers;

namespace FAIMlib.tags
{

    [FixedLengthRecord()]
    public class tag1100
    {
        [FieldFixedLength(2)]
        public string Version;

        /// <summary>
        /// Values are "T" - test or "P" production
        /// </summary>
        [FieldFixedLength(1)]
        public string TestProductionCode;

        /// <summary>
        /// spc - original message
        /// R - retrieval
        /// P - possible duplicate
        /// </summary>
        [FieldFixedLength(1)]
        public string MessageDuplication;

        /// <summary>
        /// 0 - in process
        /// 2 - successfully processed
        /// 3 - rejected
        /// 7 - successfully processed
        /// N - successfully processed
        /// S - successfully processed
        /// </summary>
        [FieldFixedLength(1)]
        public string MessageStatusIndicator;


    }
}
