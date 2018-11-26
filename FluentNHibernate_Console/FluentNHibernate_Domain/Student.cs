using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FluentNHibernate_Domain
{
    public class Student
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Sex { get; set; }
        public virtual int Age { get; set; }
        public virtual ClassRoom ClassInto { get; set; }
    }

    /// <summary>
    /// 学生班级的枚举
    /// </summary>
    public enum ClassRoom
    {
        A_1603 = 1,
        B_1603 = 2
    }

    /// <summary>
    /// Student映射类
    /// </summary>
    public class StudentMapping:ClassMap<Student>
    {
        /// <summary>
        /// 映射必须写在构造函数中
        /// </summary>
        public StudentMapping()
        {
            //逐渐自增的映射——必存在
            Id(x => x.ID);
            //map映射属性——>如果数据库中存在字段但是类中没有，可以不必映射 
            //但是如果数据库有字段 映射有字段 但是没有查询这个字段就一定会报错 
            //因为映射是跟表结构一一对应的
            Map(x => x.Name);
            Map(x => x.Age);
            Map(x => x.Sex);
            Map(x => x.ClassInto);
            //映射表名——>必存在
            Table("Student");
        }
    }

}
