using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CoreApi;

namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для WorkWondow.xaml
    /// </summary>
    public partial class WorkWondow : Window
    {
        private Session curentSession;
        public WorkWondow(Session session)
        {
            InitializeComponent();
            curentSession = session;
        }
    }
}
