using System;

namespace ADOdotNetPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Execution ex = new Execution();
            /*ex.Insert();
            ex.Delete();
            ex.Display();
            ex.ReturnBook();
            ex.BorrowBook();*/

            Console.WriteLine("Welcome to ABC Library System!\n" +
               "1: Add Book\n" +
               "2: Borrow Book\n" +
               "3: Return Book\n" +
               "4: Display List\n" +
               "5: Exit\n" +
               "============================");

            while(true)
            {
                Console.WriteLine("\nPlease Select an Option");
                int option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("=============================");
                if (option==1)
                {
                    ex.Insert();
                }
                else if(option==2)
                {
                    ex.BorrowBook();
                }
                else if(option==3)
                {
                    ex.ReturnBook();
                }
                else if(option==4)
                {
                    ex.Display();
                }
                else if(option==5)
                {
                    Console.WriteLine("Thanks");
                    break;
                }
                Console.WriteLine("=============================");
            }
        }
    }
}
