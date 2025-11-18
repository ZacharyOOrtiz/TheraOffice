using Library.TheraOffice.Models;
using Newtonsoft.Json;

namespace API.TheraOffice.Nothing
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
            _root = @"Users/zacharyortiz/temp";
            _patientRoot = $"{_root}\\Patients";
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
            //set up a new Id if one doesn't already exist
            if(patient.Id <= 0)
            {
                patient.Id = LastPatientKey + 1;
            }

            //go to the right place
            string path = $"{_patientRoot}\\{patient.Id}.json";
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(patient));

            //return the item, which now has an id
            return patient;
        }
        
        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();
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
                return _patients;
            }
        }
        
    }
}