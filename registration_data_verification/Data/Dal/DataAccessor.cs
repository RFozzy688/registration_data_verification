using registration_data_verification.Data.Context;

namespace registration_data_verification.Data.Dal
{
    public class DataAccessor
    {
        private readonly DataContext _context;
        public UserDao UserDao { get; private set; }
        public DataAccessor(DataContext context)
        {
            _context = context;
            UserDao = new(_context);
        }
    }
}
