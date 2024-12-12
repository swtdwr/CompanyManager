namespace CompanyManager.Domain.Extensions;

public static class ListExtensions
{
    /// <summary>
    /// Перемещает указанный элемент вверх в списке.
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке.</typeparam>
    /// <param name="list">Список, в котором нужно переместить элемент.</param>
    /// <param name="item">Элемент, который необходимо переместить.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если <paramref name="item"/> не найден в списке.</exception>
    public static void MoveUp<T>(this IList<T> list, T item)
    {
        var index = list.IndexOf(item);
        list.MoveUp(index);
    }
    
    /// <summary>
    /// Перемещает указанный элемент вниз в списке.
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке.</typeparam>
    /// <param name="list">Список, в котором нужно переместить элемент.</param>
    /// <param name="item">Элемент, который необходимо переместить.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если <paramref name="item"/> не найден в списке.</exception>
    public static void MoveDown<T>(this IList<T> list, T item)
    {
        var index = list.IndexOf(item);
        list.MoveDown(index);
    }
    
    private static void MoveUp<T>(this IList<T> list, int index)
    {
        if (index <= 0 || index >= list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        
        (list[index], list[index - 1]) = (list[index - 1], list[index]);
    }
    
    private static void MoveDown<T>(this IList<T> list, int index)
    {
        if (index < 0 || index >= list.Count - 1)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        
        (list[index], list[index + 1]) = (list[index + 1], list[index]);
    }
}