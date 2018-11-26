using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate_Domain;
using FluentNHibernate_Data;
namespace FluentNHibernate_Business
{
    public class Student_BLL
    {
        NHibernateHelper helper = new NHibernateHelper();
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public bool Insert(Student student)
        {
            return helper.Insert(student);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="student"></param>
        public int Update(Student student)
        {
            try
            {
                helper.Update(student);
                return 1;
            }
            catch (Exception es)
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                helper.Delete<Student>(id);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据id查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetEntity_Single(int id)
        {
            return helper.GetEntity_Single<Student>(id);
        }

        /// <summary>
        /// 获取学生信息列表
        /// </summary>
        /// <returns></returns>
        public IList<Student> GetStudentList()
        {
            return helper.GetEntity_List<Student>();
        }

    }
}
