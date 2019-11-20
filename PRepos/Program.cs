using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRepos
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
    }
    public class BookContext : DbContext
    {
        public BookContext() : base("DefaultConnection")
        { }
        public DbSet<Book> Books { get; set; }
    }
    interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetBookList(); // получение всех объектов
        T GetBook(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
    //interface IRepository : IDisposable
    //{
    //    IEnumerable<Book> GetBookList();
    //    Book GetBook(int id);
    //    void Create(Book item);
    //    void Update(Book item);
    //    void Delete(int id);
    //    void Save();
    //}
    public class SQLBookRepository : IRepository<Book>
    {
        private BookContext db;

        public SQLBookRepository()
        {
            this.db = new BookContext();
        }

        public IEnumerable<Book> GetBookList()
        {
            return db.Books;
        }

        public Book GetBook(int id)
        {
            return db.Books.Find(id);
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class HomeController
    {
        IRepository<Book> db;

        public HomeController()
        {
            db = new SQLBookRepository();
        }

        public void Index()
        {
            db.GetBookList();
        }


    }


    public class MongoBookRepository : IRepository<Book>
    {

        public void Save() { }

        public void Dispose() { }

        public void Update(Book item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBookList()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Book item)
        {
            throw new NotImplementedException();
        }


    }

}
