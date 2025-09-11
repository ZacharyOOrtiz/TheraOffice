using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System;

namespace CLI.TheraOffice
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TheraOffice.");
            List<Patient?> patientRecords = PatientServiceProxy.Current.Patients;
            List<Physician?> physicianRecords = PhysicianServiceProxy.Current.Physicians;
            List<Appointment?> appointmentRecords = AppointmentServiceProxy.Current.Appointments;
            bool active = true;

            do
            {
                Console.WriteLine("1. Add a Patient");
                Console.WriteLine("2. Add a Physician");
                Console.WriteLine("3. Make an Appointment");
                Console.WriteLine("4. List all Patients");
                Console.WriteLine("5. List all Physicians");
                Console.WriteLine("6. List all Appointments");
                Console.WriteLine("0. Quit");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var patient = new Patient();
                        Console.WriteLine("Enter the patient's name:");
                        patient.Name = Console.ReadLine();
                        Console.WriteLine("Enter the patient's address:");
                        patient.Address = Console.ReadLine();
                        Console.WriteLine("Enter the patient's birth date:");
                        patient.BirthDate = Console.ReadLine();
                        Console.WriteLine("Enter the patient's race:");
                        patient.Race = Console.ReadLine();
                        Console.WriteLine("Enter the patient's gender:");
                        patient.Gender = Console.ReadLine();
                        Console.WriteLine("Enter any additional diagnoses or prescriptions:");
                        patient.MedNotes = Console.ReadLine();
                        PatientServiceProxy.Current.Create(patient);
                        break;
                    case "2":
                        var physician = new Physician();
                        Console.WriteLine("Enter the physician's name:");
                        physician.Name = Console.ReadLine();
                        Console.WriteLine("Enter the physician's license number:");
                        physician.LicenseNum = Console.ReadLine();
                        Console.WriteLine("Enter the physician's graduation date:");
                        physician.GradDate = Console.ReadLine();
                        Console.WriteLine("Enter the physician's specializations:");
                        physician.Specs = Console.ReadLine();
                        PhysicianServiceProxy.Current.Create(physician);
                        break;
                    case "3":
                        var appointment = new Appointment();
                        PatientServiceProxy.Current.Patients.ForEach(Console.WriteLine);
                        Console.WriteLine("Enter the Patient ID:");
                        string? patientSelection = Console.ReadLine();
                        if (int.TryParse(patientSelection, out int patientID))
                        {
                            var chosenPatient = patientRecords.FirstOrDefault(p => (p?.Id ?? -1) == patientID);
                            if (chosenPatient == null)
                            {
                                Console.WriteLine("Patient not found.");
                                break;
                            }
                            appointment.Patient = chosenPatient;
                        }
                        PhysicianServiceProxy.Current.Physicians.ForEach(Console.WriteLine);
                        Console.WriteLine("Enter the Physician ID:");
                        string? physicianSelection = Console.ReadLine();
                        if (int.TryParse(physicianSelection, out int physicianID))
                        {
                            var chosenPhysician = physicianRecords.FirstOrDefault(b => (b?.Id ?? -1) == physicianID);
                            if (chosenPhysician == null)
                            {
                                Console.WriteLine("Physician not found.");
                                break;
                            }
                            appointment.Physician = chosenPhysician;
                        }
                        Console.Write("Enter appointment date and time (YYYY-MM-DD HH:MM): ");
                        string? datetimeSelection = Console.ReadLine();
                        if (DateTime.TryParse(datetimeSelection, out DateTime apptDateTime))
                        {
                            if (appointment.Physician != null && appointment.Physician.IsAvailable(apptDateTime))
                            {
                                appointment.ApptDateTime = apptDateTime;
                                appointment.Physician.Schedule.Add(apptDateTime);
                                AppointmentServiceProxy.Current.Create(appointment);
                            }
                            else
                            {
                                Console.WriteLine("Booking Failed: Physician is not available at that time.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date/time format.");
                        }
                        break;
                    case "4":
                        PatientServiceProxy.Current.Patients.ForEach(Console.WriteLine);
                        break;
                    case "5":
                        PhysicianServiceProxy.Current.Physicians.ForEach(Console.WriteLine);
                        break;
                    case "6":
                        AppointmentServiceProxy.Current.Appointments.ForEach(Console.WriteLine);
                        break;
                    case "0":
                        active = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Entry. Please Try Again.");
                        break;
                }

            } while(active);
        }
    }
}