using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    //public delegate void D1(Test inst);
    //public delegate void D1<T>(T inst);
    public class Guitar
    {

        [DynamicPath]
        public void Destroy()
        {
            Console.WriteLine("Play guitar");
        }

        public void NonDestroyed()
        {

        }
    }
    
    public class Destruction
    {

        [DynamicPath]
        public void Morty()
        {
            Console.WriteLine("Man evolved from corn");
        }
    }

    public class InheritedTestLayer4 : InheritedTest
    {
        public InheritedTestLayer4(string inp):base(inp)
        {

        }

        [DynamicPath]
        public void Destruction2(){
        }
     }
        
    public class Unown
    {
        [DynamicPath]
        public void LOL()
        {

        }
    }

    public class Unown2
    {
        [DynamicPath]
        public void Unown() { }
    }


    public class InheritedTest : Test
    {
        public InheritedTest(string inp) : base(inp)
        {

        }

        public override void Method1()
        {
            //Console.WriteLine($"{testinput} Called 1 override!");
        }

        [DynamicPath]
        public void Method4()
        {
            //Console.WriteLine($"{testinput} Called 4!");
        }
    }

    public class Test
    {
        protected string testinput;

        public Test(string inp)
        {
            testinput = inp;
        }

        [DynamicPath]
        public void Method0()
        {
            Console.WriteLine($"{testinput} Called 0!");
        }

        [DynamicPath]
        public virtual void Method1()
        {
            Console.WriteLine($"{testinput} Called 1!");
        }

        public void Method2()
        {
            Console.WriteLine($"{testinput} Called 2!");
        }
    }

    public class NonTest
    {
        string testinput;

        public NonTest(string inp)
        {
            testinput = inp;
        }

        [DynamicPath]
        public void Method0()
        {
            Console.WriteLine($"{testinput} Called 0!");
        }

        [DynamicPath]
        public virtual void Method1()
        {
            Console.WriteLine($"{testinput} Called 1!");
        }

        public void Method2()
        {
            Console.WriteLine($"{testinput} Called 2!");
        }
    }

    class Program
    {


        /*private static D1 GetDelegateWithArg(MethodInfo method)
        {
            var instanceParameter = Expression.Parameter(typeof(Test));
            var body = Expression.Call(Expression.Convert(instanceParameter, method.DeclaringType), method);
            var lambda = Expression.Lambda<D1>(body, instanceParameter);
            return lambda.Compile();
        }*/

        static void Main(string[] args)
        {
            #region unused
            /*Dictionary<Tuple<Type, string>, RunnerBase> dynamicPaths = new Dictionary<Tuple<Type, string>, RunnerBase>();
            var methods = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x == typeof(Test) || x.IsSubclassOf(typeof(Test))).SelectMany(x => x.GetMethods()).Where(x => x.GetCustomAttributes(typeof(ExtentionMethods), false).FirstOrDefault() != null);
            Console.WriteLine("Begin attribute parsing");
            //List<D1> delegTest = new List<D1>();

            //RunnerBase[] runnerBases = new RunnerBase[methods.Count()];

            //Dictionary<Type, int> typeMap = new Dictionary<Type, int>();
            //typeMap.Add(typeof(InheritedTest), 0);
            //typeMap.Add(typeof(Test), 1);

            //Type[] declareT = new Type[methods.Count()];
            int currEle = 0;
            InheritedTest testinst = new InheritedTest("Seperate instanc test");

            foreach (var method in methods)
            {
                //delegTest.Add(GetDelegateWithArg(method));
                //Type targetType = null;
                Type dR = typeof(DelegateRunner<>).MakeGenericType(method.DeclaringType);
                Type d = typeof(D1<>).MakeGenericType(method.DeclaringType);

                Tuple<Type, string> key = Tuple.Create(method.DeclaringType, method.Name);

                if (!dynamicPaths.ContainsKey(key))
                    dynamicPaths.Add(key, Activator.CreateInstance(dR, method.CreateDelegate(d), testinst) as RunnerBase);
                /*if (method.DeclaringType == typeof(Test))
                    runnerBases[currEle] = new DelegateRunner<Test>((D1<Test>)method.CreateDelegate(typeof(D1<Test>)),testinst);

                if (method.DeclaringType == typeof(InheritedTest))
                    runnerBases[currEle] = new DelegateRunner<InheritedTest>((D1<InheritedTest>)method.CreateDelegate(typeof(D1<InheritedTest>)),testinst);
                    
                //declareT[currEle] = method.DeclaringType;
                currEle++;

                //delegTest.Add(GetDelegateWithArg(method));
                //delegTest.Add((D1)method.CreateDelegate(typeof(D1)));
                Console.WriteLine($"{method.Name} created from {method.DeclaringType.Name}");
            }

            

            
            Tuple<Type, string> testKey = Tuple.Create(typeof(InheritedTest), "Method4");
            RunnerBase inst = dynamicPaths[testKey];
            for (int i = 0; i < 100000000; i++)
                inst.RunDeleg();
            //for (int j = 0; j < runnerBases.Length; j++)
            //runnerBases[j].RunDeleg();

            //for (int j = 0; j < delegTest.Count; j++)
            //delegTest[j](testinst);
            */
            #endregion

            Console.WriteLine("Begin attribute parsing");
            //DynamicPathManager manager = new DynamicPathManager();
            DynamicPathManager manager = new DynamicPathManager();
            Console.WriteLine($"Parsed {manager.dynamicPaths.Count} marked functions.");

            Console.WriteLine($"End attribute parsing. Press key to run methods.");
            Console.ReadKey();
            Console.WriteLine("Running...");

            Test guitar = new Test("ultimate");
            manager.dynamicPaths[Tuple.Create(typeof(Test), "Method0")].RunDeleg(guitar);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
