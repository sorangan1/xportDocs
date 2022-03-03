using System.Windows;
using System.IO;
using System.Reflection;

namespace xportdocs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckProjects();
        }

        private void CopyHtdocs(object sender, RoutedEventArgs e)
        {
            foreach (string projectFolder in projectList.SelectedItems)
            {
                DirectoryInfo source = new DirectoryInfo(projectFolder);

                var targetPath = System.AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo target = new DirectoryInfo(targetPath + source.Name);
                //DirectoryInfo target = new DirectoryInfo("C:\\Users\\ragnar\\Desktop\\" + source.Name);

                CopyFiles(source, target);
            }
        }

        private void ImportHtdocs(object sender, RoutedEventArgs e)
        {

        }

        private void CheckProjects()
        {
            string path = "C:\\xampp\\htdocs\\";

            projectList.Items.Clear();
            foreach (var project in Directory.GetDirectories(path))
            {
                projectList.Items.Add(project);
            }
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            foreach (DirectoryInfo subDirectory in source.GetDirectories())
            {
                DirectoryInfo nextSubTarget = target.CreateSubdirectory(subDirectory.Name);
                CopyFiles(subDirectory, nextSubTarget);
            }
        }

        private void RefreshProjects(object sender, RoutedEventArgs e)
        {
            CheckProjects();
        }
    }
}
