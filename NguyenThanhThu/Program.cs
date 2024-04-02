using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TodoItem
{
    public string Title { get; set; }
    public int Priority { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

class TodoListManager
{
    private List<TodoItem> todoList = new List<TodoItem>();

    public void AddTodoItem(string title, int priority, string description)
    {
        todoList.Add(new TodoItem { Title = title, Priority = priority, Description = description });
        Console.WriteLine("Đã thêm việc mới: " + title);
    }

    public void RemoveTodoItem(int index)
    {
        if (index >= 0 && index < todoList.Count)
        {
            todoList.RemoveAt(index);
            Console.WriteLine("Đã xóa việc tại vị trí {0}.", index);
        }
        else
        {
            Console.WriteLine("Không tìm thấy việc cần xóa.");
        }
    }

    public void UpdateTodoItemStatus(int index, bool status)
    {
        if (index >= 0 && index < todoList.Count)
        {
            todoList[index].IsCompleted = status;
            Console.WriteLine("Đã cập nhật trạng thái của việc \"{0}\" thành {1}.", todoList[index].Title, status ? "Hoàn thành" : "Chưa hoàn thành");
        }
        else
        {
            Console.WriteLine("Không tìm thấy việc cần cập nhật.");
        }
    }

    public void SearchTodoItem(string title)
    {
        var foundItems = todoList.Where(item => item.Title.ToLower().Contains(title.ToLower())).ToList();
        if (foundItems.Any())
        {
            Console.WriteLine("Các việc cần làm có tên chứa \"{0}\":", title);
            foreach (var item in foundItems)
            {
                Console.WriteLine("- {0}", item.Title);
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy việc cần làm có tên chứa \"{0}\".", title);
        }
    }

    public void DisplaySortedTodoListByPriority()
    {
        todoList = todoList.OrderByDescending(item => item.Priority).ToList();
        Console.WriteLine("Danh sách việc cần làm theo độ ưu tiên giảm dần:");
        foreach (var item in todoList)
        {
            Console.WriteLine("- {0} (Độ ưu tiên: {1})", item.Title, item.Priority);
        }
    }

    public void DisplayAllTodoList()
    {
        Console.WriteLine("Danh sách toàn bộ việc cần làm:");
        foreach (var item in todoList)
        {
            Console.WriteLine("- {0} (Độ ưu tiên: {1})", item.Title, item.Priority);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        TodoListManager todoListManager = new TodoListManager();

        while (true)
        {
            Console.WriteLine("Chọn một trong các tùy chọn sau:");
            Console.WriteLine("1. Thêm một việc mới");
            Console.WriteLine("2. Xóa một việc cần làm");
            Console.WriteLine("3. Cập nhật trạng thái của một việc cần làm");
            Console.WriteLine("4. Tìm kiếm việc cần làm theo tên");
            Console.WriteLine("5. Hiển thị danh sách việc cần làm theo độ ưu tiên giảm dần");
            Console.WriteLine("6. Hiển thị toàn bộ danh sách việc cần làm");
            Console.WriteLine("7. Thoát ứng dụng");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Vui lòng nhập một số nguyên từ 1 đến 7.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Nhập thông tin việc mới:");
                    Console.WriteLine("Tên việc:");
                    string title = Console.ReadLine();
                    Console.WriteLine("Độ ưu tiên (1 - 5):");
                    int priority;
                    if (!int.TryParse(Console.ReadLine(), out priority) || priority < 1 || priority > 5)
                    {
                        Console.WriteLine("Độ ưu tiên không hợp lệ. Độ ưu tiên sẽ được đặt mặc định là 1.");
                        priority = 1;
                    }
                    Console.WriteLine("Mô tả:");
                    string description = Console.ReadLine();
                    todoListManager.AddTodoItem(title, priority, description);
                    break;
                case 2:
                    Console.WriteLine("Nhập vị trí của việc cần xóa:");
                    if (int.TryParse(Console.ReadLine(), out int removeIndex))
                    {
                        todoListManager.RemoveTodoItem(removeIndex);
                    }
                    else
                    {
                        Console.WriteLine("Vui lòng nhập một số nguyên.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Nhập vị trí của việc cần cập nhật trạng thái:");
                    if (int.TryParse(Console.ReadLine(), out int updateIndex))
                    {
                        Console.WriteLine("Nhập trạng thái mới (true/false):");
                        if (bool.TryParse(Console.ReadLine(), out bool newStatus))
                        {
                            todoListManager.UpdateTodoItemStatus(updateIndex, newStatus);
                        }
                        else
                        {
                            Console.WriteLine("Trạng thái không hợp lệ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vui lòng nhập một số nguyên.");
                    }
                    break;
                case 4:
                    Console.WriteLine("Nhập tên việc cần tìm:");
                    string searchTitle = Console.ReadLine();
                    todoListManager.SearchTodoItem(searchTitle);
                    break;
                case 5:
                    todoListManager.DisplaySortedTodoListByPriority();
                    break;
                case 6:
                    todoListManager.DisplayAllTodoList();
                    break;
                case 7:
                    Console.WriteLine("Ứng dụng đã kết thúc.");
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }
}