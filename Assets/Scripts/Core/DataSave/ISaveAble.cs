namespace Core.DataSave
{
    public interface ISaveAble
    {
        public object SaveData();
        public void LoadData(object data);
    }
}