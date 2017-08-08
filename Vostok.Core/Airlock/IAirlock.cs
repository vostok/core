﻿using System.IO;

namespace Vostok.Airlock
{
    // (iloktionov): На первый взгляд кажется, что Airlock не должен знать ничего о природе протаскиваемых через него сообщений.
    // С отправляющей стороны знание их формата должно быть у инструментации какой-то подсистемы (трассировки, логов, метрик).
    // С принимающей же стороны - у демонов, перекладывающих данные из Кафки куда-то.
    // Задачи клиента к Airlock'у:
    // 1. Доставлять до шлюза блобы, снабженные произвольными метками. Эти метки позволят делать какой-то routing и выбирать на читающей стороне десериализатор.
    // 2. Быть кошмарно производительным.
    // 3. Не влиять на работу приложения, даже когда шлюз прилег.
    // Поэтому первый вариант фасада позволяет ровно это: выкинуть в шлюз сериализуемый объект с какой-то меткой в формате fire'n'forget.

    public interface IAirlock
    {
        void Throw<T>(string category, T item);
    }

    public interface IAirlockSerializer<in T>
    {
        void Serialize(T item, IAirlockSink sink);
    }

    public static class AirlockSerializerRegistry
    {
        public static void Register<T>(IAirlockSerializer<T> serializer)
        {
            // ...
        }
    }

    public interface IAirlockSink
    {
        Stream WriteStream { get; }

        void Write(int value);
        void Write(long value);
        void Write(byte value);
        void Write(byte[] value);
        void Write(string value);
        void Write(double value);
        // ...
    }
}