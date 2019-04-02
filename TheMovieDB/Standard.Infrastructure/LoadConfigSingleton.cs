namespace Standard.Infrastructure
{
     class LoadConfigSingleton
    {
        private static LoadConfigSingleton _instance;

        public string APIKey => "1f54bd990f1cdfb230adb312546d765d";
        public string URL => "https://api.themoviedb.org/3/";

        private static object syncLock = new object();

        protected LoadConfigSingleton()
        {
            //TBD charge values from config.json
 
        }

        public static LoadConfigSingleton GetLoadConfigSingleton()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LoadConfigSingleton();
                    }
                }
            }

            return _instance;
        }




    }
}
