using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    /// <summary>
    /// DB Context class
    /// </summary>
    /// DbContext : Core pkg
    public class BookAppDbContext : DbContext
    {
        // EntityFrameworkCore
        // EntityFrameworkCore.SqlServer
        // EntityFrameworkCore.Tools
        // EntityFrameworkCore.Inmemory
        // System.Configuration.ConfigurationManager
        // Microsoft.Data.SqlClient

        public BookAppDbContext()
        {
             
        }

        public BookAppDbContext(DbContextOptions<BookAppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역
            // App.Config 또는 Web.Config의 연결 문자열 사용
            // 직접 데이터베이스 연결문자열 설정 가능
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
                    "ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Books 테이블의 Created 열의 자동으로 GetDate() 제약조건 부여하기
            modelBuilder.Entity<Book>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        // BookApp 솔루션 관련 모든 테이블에 대한 참조
        public DbSet<Book> Books { get; set; }
        public DbSet<HaruUser> HaruUsers { get; set; }
    }
}
