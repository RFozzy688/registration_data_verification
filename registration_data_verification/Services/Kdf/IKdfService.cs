namespace registration_data_verification.Services.Kdf
{
    public interface IKdfService
    {
        void Config(int iterationCount, int dkLength);
        String GetDerivedKey(String password, String salt);
    }
}
