Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace WpfApp30
    Public Class ViewModel
        Public Property Tasks() As ObservableCollection(Of Task)
        Public Sub New()
            InitTasks()
        End Sub
        Private Sub InitTasks()
            Tasks = New ObservableCollection(Of Task)()
            For i As Integer = 0 To 29
                Dim succeed As Boolean = i Mod 2 = 0
                Dim task As Task = ViewModelSource.Create(Function() New Task() With { _
                    .TaskID = i, _
                    .TaskName = "Task" & i, _
                    .Succeed = succeed _
                })
                If succeed Then
                    task.CompletedActions = GenerateActions(i)
                Else
                    task.Errors = GenerateErrors(i)
                End If
                Tasks.Add(task)
            Next i
        End Sub
        Private Function GenerateErrors(ByVal parentTaskID As Integer) As ObservableCollection(Of [Error])
            Dim errors As New ObservableCollection(Of [Error])()
            For i As Integer = 0 To 9
                errors.Add(New [Error]() With { _
                    .ErrorCode = parentTaskID * 1000 + i, _
                    .ErrorDescription = String.Format("Task {0} error {1}", parentTaskID, i) _
                })
            Next i
            Return errors
        End Function
        Private Function GenerateActions(ByVal parentTaskID As Integer) As ObservableCollection(Of Action)
            Dim actions As New ObservableCollection(Of Action)()
            For i As Integer = 0 To 9
                actions.Add(New Action() With {.ActionName = String.Format("Task {0} action {1}", parentTaskID, i)})
            Next i
            Return actions
        End Function
    End Class
    Public Class Task
        Public Property TaskID() As Integer
        Public Property TaskName() As String
        Public Overridable Property Succeed() As Boolean
        Public Property Errors() As ObservableCollection(Of [Error])
        Public Property CompletedActions() As ObservableCollection(Of Action)
    End Class
    Public Class [Error]
        Public Property ErrorCode() As Integer
        Public Property ErrorDescription() As String
    End Class
    Public Class Action
        Public Property ActionName() As String
    End Class
End Namespace
