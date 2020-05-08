using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADOdotNetPractice
{
    class Execution
    {
        public void Insert()
        {
            

            Console.Write("Book Name: ");
            string name = Console.ReadLine();
            Console.Write("Book Code: ");
            string code = Console.ReadLine();
            Console.Write("Author Name: ");
            string author = Console.ReadLine();
            Console.Write("Book Stock: ");
            int stock = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Successfully Book Added in Library!");


            string connectionString = "Server=localhost;Database=dbLibrary;Integrated Security=True";
            string sql = "insert into bookList(id, name, author, stock) values( @a , @b, @c, @d )";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    //parameter Bind
                    var idPM = new SqlParameter("@a", System.Data.SqlDbType.Text);
                    idPM.Value = code;

                    var namePM = new SqlParameter("@b", System.Data.SqlDbType.Text);
                    namePM.Value = name;

                    var authorPM = new SqlParameter("@c", System.Data.SqlDbType.Text);
                    authorPM.Value = author;

                    var stockPM = new SqlParameter("@d", System.Data.SqlDbType.Int);
                    stockPM.Value = stock;

                    command.Parameters.Add(idPM);
                    command.Parameters.Add(namePM);
                    command.Parameters.Add(authorPM);
                    command.Parameters.Add(stockPM);

                    command.ExecuteNonQuery();
                }
            }

        }

        public void Delete()
        {
            Console.Write("Enter Book Code to Delete: ");
            string code = Console.ReadLine();

            string connectionString = "Server=localhost;Database=dbLibrary;Integrated Security=True";
            string sql = "delete from bookList where id=@a";


            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql,connection))
                {
                    connection.Open();

                    var codePM = new SqlParameter("@a", System.Data.SqlDbType.VarChar);
                    codePM.Value = code;

                    command.Parameters.Add(codePM);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Book Deleted Successfuly!");
        }

        public void Display()
        {   
            string connectionString = "Server=localhost;Database=dbLibrary;Integrated Security=True";
            string sql = "Select * from bookList";

            using(var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql,connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();
                        string author = reader["author"].ToString();
                        int stock = Convert.ToInt32(reader["stock"].ToString());

                        Console.Write("Book Code : " + id+"\t");
                        Console.Write("Book name : " + name + "\t");
                        Console.Write("Author : "+author + "\t");
                        Console.WriteLine("Stock : " + stock + "\t");
                        Console.WriteLine("_________________________________");
                    }

                }
            }

        }

        public void ReturnBook()
        {
            Console.Write("Enter book code to return : ");
            string code = Console.ReadLine();
            Console.Write("Enter quantity : ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            string connectionString = "Server=localhost;Database=dbLibrary;Integrated Security=true";
            string sql = "UPDATE bookList SET stock=stock+@b WHERE id=@a";

            using(var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    var idPM = new SqlParameter("@a", System.Data.SqlDbType.VarChar);
                    idPM.Value = code;
                    var qPM = new SqlParameter("@b", System.Data.SqlDbType.Int);
                    qPM.Value = quantity;

                    command.Parameters.Add(idPM);
                    command.Parameters.Add(qPM);

                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Book Returned");

        }
        public void BorrowBook()
        {
            Console.Write("Enter book code to Borrow : ");
            string code = Console.ReadLine();
            Console.Write("Enter quantity : ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            string connectionString = "Server=localhost;Database=dbLibrary;Integrated Security=true";
            
            string sql1 = "select stock from bookList where id=@x";
            string sql2= "UPDATE bookList SET stock=stock-@b WHERE id=@a";

            int flag = 1;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql1, connection))
                {
                    connection.Open();

                    var idPM = new SqlParameter("@x", System.Data.SqlDbType.VarChar);
                    idPM.Value = code;
                    
                    command.Parameters.Add(idPM);

                    int st=0;

                    SqlDataReader data = command.ExecuteReader();
                    while(data.Read())
                    {
                        st = Convert.ToInt32(data["stock"].ToString());
                        break;
                    }

                    if (st < quantity)
                    {
                        flag = 0;
                    }
                }
            }

            if(flag==0)
            {
                Console.WriteLine("out of stock");
            }
            else
            {

                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql2, connection))
                    {
                        connection.Open();

                        var idPM = new SqlParameter("@a", System.Data.SqlDbType.VarChar);
                        idPM.Value = code;

                        var qPM = new SqlParameter("@b", System.Data.SqlDbType.Int);
                        qPM.Value = quantity;

                        command.Parameters.Add(idPM);
                        command.Parameters.Add(qPM);

                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("book borrowed success");
            }

        }



    }
}
