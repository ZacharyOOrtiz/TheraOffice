using Library.TheraOffice.Models;
using Newtonsoft.Json;

namespace API.TheraOffice.Database
{
    public class Filebase
    {
        private string _root;
        private string _patientRoot;
        private static Filebase _instance;
        private static object instanceLock = new object();
        
        public static Filebase Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                       _instance = new Filebase();
                    }
                }
                
                return _instance;
            }
        }

        private Filebase()
        {
            // Updates path to be dynamic for Mac/Windows
            _root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _patientRoot = Path.Combine(_root, "Patients");
            
            // Ensure the directory exists
            if (!Directory.Exists(_patientRoot))
            {
                Directory.CreateDirectory(_patientRoot);
            }
        }

        public int LastPatientKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Patient Create(Patient patient)
        {
            // Set up a new Id if one doesn't already exist
            if(patient.Id <= 0)
            {
                patient.Id = LastPatientKey + 1;
            }

            // Path.Combine handles the slashes for Mac/Windows automatically
            string path = Path.Combine(_patientRoot, $"{patient.Id}.json");

            // If the item has been previously persisted, blow it up (for updates)
            if(File.Exists(path))
            {
                File.Delete(path);
            }

            // Write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(patient));

            return patient;
        }

        // NEW METHOD: Added so PatientEC can call it
        public bool Delete(int id)
        {
            string path = Path.Combine(_patientRoot, $"{id}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        
        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();
                // Only try to get files if the folder actually exists
                if (root.Exists)
                {
                    foreach(var patientFile in root.GetFiles())
                    {
                        var patient = JsonConvert
                            .DeserializeObject<Patient>
                            (File.ReadAllText(patientFile.FullName));
                        if(patient != null)
                        {
                            _patients.Add(patient);
                        }
                    }
                }
                return _patients;
            }
        }
    }
}