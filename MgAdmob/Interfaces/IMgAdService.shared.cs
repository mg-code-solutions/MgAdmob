namespace Plugin.MgAdmob.Interfaces;

public interface IMgAdService
{
   void Init(IMgAdmobImplementation implementation);
   bool IsInitialised { get; }

   bool IsLoaded { get; }
   void Show();
   void Load(string adUnit);
}