using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace SmallPdfTest.Services
{
    public class IsolatedStorageService : IIsolatedStorageService
    {
        private readonly IsolatedStorageFile isoStore =
            IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        public void Save(string filename, object item)
        {
            using (var isoStream = new IsolatedStorageFileStream(filename, FileMode.Create, isoStore))
            {
                using (var writer = new StreamWriter(isoStream))
                {
                    var content = JsonConvert.SerializeObject(item);

                    writer.Write(content);
                }
            }
        }

        public T Load<T>(string filename)
        {
            if (isoStore.FileExists(filename))
                using (var isoStream = new IsolatedStorageFileStream(filename, FileMode.Open, isoStore))
                {
                    using (var reader = new StreamReader(isoStream))
                    {
                        var content = reader.ReadToEnd();
                        var res = JsonConvert.DeserializeObject<T>(content);

                        return res;
                    }
                }

            return default;
        }
    }

    public interface IIsolatedStorageService
    {
        void Save(string filename, object item);

        T Load<T>(string filename);
    }
}