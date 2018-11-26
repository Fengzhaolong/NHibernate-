using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

using FluentNHibernate_Domain;
namespace FluentNHibernate_Data
{
    /// <summary>
    /// NHibernate的帮助类
    /// </summary>
    public class NHibernateConnection
    {
        private static ISessionFactory _isessionFactory = null;

        private static void InitializeSessionFactory()
        {
            _isessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(m => m.FromConnectionStringWithKey("ConnectionStr")).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<Student>()).Mappings(m => m.FluentMappings.Conventions.Setup(x => 
            {
                x.Add(typeof(EnumConvention));
            })).BuildSessionFactory();

            //这个是对数据库的连接配置——>读取的是UI层的webconfig中的connectionstring节点的name
            //Database(MsSqlConfiguration.MsSql2012.ConnectionString(m => m.FromConnectionStringWithKey("ConnectionStr")).ShowSql())
            //也可以直接
            //Database(MsSqlConfiguration.MsSql2012.ConnectionString("Server = 101.251.196.135; pooling = true;Max Pool Size = 40000; Min Pool Size = 0; initial catalog = Tidebuy_SCM_Test;uid = scmtest;pwd =@^ AIv4t * 4;").ShowSql())


            //添加映射配置....所有在这个命名空间下的类都会被映射,前提是都有写映射类   FluentNHibernate对比NHibernate最好的一点就是不用写配置文件。
            //至于说为什么AddFromAssemblyof《student》 不一定要写student, 你只要保证你的Entity和Mapping都在同一个命名空间下 这个前提下 ：《随便写》
            //Mappings(m => m.FluentMappings.AddFromAssemblyOf<Student>())

            //NHibernate本身自带支持Enum类型会自动把枚举类型转化成string类型
            //添加NHibernate的枚举配置...
            //原因：有了枚举配置可以直接把枚举类型对应的值存到数据库并且读取的时候会自动转化枚举对应的汉子或者英文 这个是EF目前不支持的
            //Mappings(m => m.FluentMappings.Conventions.Setup(x => x.Add<EnumConvention>()))
        }

        /// <summary>
        /// 只读属性
        /// </summary>
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_isessionFactory==null)
                {
                    InitializeSessionFactory();
                }
                return _isessionFactory;
            }
        }

        /// <summary>
        /// 从线程安全的ISessionFactory中打开一个ISession
        /// 因为ISession是不安全的
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession()
        {
           return SessionFactory.OpenSession();
        }

    }
}
