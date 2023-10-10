using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Объявляем класс AVLNode, представляющий узел AVL-дерева
class AVLNode<T> where T : IComparable<T>
{
    public T Data;            // Значение узла
    public int Height;        // Высота узла
    public AVLNode<T> Left;   // Ссылка на левое поддерево
    public AVLNode<T> Right;  // Ссылка на правое поддерево

    // Конструктор узла, принимающий значение и инициализирующий поля
    public AVLNode(T data)
    {
        Data = data;
        Height = 1;           // Новый узел имеет высоту 1
        Left = null;
        Right = null;
    }
}

// Объявляем класс AVLTree, представляющий AVL-дерево
class AVLTree<T> where T : IComparable<T>
{
    private AVLNode<T> root;  // Корневой узел дерева

    //  метод для вычисления высоты узла
    private int Height(AVLNode<T> node)
    {
        if (node == null)
            return 0;
        return node.Height;
    }

    //  метод для вычисления баланса узла
    private int GetBalance(AVLNode<T> node)
    {
        if (node == null)
            return 0;
        return Height(node.Left) - Height(node.Right);
    }
    // метод Find в класс AVLTree. выполняет поиск заданного значения data в дереве, начиная с корня root,
    // и возвращает true, если значение найдено, и false, если не найдено. 
    public bool Find(T data)
    {
        return Find(root, data);
    }

    private bool Find(AVLNode<T> node, T data)
    {
        if (node == null)
            return false;

        int comparisonResult = data.CompareTo(node.Data);
        if (comparisonResult == 0)
            return true;
        else if (comparisonResult < 0)
            return Find(node.Left, data);
        else
            return Find(node.Right, data);
    }

    //  метод для обновления высоты узла
    private void UpdateHeight(AVLNode<T> node)
    {
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    //  метод для выполнения малого правого 
    private AVLNode<T> RotateRight(AVLNode<T> y)
    {
        AVLNode<T> x = y.Left;
        AVLNode<T> temp = x.Right;

        x.Right = y;
        y.Left = temp;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    //  метод для выполнения малого левого вращения
    private AVLNode<T> RotateLeft(AVLNode<T> x)
    {
        AVLNode<T> y = x.Right;
        AVLNode<T> temp = y.Left;

        y.Left = x;
        x.Right = temp;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    //  метод для вставки значения в дерево
    public void Insert(T data)
    {
        root = Insert(root, data);
    }

    //  метод для рекурсивной вставки значения
    private AVLNode<T> Insert(AVLNode<T> node, T data)
    {
        if (node == null)
            return new AVLNode<T>(data);

        if (data.CompareTo(node.Data) < 0)
            node.Left = Insert(node.Left, data);
        else if (data.CompareTo(node.Data) > 0)
            node.Right = Insert(node.Right, data);
        else // Если ключи равны, не позволяем дубликатам
            return node;

        UpdateHeight(node);

        int balance = GetBalance(node);

        // Балансировка
        if (balance > 1)
        {
            if (data.CompareTo(node.Left.Data) < 0)
                return RotateRight(node);
            else
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }
        if (balance < -1)
        {
            if (data.CompareTo(node.Right.Data) > 0)
                return RotateLeft(node);
            else
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }

        return node;
    }

    //  метод для удаления значения из дерева
    public void Delete(T data)
    {
        root = Delete(root, data);
    }

    //  метод для рекурсивного удаления значения
    private AVLNode<T> Delete(AVLNode<T> node, T data)
    {
        if (node == null)
            return node;

        if (data.CompareTo(node.Data) < 0)
            node.Left = Delete(node.Left, data);
        else if (data.CompareTo(node.Data) > 0)
            node.Right = Delete(node.Right, data);
        else
        {
            if ((node.Left == null) || (node.Right == null))
            {
                AVLNode<T> temp = null;
                if (temp == node.Left)
                    temp = node.Right;
                else
                    temp = node.Left;

                if (temp == null)
                {
                    temp = node;
                    node = null;
                }
                else
                    node = temp;
            }
            else
            {
                AVLNode<T> temp = MinValueNode(node.Right);
                node.Data = temp.Data;
                node.Right = Delete(node.Right, temp.Data);
            }
        }

        if (node == null)
            return node;

        UpdateHeight(node);

        int balance = GetBalance(node);

        // Балансировка
        if (balance > 1)
        {
            if (GetBalance(node.Left) >= 0)
                return RotateRight(node);
            else
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }
        if (balance < -1)
        {
            if (GetBalance(node.Right) <= 0)
                return RotateLeft(node);
            else
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }

        return node;
    }

    //  метод для поиска минимального элемента в дереве
    private AVLNode<T> MinValueNode(AVLNode<T> node)
    {
        AVLNode<T> current = node;

        while (current.Left != null)
            current = current.Left;

        return current;
    }

    //  метод для обхода дерева в порядке возрастания (in-order)
    public void InOrderTraversal()
    {
        InOrderTraversal(root);
    }

    //  метод для рекурсивного обхода дерева
    //выполняет обход всех узлов дерева в определенном порядке, используя рекурсию
    private void InOrderTraversal(AVLNode<T> node)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left);
            Console.Write(node.Data + " ");
            InOrderTraversal(node.Right);
        }
    }
}


