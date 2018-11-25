namespace MasterData1000
{
    public partial class ServiceResult : object
    {
        public T CastByClone<T>() where T : ServiceResult, new()
          => new T
          {
              StatusAsInt = StatusAsInt,
              Status = Status,
              Message = Message,
              Data = Data,
          };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() : base()
        {

        }
        public new T Data { get => (T)base.Data; set => base.Data = value; }
    }
}
