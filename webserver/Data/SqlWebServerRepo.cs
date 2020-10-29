using System;
using System.Collections.Generic;
using System.Linq;
using WebServerCon.Data;
using WebServer.Models;

namespace WebServer.Data
{
    public class SqlWebServerRepo : IWebServerRepo
    {
        private readonly WebServerContext _context;

        public SqlWebServerRepo(WebServerContext context)
        {
            _context = context;
        }
        public Device GetDeviceById(int id)
        {
            return _context.Devices.FirstOrDefault(p => p.GeräteID == id);
        }
        public IEnumerable<Device> GetDevices()
        {
            return _context.Devices.ToList();
        }
        public void DeleteDeviceById(Device dev)
        {
            if(dev == null)
            {
                throw new NotImplementedException();
            }
            _context.Devices.Remove(dev);
            _context.SaveChanges();       
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void CreateDevice(Device dev)
        {
            if (dev == null)
            {
                throw new ArgumentNullException(nameof(dev));
            }
            _context.Devices.Add(dev);
        }

        public void UpdateDevice(Device dev)
        {
            if (dev == null)
            {
                throw new ArgumentNullException(nameof(dev));
            }
            _context.Devices.Update(dev);
        }
    }
}