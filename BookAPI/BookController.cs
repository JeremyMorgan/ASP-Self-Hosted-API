using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookAPI
{
    public class BookController : ApiController
    {
        //public Book[] ourbooks = new Book[]  
        //{  
        //    new Book { Id = 1, Title = "Microsoft Visual C# 2012", Author = "John Sharp", Price = 29.99M },  
        //    new Book { Id = 2, Title = "C# 5.0 in a nutshell", Author = "Joseph Albahari", Price = 19.99M },  
        //    new Book { Id = 3, Title = "C# in Depth, 3rd Edition", Author = "Jon Skeet", Price = 29.99M },  
        //    new Book { Id = 4, Title = "Pro ASP.NET MVC 5", Author = "Adam Freeman", Price = 34.01M }
        //};

        static List<Book> ourbooks = InitBooks();

        private static List<Book> InitBooks()
        {
            var booklist = new List<Book>();
            booklist.Add(new Book { Id = 1, Title = "Microsoft Visual C# 2012", Author = "John Sharp", Price = 29.99M });
            booklist.Add(new Book { Id = 2, Title = "C# 5.0 in a nutshell", Author = "Joseph Albahari", Price = 19.99M }); 
            booklist.Add(new Book { Id = 3, Title = "C# in Depth, 3rd Edition", Author = "Jon Skeet", Price = 29.99M });
            booklist.Add(new Book { Id = 4, Title = "Pro ASP.NET MVC 5", Author = "Adam Freeman", Price = 34.01M });

            return booklist;
        }


        public IEnumerable<Book> Get()
        {
            return ourbooks;
        }

        public Book Get(int Id)
        {

            var result = (from b in ourbooks
                          where b.Id == Id
                          select b).FirstOrDefault();

            if (result == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No book with ID = {0}", Id)),
                    ReasonPhrase = "Book ID Not Found"
                };
                throw new HttpResponseException(resp);
            }

            return result;
        }

        public void Post([FromBody]Book b)
        {
            b.Id = ourbooks.Count + 1;
            ourbooks.Add(b);

            var resp = new HttpResponseMessage(HttpStatusCode.Created);           
            throw new HttpResponseException(resp);
        }
        public void Put(int Id, [FromBody]Book book)
        {
            var result = (from b in ourbooks
                          where b.Id == Id
                          select b).FirstOrDefault();
            
            result.Title = book.Title;
            result.Author = book.Author;
            result.Price = book.Price;

            var resp = new HttpResponseMessage(HttpStatusCode.Accepted);
            throw new HttpResponseException(resp);
        }

        public void Delete(int Id)
        {
            var result = (from b in ourbooks
                          where b.Id == Id
                          select b).FirstOrDefault();

            ourbooks.Remove(result);
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            throw new HttpResponseException(resp);
        }                   

    }
}
