public interface IPool <T> where T : IPoolObject {
    public T GetObject();
    public void ReturnObject(T poolObject);
}