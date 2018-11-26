using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate_Business;
using FluentNHibernate_Domain;
namespace FluentNHibernate_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Student_BLL bll = new Student_BLL();

            ////读取
            //IList<Student> list = bll.GetStudentList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.ID + "..." + item.Name + "..." + item.Sex + "..." + item.ClassInto);
            //    Console.WriteLine();
            //}


            //插入
            Student stu = new Student()
            {
                Name="冯兆隆",
                Age=13,
                ClassInto=ClassRoom.B_1603,
                Sex=true
            };

            bll.Insert(stu);

            IList<Student> list = bll.GetStudentList();
            foreach (var item in list)
            {
                Console.WriteLine(item.ID + "..." + item.Name + "..." + item.Sex + "..." + item.ClassInto);
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
