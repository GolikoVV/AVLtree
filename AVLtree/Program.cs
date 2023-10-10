using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace AVLTree
    {
        class Program
        {
            static void Main()
            {
                AVLTree<int> avlTree = new AVLTree<int>();

                while (true)
                {
                    Console.WriteLine("Выберите то, что желаете:");
                    Console.WriteLine("1. Хочу вставить элемент");
                    Console.WriteLine("2. Хочу удалить элемент");
                    Console.WriteLine("3. Хочу найти элемент");
                    Console.WriteLine("4. Вывожу элеменеты в порядке возрастания");
                    Console.WriteLine("5. Вывожу элементы в порядке убывания");
                    Console.WriteLine("6. Обхожу и печатаю содержимое дерева");
                    Console.WriteLine("7. Выход");

                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Введите корректное число.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Введите элемент для вставки: ");
                            if (int.TryParse(Console.ReadLine(), out int insertKey))
                            {
                                avlTree.Insert(insertKey);
                                Console.WriteLine("Элемент успешно вставлен.");
                            }
                            else
                            {
                                Console.WriteLine("Введите корректное число.");
                            }
                            break;

                        case 2:
                            Console.Write("Введите элемент для удаления: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteKey))
                            {
                                avlTree.Delete(deleteKey);
                                Console.WriteLine("Элемент успешно удален.");
                            }
                            else
                            {
                                Console.WriteLine("Введите корректное число.");
                            }
                            break;

                        case 3:
                            Console.Write("Введите элемент для поиска: ");
                            if (int.TryParse(Console.ReadLine(), out int findKey))
                            {
                                if (avlTree.Find(findKey))
                                {
                                    Console.WriteLine("Элемент найден.");
                                }
                                else
                                {
                                    Console.WriteLine("Элемент не найден.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Введите корректное число.");
                            }
                            break;

                        case 4:
                            Console.WriteLine("Вывод в порядке возрастания:");
                            avlTree.InOrderTraversal();
                            Console.WriteLine();
                            break;


                        case 5:
                            Console.WriteLine("Обход и вывод содержимое дерева:");
                            avlTree.InOrderTraversal();
                            Console.WriteLine();
                            break;

                        case 6:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Выберите корректное действие.");
                            break;
                    }
                }
            }
        }
    }

