using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _speciality = string.Empty;
        private string _group = string.Empty;
        private Student? _selectedStudent;

        public ObservableCollection<Student> Students { get; } = new();

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Speciality
        {
            get => _speciality;
            set { _speciality = value; OnPropertyChanged(); }
        }

        public string Group
        {
            get => _group;
            set { _group = value; OnPropertyChanged(); }
        }

        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public StudentViewModel()
        {
            AddCommand = new RelayCommand(_ => AddStudent(), _ => CanAdd());
            DeleteCommand = new RelayCommand(_ => DeleteStudent(), _ => SelectedStudent != null);
        }

        private bool CanAdd() =>
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(Speciality) &&
            !string.IsNullOrWhiteSpace(Group);

        private void AddStudent()
        {
            Students.Add(new Student
            {
                Name = Name.Trim(),
                Speciality = Speciality.Trim(),
                Group = Group.Trim()
            });

            Name = string.Empty;
            Speciality = string.Empty;
            Group = string.Empty;
        }

        private void DeleteStudent()
        {
            if (SelectedStudent != null)
            {
                Students.Remove(SelectedStudent);
                SelectedStudent = null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}