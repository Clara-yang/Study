using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;

namespace MVVMBinding.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Test ts = new Test();

        #region property
        private string m_HollowDirection;
        public string HollowDirection
        {
            get { return m_HollowDirection; }
            set
            {
                m_HollowDirection = value;
                OnPropertyChanged("HollowDirection");
            }
        }
        private string m_HollowVelocity;
        public string HollowVelocity
        {
            get { return m_HollowVelocity; }
            set
            {
                m_HollowVelocity = value;
                OnPropertyChanged("HollowVelocity");
            }
        }

        private string m_GroundWindDirection;
        public string GroundWindDirection
        {
            get { return m_GroundWindDirection; }
            set
            {
                m_GroundWindDirection = value;
                OnPropertyChanged("GroundWindDirection");
            }
        }

        private string m_GroundWindVelocity;
        public string GroundWindVelocity
        {
            get { return m_GroundWindVelocity; }
            set
            {
                m_GroundWindVelocity = value;
                OnPropertyChanged("GroundWindVelocity");
            }
        }
        private string m_GustSpeed;
        public string GustSpeed
        {
            get { return m_GustSpeed; }
            set
            {
                m_GustSpeed = value;
                OnPropertyChanged("GustSpeed");
            }
        }
        private ICommand m_GroundDirectionCommand;

        public ICommand GroundDirectionCommand
        {
            get { return m_GroundDirectionCommand ?? new RelayCommand<string>(GroundDirectionChanged); }
        }

        private ICommand m_GroundSpeedCommand;

        public ICommand GroundSpeedCommand
        {
            get { return m_GroundSpeedCommand ?? new RelayCommand<string>(GroundWindSpeedChanged); }
        }

        private ICommand m_HollowDirectionCommand;

        public ICommand HollowDirectionCommand
        {
            get { return m_HollowDirectionCommand ?? new RelayCommand<string>(HollowDirectionChanged); }
        }

        private ICommand m_HollowSpeedCommand;

        public ICommand HollowSpeedCommand
        {
            get { return m_HollowSpeedCommand ?? new RelayCommand<string>(HollowWindSpeedChanged); }
        }

        private ICommand m_GustSpeedCommand;

        public ICommand GustSpeedCommand
        {
            get { return m_GustSpeedCommand ?? new RelayCommand<string>(GustSpeedChanged); }
        }
        #endregion

        public MainViewModel()
        {
            var list = new List<Student>();
            list.Add(new Student(1, "陈兆杰", 10, "班级一"));
            list.Add(new Student(2, "陈志杰", 10, "班级一"));
            list.Add(new Student(3, "废纸杰拉德", 10, "班级二"));
            list.Add(new Student(4, "马克吐恩", 12, "班级一"));


            Console.WriteLine("********************解决方法一 分割线******************");
            {
                var groups = list.GroupBy(m => new NewStudentKey
                {
                    Age = m.Age,
                    Class = m.Class
                });
                foreach (var group in groups)
                {
                    Console.WriteLine("分组值 Age:{0},Class:{1}", group.Key.Age, group.Key.Class);
                    foreach (var student in group)
                    {
                        Console.WriteLine(student);
                    }
                }
            }

            var i = "1";
            var b = "true";
            var c = 123;
            Type typeInt = typeof(Int64);
            Type typeBool = typeof(bool);
            Type typeString = typeof(string);

            object intI = ConvertToObject(i, typeInt);
            object boolB = ConvertToObject(b, typeBool);
            object stringC = ConvertToObject(c, typeString);

            var name = "HollowDirection";
            var sourceProperties = typeof(Test).GetProperties();

            string str = "";
            foreach (PropertyInfo p in sourceProperties)
            {
                object pvalue = p.GetValue(ts, null);
                string pname = p.Name;
                str += string.Format("{0}:{1}\n", pname, pvalue);//格式化输出
            }

            //bool aa = ContainProperty(this,"HollowDirection");  LS5A2DKR5PA117382

        }

        #region 环境

        public void WindClear()
        {
            GroundWindDirection = "0";
            GroundWindVelocity = "0";
            HollowDirection = "0";
            HollowVelocity = "0";
            GustSpeed = "0";
        }
        public void TurbulenceChanged(string angle)
        {

        }
        public void HollowHeightChanged(string num)
        {

        }
        /// <summary>
        /// 改变地面风向
        /// </summary>
        public void GroundDirectionChanged(string num)
        {

        }

        /// <summary>
        /// 改变地面风速
        /// </summary>

        public void GroundWindSpeedChanged(string num)
        {

        }

        /// <summary>
        /// 改变中空风向
        /// </summary>
        public void HollowDirectionChanged(string num)
        {

        }

        /// <summary>
        /// 改变中空风速
        /// </summary>
        public void HollowWindSpeedChanged(string num)
        {

        }

        /// <summary>
        /// 改变阵风速度
        /// </summary>
        public void GustSpeedChanged(string num)
        {

        }

        #endregion

        /// <summary>
        /// 利用反射来判断对象是否包含某个属性
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <returns>是否包含</returns>
        public static bool ContainProperty(object instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _findedPropertyInfo = instance.GetType().GetProperty(propertyName);
                return (_findedPropertyInfo != null);
            }
            return false;
        }

        public static void MapProperties<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(dp => dp.Name == sourceProperty.Name && dp.PropertyType == sourceProperty.PropertyType);

                if (destinationProperty != null)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的对象</returns>
        public static object ConvertToObject(object obj, Type type)
        {
            if (type == null) return obj;
            if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType())) // 如果待转换对象的类型与目标类型兼容，则无需转换
            {
                return obj;
            }
            else if ((underlyingType ?? type).IsEnum) // 如果待转换的对象的基类型为枚举
            {
                if (underlyingType != null && string.IsNullOrEmpty(obj.ToString())) // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
                {
                    return null;
                }
                else
                {
                    return Enum.Parse(underlyingType ?? type, obj.ToString());
                }
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type)) // 如果目标类型的基类型实现了IConvertible，则直接转换
            {
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType()))
                {
                    return converter.ConvertFrom(obj);
                }
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    object o = constructor.Invoke(null);
                    PropertyInfo[] propertys = type.GetProperties();
                    Type oldType = obj.GetType();
                    foreach (PropertyInfo property in propertys)
                    {
                        PropertyInfo p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                        {
                            property.SetValue(o, ConvertToObject(p.GetValue(obj, null), property.PropertyType), null);
                        }
                    }
                    return o;
                }
            }
            return obj;
        }

    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }

        public Student(int id, string name, int age, string @class)
        {
            Id = id;
            Name = name;
            Age = age;
            Class = @class;
        }

        public override string ToString()
        {
            return $"Id={Id},Name={Name},Age={Age},Class={Class}";
        }
    }


    class StudentKey
    {
        public int Age { get; set; }
        public string Class { get; set; }
    }

    class Employee
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public int Age { get; set; }
        public char Sex { get; set; }
    }
    class NewStudentKey : IEquatable<NewStudentKey>
    {
        public int Age { get; set; }
        public string Class { get; set; }

        public override int GetHashCode()
        {
            return Age.GetHashCode() ^ Class.GetHashCode();
        }

        public bool Equals(NewStudentKey other)
        {
            return Age == other.Age && Class == other.Class;
        }
    }

}