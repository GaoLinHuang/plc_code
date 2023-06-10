namespace Windows.Base
{
    public class Singleton<T> where T : class, new()
    {
        private static object Loc = new object();
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Loc)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }

                return _instance;
            }
        }

        public static T GetInstance()
        {
            return Instance;
        }
    }
}