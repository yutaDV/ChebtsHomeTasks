// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
/* Dequeue використання для вилучення елементів з початку черги або з кінця.
 *  черга підтримує роботу з обома кінцями (двостороння черга або deque), 
 *   Dequeue можна використовувати для вилучення елементів з будь-якого кінця черги*/

// Створюємо екземпляр класу Deque
Deque<string> deque = new Deque<string>();

// Додаємо елементи в початок та кінець черги
deque.EnqueueFront("World");
deque.EnqueueBack("Hello");

// Видаляємо елементи з початку та кінця черги та виводимо їх на консоль
Console.WriteLine(deque.DequeueFront() + " " + deque.DequeueBack()); // "Hello World"


public class Deque<T> //<T> - позначення що тут можуть бути значення різного типу
{
    private Node<T> head;
    private Node<T> tail;
    private int count;

    public void EnqueueFront(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (count == 0)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head.Previous = newNode;
            head = newNode;
        }

        count++;
    }

    public void EnqueueBack(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (count == 0)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
        }

        count++;
    }

    public T DequeueFront()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("The deque is empty.");
        }

        T value = head.Value;
        head = head.Next;

        if (head == null)
        {
            tail = null;
        }
        else
        {
            head.Previous = null;
        }

        count--;

        return value;
    }

    public T DequeueBack()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("The deque is empty.");
        }

        T value = tail.Value;
        tail = tail.Previous;

        if (tail == null)
        {
            head = null;
        }
        else
        {
            tail.Next = null;
        }

        count--;

        return value;
    }

    public int Count
    {
        get { return count; }
    }

    private class Node<T>
    {
        public T Value { get; private set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}
