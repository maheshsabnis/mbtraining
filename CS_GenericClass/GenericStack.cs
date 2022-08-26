using System;
namespace CS_GenericClass
{
    /// <summary>
    /// The Generic Stack Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericStack<T>
    {
        // define a Generic Array that will store the data for the class
        T[] array = new T[5];

        int top = 0;
        /// <summary>
        /// Generic Method with Generic Input Parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Push(T item)
        {
            array[top++] = item;
        }
        /// <summary>
        /// Generic Method with Outpt Parameter
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            return array[--top];
        }

        public T[] Display()
        {
            return array;
        }
    }
}

