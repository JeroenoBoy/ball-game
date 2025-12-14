namespace Sockets {
    public class Reference<T> {
        private T obj;

        public bool IsSet() {
            return obj != null;
        }

        public T Get() {
            return obj;
        }

        public void Set(T obj) {
            this.obj = obj;
        }
    }
}