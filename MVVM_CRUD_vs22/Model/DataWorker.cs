using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_mvvm_.Model.Data;


namespace CRUD_mvvm_.Model
{
    internal static class DataWorker
    {
        //получить все офисы
        public static List<Office> GetAllOffices()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var  result = db.Offices.ToList();
                return result;  
            }
        }
        //создать офис
        public static string CreateOffice(string name, int inn, string address)
        {
            string result = "Уже существует";
            using(ApplicationContext db = new ApplicationContext())
            {
                //проверка существует ли офис
                bool cheakIsExist = db.Offices.Any(el=>el.Name == name && el.Inn == inn && el.Address == address);
                if (!cheakIsExist)
                {
                    Office newOffice = new Office
                    {
                        Name = name,
                        Inn = inn,
                        Address = address
                    }; 
                    db.Offices.Add(newOffice);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //удалить офис
        public static string DeleteOffice(Office office)
        {
            string result = "Такого отдела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
               db.Offices.Remove(office);
               db.SaveChanges();
               result = "Сделано! Офис " + office.Name + " удалён";
            }
            return result;
        }
        //редактировать офис
        public static string EditOffice(Office OldOffice, string newName, int newInn)
        {
            string result = "Такого офиса не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Office office = db.Offices.FirstOrDefault(d => d.Id == OldOffice.Id);
                office.Name = newName;
                office.Inn = newInn;
                db.SaveChanges();
                result = "Сделано! Офис " + office.Name + " изменен";
            } 
            return result;
        }
        //получить все позиции
        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }
        //создать позицию
        public static string CreatePosition(string name, decimal salary, int maxStaff, Office office)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверка существует ли позиция
                bool cheakIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary
                && el.MaxStaff == maxStaff && el.Office == office);
                if (!cheakIsExist)
                {
                    Position newPosition = new Position 
                    { 
                        Name = name, 
                        Salary = salary, 
                        MaxStaff = maxStaff, 
                        OfficeId = office.Id
                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //удалить позицию
        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + " удалён";
            }
            return result;
        }
        //редактировать позицию
        public static string EditPosition(Position OldPosition, string newName, decimal newSalary, int newMaxStaff,Office newOffice)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == OldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxStaff = newMaxStaff;
                position.Office = newOffice;
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + " изменена.";
            }
            return result;
        }
        //получить всех сотрудников
        public static List<Staff> GetAllStaff()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db._Staff.ToList();
                return result;
            }
        }
        //создать сотрудника
        public static string CreateStaff(string name,string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверка существует ли сотрудник
                bool cheakIsExist = db._Staff.Any(el => el.Name == name && el.SurName == surName
                && el.Phone == phone && el.Position == position);
                if (!cheakIsExist)
                {
                    Staff newStaff = new Staff 
                    { 
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db._Staff.Add(newStaff);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //удалить сотрудника
        public static string DeleteStaff(Staff staff)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db._Staff.Remove(staff);
                db.SaveChanges();
                result = "Сделано! Сотрудник " + staff.Name + " удалён.";
            }
            return result;
        }
        //редактировать сотрудника
        public static string EditStaff(Staff OldStaff, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Staff staff = db._Staff.FirstOrDefault(p => p.Id == OldStaff.Id);
                staff.Name = newName;
                staff.SurName = newSurName;
                staff.Phone = newPhone;
                staff.Position = newPosition;
                db.SaveChanges();
                result = "Сделано! Сотрудник " + staff.Name + " изменен.";
            }
            return result;
        }

        //Получение позиции по id позиции
        public static Position GetPositionById(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        //Получение офиса по id офиса
        public static Office GetOfficeById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Office office = db.Offices.FirstOrDefault(p => p.Id == id);
                return office;
            }
        }

        //Получение всех сотрудников по id позиции
        public static List<Staff> GetAllStaffByPositionId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Staff> staffs = (from staff in GetAllStaff() where staff.PositionId == id select staff).ToList();
                return staffs;
            }
        }
        //Получение всех позиций по id офиса
        public static List<Position> GetAllPositionsByOfficeId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.OfficeId == id select position).ToList();
                return positions;
            }
        }
    }
}
