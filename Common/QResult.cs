namespace Qistas
{
    public class QResult<VALUE, DATA>
    {
        public VALUE Value { get; set; }
        public DATA Data { get; set; }
        public QResultType Type { get; set; }
        public bool IsSuccess { get; set; } = true;
        public QResultType QResultType { get; private set; }

        public string FailureMessage { get; set; } = "";

        public static QResult<VALUE, DATA> Success(VALUE value)
        {
            return new QResult<VALUE, DATA>
            {
                Value = value,
                QResultType = QResultType.Success,
            };
        }

        public static QResult<VALUE, DATA> Success(VALUE value, DATA data)
        {
            return new QResult<VALUE, DATA>
            {
                Value = value,
                Data = data,
                QResultType = QResultType.SuccessWithData,
            };
        }






        public static QResult<VALUE, DATA> Failure(string message)
        {
            return new QResult<VALUE, DATA>
            {
                IsSuccess = false,
                QResultType = QResultType.Failure,
                FailureMessage = message
            };
        }

        public static QResult<VALUE, DATA> Failure(string message, DATA data)
        {
            return new QResult<VALUE, DATA>
            {
                IsSuccess = false,
                Data = data,
                QResultType = QResultType.FailureWithData,
                FailureMessage = message

            };
        }
    }


    public enum QResultType
    {
        Success,
        SuccessWithData,

        Failure,
        FailureWithData
        ,
    }
}
