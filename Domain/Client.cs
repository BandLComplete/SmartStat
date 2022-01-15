using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain
{
    public class Client
    {
        protected readonly HttpClient client;

        public Client()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            client = new HttpClient(clientHandler);
        }

        protected virtual string Url => "http://SmartStat.somee.com";

        public async Task<bool> Login(User user)
        {
            return await SendPost<User, bool>(user, Api.Login);
        }

        public async Task<bool> Register(User user)
        {
            return await SendPost<User, bool>(user, Api.Register);
        }

        public async Task<bool> DeleteUser(User user)
        {
            return await SendPost<User, bool>(user, Api.DeleteUser);
        }

        public async Task<bool> AddPractice(Practice practice)
        {
            return await SendPost<Practice, bool>(practice, Api.AddPractice);
        }

        public async Task<bool> DeletePractice(Practice practice)
        {
            return await SendPost<Practice, bool>(practice, Api.DeletePractice);
        }

        /// <summary>
        ///     Поиск тренировок
        /// </summary>
        /// <param name="practice">Сюда пихать нужные значения для поиска. Все Users должны учавстовать в тренировке</param>
        /// <returns></returns>
        public async Task<Practice[]> GetPractices(Practice practice)
        {
            return await SendPost<Practice, Practice[]>(practice, Api.GetPractices);
        }

        /// <summary>
        /// Удалять можно на похуй - если нет, то вернёт true. Добавление кидает ошибку, если уже есть. Обновление кидает ошибку, если ещё нет.
        /// </summary>
        /// <param name="stat">Для добавления и обновления должны быть ключевые значения указаны (User, Date, Name)</param>
        /// <param name="dbAction"></param>
        /// <returns></returns>
        public async Task<bool> PatchStat(Stat stat, DbAction dbAction)
        {
            return await SendPost<Stat, bool>(stat, Api.PatchStat, ("dbAction", dbAction.ToString()));
        }

        /// <summary>
        ///     Поиск тренировок
        /// </summary>
        /// <param name="stat">User'a обязательно указывать. Дату и название по желанию</param>
        /// <returns></returns>
        public async Task<Stat[]> GetStats(Stat stat)
        {
            return await SendPost<Stat, Stat[]>(stat, Api.GetStats);
        }

        private async Task<TResult> SendPost<T, TResult>(T body, string method, params (string Name, string Value)[] queries)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(body);
            var content = new ByteArrayContent(json);
            var url = AddQueries($"{Url}/{method}", queries);
            var response = await client.PostAsync(url, content).ConfigureAwait(false);
            return await ReadBody<TResult>(response);
        }

        protected static string AddQueries(string url, (string Name, string Value)[]? queries)
        {
            if (queries == null || queries.Length == 0)
                return url;

            var builder = new StringBuilder(url);
            var isFirst = true;
            foreach (var (name, value) in queries)
            {
                builder.Append(isFirst ? "?" : "&");
                isFirst = false;
                builder.Append(name);
                builder.Append('=');
                builder.Append(value);
            }

            return builder.ToString();
        }

        protected static async Task<T> ReadBody<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception("Request failed");
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync()) ??
                   throw new NullReferenceException();
        }
    }
}