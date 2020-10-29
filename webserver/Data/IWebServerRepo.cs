using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Data
{
        public interface IWebServerRepo
        {
            IEnumerable<Device> GetDevices();
            Device GetDeviceById(int id);
            void CreateDevice(Device dev);
            void DeleteDeviceById(Device dev);
            void UpdateDevice(Device dev);
            bool SaveChanges();
    }
}