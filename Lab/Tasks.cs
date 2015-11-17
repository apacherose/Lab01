using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab
{
    public class Tasks
    {
        string[] urls = new string[] { "http://dnevnik.bg", "http://vesti.bg", "http://www.welt.de/", "http://www.msn.com/" };

        /// <summary>
        /// паралелно асинхронно изпълнение с await Task.WhenAll(tasks);
        /// </summary>
        public async void TestAsync()
        {            
            DateTime t1 = DateTime.Now;

            Console.WriteLine("o0");

            var tasks = urls.Select(url => TestAsync2(url)).ToArray();

            var res = await Task.WhenAll(tasks);

            foreach (var r in res)
            {
                Console.WriteLine(r.Length);
            }

            var s = DateTime.Now - t1;           

            Console.WriteLine(s.ToString());

        }

        /// <summary>
        /// последователно изпълнение
        /// </summary>
        public async void Test()
        {
            DateTime t1 = DateTime.Now;

            Console.WriteLine("o0");

            foreach (var url in urls)
            {
                var cl = new HttpClient();
                var r = await cl.GetStringAsync(url);

                Console.WriteLine(r.Length);
            }

            var s = DateTime.Now - t1;

            Console.WriteLine(s.ToString());
        }

        /// <summary>
        /// паралелно асинхронно изпълнение с итериране на await t;
        /// </summary>
        public async void TestAsync2()
        {
            DateTime t1 = DateTime.Now;

            Console.WriteLine("o0");

            var tasks = urls.Select(url => TestAsync2(url)).ToArray();

            foreach (var t in tasks)
            {
                var r = await t;
                Console.WriteLine(r.Length);
            }

            var s = DateTime.Now - t1;

            Console.WriteLine(s.ToString());

        }


        public async Task<int> TestAsync1(int val)
        {
            var r = await Task.Run(() => { Thread.Sleep(5000); return val; });
            return r;
        }

        public async Task<string> TestAsync2(string url)
        {
            var cl = new HttpClient();
            return await cl.GetStringAsync(url);
        }

    }
}
