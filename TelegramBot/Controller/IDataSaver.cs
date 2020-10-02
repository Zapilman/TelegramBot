using System;
using System.Collections.Generic;


namespace TelegramBot.Controller
{
    public interface IDataSaver
    {

        void Save<T>(List<T> items) where T : class;

        List<T> Load<T>() where T : class;
    }
}
