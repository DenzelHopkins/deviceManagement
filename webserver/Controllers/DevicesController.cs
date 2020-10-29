using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Controllers
{
    //api/commands
    [Route("api/commands")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        
        private readonly IWebServerRepo _repository;
        private readonly IMapper _mapper;

        public DevicesController(IWebServerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<DeviceReadDto>> GetAllDevices()
        {
            var devices = _repository.GetDevices();
            return Ok(_mapper.Map<IEnumerable<DeviceReadDto>>(devices));

        }

        //GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult <DeviceReadDto> GetDeviceById(int id)
        {
            var device = _repository.GetDeviceById(id);
            if(device != null)
            {
                return Ok(_mapper.Map<DeviceReadDto>(device));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<DeviceReadDto> CreateDevice(DeviceCreateDto deviceCreateDto)
        {
            var deviceModel = _mapper.Map<Device>(deviceCreateDto);
            _repository.CreateDevice(deviceModel);
            _repository.SaveChanges();

            return Ok(deviceModel);
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteDeviceById(int id)
        {
            var deviceModelFromRepo = _repository.GetDeviceById(id);
            if(deviceModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteDeviceById(deviceModelFromRepo);
            _repository.SaveChanges();
            
            return NoContent();
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateDevice(int id, DeviceUpdateDto deviceUpdateDto)
        {
            var deviceModelFromRepo = _repository.GetDeviceById(id);
            if(deviceModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(deviceUpdateDto, deviceModelFromRepo);
            _repository.UpdateDevice(deviceModelFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }
    }
}