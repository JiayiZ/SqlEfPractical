using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlynnSqlEfPractical_Aiden
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new FlynnPracticalTestEntities())
            {
                //inner join
                var InnerJoinQuery = from code in context.Table_1
                                     join name in context.Table_2 on code.Id equals name.Id
                                     select new { code.Id, code.Code, Name = name.Name };

                //left join
                var LeftJoinQuery = from code in context.Table_1
                                    join name in context.Table_2 on code.Id equals name.Id
                                    into temp
                                    from name in temp.DefaultIfEmpty()
                                    select new { code.Id, code.Code, Name = name.Name };
               
                //Right join
                var RightJoinQuery = from name in context.Table_2
                                     join code in context.Table_1 on name.Id equals code.Id
                                     into temp
                                     from code in temp.DefaultIfEmpty()
                                     select new { name.Id, code.Code, name.Name };

                //Outer join
                var OuterJoinQuery = LeftJoinQuery.Union(RightJoinQuery);
                
                //Display to console
                Console.WriteLine("Result of Table_1 inner join Table_2:");
                foreach (var result in InnerJoinQuery)
                {
                    Console.WriteLine(result);             
                }
                Console.WriteLine();

                Console.WriteLine("Result of Table_1 left join Table_2:");
                foreach (var result in LeftJoinQuery)
                {
                    Console.WriteLine(result);
                }
                Console.WriteLine();
                
                Console.WriteLine("Result of Table_1 right join Table_2:");
                foreach (var result in RightJoinQuery)
                {
                    Console.WriteLine(result);
                }
                Console.WriteLine();

                Console.WriteLine("Result of Table_1 outer join Table_2:");
                foreach (var result in OuterJoinQuery)
                {
                    Console.WriteLine(result);
                }
                Console.WriteLine();
               
                Console.WriteLine("Press any key to close the console...");
                Console.ReadKey();
            }
        }
    }
}
