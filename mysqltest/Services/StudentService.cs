/*using AutoMapper;
using mysqltest.Helpers;
using mysqltest.Models;
using mysqltest.Models.Users;
using System.Collections.Generic;
using WebApi.Authorization;
using BCryptNet = BCrypt.Net.BCrypt;

namespace mysqltest.Services
{
    public interface IStudentService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        IEnumerable<Student> GetAll();

        User GetById(int id);

        void Register(RegisterRequest model);

        void Update(int id, UpdateRequest model);

        void Delete(int id);
    }

    public class StudentService : IStudentService
    {
        private ClubsContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public StudentService(
            ClubsContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Students.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Students.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            _context.Students.Add(user);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _context.Students.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Students.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Students.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUser(int id)
        {
            var user = _context.Students.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
*/