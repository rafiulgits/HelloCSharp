using System;
using System.Threading;

namespace CSharp.Threads
{
    public class MultiThread {
        private Object obj = new Object();

        public MultiThread(){
            // Create a thread pool on an operation 
            // By default a thread will be created in this pool
            ThreadPool.QueueUserWorkItem(Operation);

            // Creating 2 addition threads and add in this pool
            var thread1 = new Thread(Operation);
            thread1.IsBackground = true;
            thread1.Name = "Additional Thread : 1";
            thread1.Start("Add Thread 1 callback");

            var thread2 = new Thread(Operation);
            thread2.Priority = ThreadPriority.AboveNormal;
            thread2.Name = "Additional Thread : 2";
            thread2.Start("Add Thread 2 callback");

            // Sleep Main Thread
            Thread.Sleep(1000);
            Thread.CurrentThread.Name = "Main";
            Operation("Main Thread callback");
        }

        public void Operation(Object callback){
            Console.WriteLine($"Called by {callback}\n");
            lock(obj){
                Console.WriteLine($"Executing : {callback}");
                var thread = Thread.CurrentThread;
                Console.WriteLine($"Managed Thread : #{thread.ManagedThreadId}");
                Console.WriteLine($"\tThread Name : {thread.Name}");
                Console.WriteLine($"\tBackground Thread : {thread.IsBackground}");
                Console.WriteLine($"\tThread Pool : {thread.IsThreadPoolThread}");
                Console.WriteLine($"\tPriority : {thread.Priority}\n");
            }
        }
    }

    public class ThreadsExample {
        public static void MultiThreading() {
            new MultiThread();
        }
    }
}