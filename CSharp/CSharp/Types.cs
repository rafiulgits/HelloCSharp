using System;
using CSharp.Types.Value;
using CSharp.Types.Reference;

namespace CSharp.Types
{
    namespace Value
    {
        public struct Point 
        {
            private double x, y;

            public Point(double x, double y){
                this.x = x;
                this.y = y;
            }        

            public bool isCenter(){
                if(x == 0 && y == 0){
                    return true;
                }
                return false;
            }

            public double GetX(){
                return x;
            }

            public double GetY(){
                return y;
            }
        }

        public enum DAYS { SUNDAY, MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY};
    }

    namespace Reference
    {
        public class MyQueue {
            private int max;
            private int current = -1;
            private int[] array;

            public MyQueue(int size){
                max = size;
                array = new int[size];
            }

            public MyQueue(){
                max = 10;
                array = new int[10];
            }

            public void Enqueue(int value){
                if(current < max -1){
                    current++;
                    array[current] = value;
                }
            }

            public int Dequeue(){
                if(current == -1){
                    return int.MinValue;
                }
                int value = array[0];
                for(int i=1; i<=current; i++){
                    array[i-1] = array[i];
                }
                current--;
                return value;
            }

            public int Size(){
                return current + 1;
            }
        } 
    }


    public class TypesExamples {
        public static void ValueTypes(){
            var p1 = new Point(0, 10.5);
            var sizeOfP1 = System.Runtime.InteropServices.Marshal.SizeOf(p1);
            Console.WriteLine($"Point {p1.GetX()} , {p1.GetY()} : {p1.GetType()} : Size {sizeOfP1} bytes");
            var sundaySize = Value.DAYS.SUNDAY;
            Console.WriteLine($"enum {Value.DAYS.SUNDAY} : type: {Value.DAYS.SUNDAY.GetType()} : size {sundaySize}");
        }

        public static void ReferenceTypes(){
            MyQueue queue = new MyQueue(5);
            queue.Enqueue(10);
            queue.Enqueue(5);
            queue.Enqueue(45);

            while(queue.Size() != 0){
                Console.WriteLine(queue.Dequeue());
            }

            var myGames= Tuple.Create("WWE", "Cricket", "Tennis");
            Console.WriteLine($"{myGames.Item2}, {myGames.Item2}, {myGames.Item3}");

            var map = (name: "Rafiul Islam", age : 22);
            Console.WriteLine($"{map.name} : {map.age}");

            Console.WriteLine(GetTuple(10, "Rafiul"));
        }

        private static (int , string) GetTuple(int id, string name){
            return (id, name);
        }
    }
}