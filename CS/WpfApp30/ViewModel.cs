using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp30 {
    public class ViewModel {
        public ObservableCollection<Task> Tasks {
            get;
            set;
        }
        public ViewModel() {
            InitTasks();
        }
        void InitTasks() {
            Tasks = new ObservableCollection<Task>();
            for (int i = 0; i < 30; i++) {
                bool succeed = i % 2 == 0;
                Task task = ViewModelSource.Create(() => new Task() { TaskID = i, TaskName = "Task" + i, Succeed = succeed });
                if (succeed)
                    task.CompletedActions = GenerateActions(i);
                else
                    task.Errors = GenerateErrors(i);
                Tasks.Add(task);
            }
        }
        ObservableCollection<Error> GenerateErrors(int parentTaskID) {
            ObservableCollection<Error> errors = new ObservableCollection<Error>();
            for (int i = 0; i < 10; i++) {
                errors.Add(new Error() { ErrorCode = parentTaskID * 1000 + i, ErrorDescription = string.Format("Task {0} error {1}", parentTaskID, i) });
            }
            return errors;
        }
        ObservableCollection<Action> GenerateActions(int parentTaskID) {
            ObservableCollection<Action> actions = new ObservableCollection<Action>();
            for (int i = 0; i < 10; i++) {
                actions.Add(new Action() { ActionName = string.Format("Task {0} action {1}", parentTaskID, i) });
            }
            return actions;
        }
    }
    public class Task {
        public int TaskID {
            get;
            set;
        }
        public string TaskName {
            get;
            set;
        }
        public virtual bool Succeed {
            get;
            set;
        }
        public ObservableCollection<Error> Errors {
            get;
            set;
        }
        public ObservableCollection<Action> CompletedActions {
            get;
            set;
        }
    }
    public class Error {
        public int ErrorCode {
            get;
            set;
        }
        public string ErrorDescription {
            get;
            set;
        }
    }
    public class Action {
        public string ActionName {
            get;
            set;
        }
    }
}
