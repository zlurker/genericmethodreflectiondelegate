using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DynamicPath : Attribute
    {
    }

    public class DelegateRunner<T> : RunnerBase where T : class
    {
        public DynamicPathDelegate<T> dynamicDelegate;

        public DelegateRunner(DynamicPathDelegate<T> degInst)
        {
            dynamicDelegate = degInst;
            // Apparently converting this is the problem! That's why it led to it being 11 seconds. 
            // Currently testcase ends at 4 as we cache the value early.
            //inst = testCase as T;
        }

        public override void RunDeleg(object target)
        {
            T delegTarget = target as T;

            if (delegTarget != null)
                dynamicDelegate(delegTarget);
            else
                Console.WriteLine($"Target is not {typeof(T).Name}");
        }
    }

    public class RunnerBase
    {
        public virtual void RunDeleg(object target)
        {

        }
    }

    public delegate void DynamicPathDelegate<T>(T target);

    class DynamicPathManager
    {
        public Dictionary<Tuple<Type, string>, RunnerBase> dynamicPaths;

        public DynamicPathManager()
        {
            dynamicPaths = new Dictionary<Tuple<Type, string>, RunnerBase>();
            MethodInfo[] methods = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsClass).SelectMany(x => x.GetMethods()).Where(x => x.GetCustomAttributes(typeof(DynamicPath), false).FirstOrDefault() != null).ToArray();

            for (int i = 0; i < methods.Length; i++)
            {
                Tuple<Type, string> key = Tuple.Create(methods[i].DeclaringType, methods[i].Name);

                if (!dynamicPaths.ContainsKey(key))
                {
                    Console.WriteLine(key.ToString());
                    Type dR = typeof(DelegateRunner<>).MakeGenericType(methods[i].DeclaringType);
                    Type d = typeof(DynamicPathDelegate<>).MakeGenericType(methods[i].DeclaringType);
                    dynamicPaths.Add(key, Activator.CreateInstance(dR, methods[i].CreateDelegate(d)) as RunnerBase);
                }               
            }
        }
    }
}
